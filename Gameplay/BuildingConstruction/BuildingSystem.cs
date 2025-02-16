using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using FireNBM.Pattern;
using UnityEngine.EventSystems;
using UnityEngine.AI;

namespace FireNBM
{
    /// <summary>
    ///     Hệ thống xây công trình trên bản đồ.
    /// </summary>
    [AddComponentMenu("FireNBM/System/Building System")]
    public class BuildingSystem : Singleton<BuildingSystem>
    {
        private Grid m_grid;                                    // Thực hiện thao tác với lưới.                         
        private GridLayout m_gridLayout;                        // Giúp định vị các đối tượng trong lưới.

        [SerializeField] private Tilemap m_tilemapOccupied;     // Lưu trữ các vùng đã chiếm hữu.
        [SerializeField] private Tilemap m_tilemapHint;         // Gợi ý các vùng có thể xây dựng.
        [SerializeField] private TileBase m_tileHint;           // Hiển thị khu phạm vị của công trình.

        [SerializeField] private Material m_placeable;          // Đánh đấu công trình có thể xây dựng được.
        [SerializeField] private Material m_noPlaceable;        // Đã bị chiếm.

        // Lựa chọn công trình để xây.
        private Dictionary<TypeNameBuilding, Transform> m_mapHintBuilding;
        private Dictionary<TypeNameBuilding, MeshRenderer[]> m_cachedRenderers;

        // Dành cho công trình hiện tại
        private TypeNameBuilding m_nameBuilding;
        private GameObject m_buildingCurrent;   
        private BuildingDragComp m_buildingDragComp;
        private BuildingPlacebleComp m_BuildingPlaceComp;
 
        // Dữ liệu của công trình.
        private bool m_isPlaceable;                             // Liên quan đến thiết lập material 
        private bool m_isUpdateBuilding;                        // Đánh dấu đang đặt công trình.
        private BoundsInt m_previousArea;                       // Lưu khu vực cũ để xóa vùng gợi ý trước đó.
        private Vector3 m_prePosBuilding;
        private TileBase[] m_cachedTiles; 

        private MessagingSystem m_messageManager; 
        public static BuildingSystem Instance { get => InstanceSingletonInScene; }



        // ----------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // ///////////////////////////////////////////////////////////////////////////////////
        
        protected override void Awake()
        {
            base.Awake();

            m_grid = GetComponent<Grid>();
            DebugUtils.HandleErrorIfNullGetComponent<Grid, BuildingSystem>(m_grid, this, gameObject);

            m_gridLayout = GetComponent<GridLayout>();
            DebugUtils.HandleErrorIfNullGetComponent<GridLayout, BuildingSystem>(m_gridLayout, this, gameObject);
            
            m_prePosBuilding = Vector3.zero;
            m_isUpdateBuilding = false;
            m_isPlaceable = true;
            m_buildingCurrent = null;
            m_BuildingPlaceComp = null;
            m_nameBuilding = TypeNameBuilding.None;

            m_mapHintBuilding = new Dictionary<TypeNameBuilding, Transform>();
            m_cachedRenderers = new Dictionary<TypeNameBuilding, MeshRenderer[]>();

            m_messageManager = MessagingSystem.Instance;
            m_tilemapOccupied.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            // Nhận thông điệp để tạo một công trình mới.
            m_messageManager.FunAttachListener(typeof(MessageInitializeBuilding), OnStartCreateBuilding);
        }

        private void OnDisable()
        {
            // Xóa thông điệp.
            m_messageManager.FunDetachListener(typeof(MessageInitializeBuilding), OnStartCreateBuilding);
        }

        private void FixedUpdate()
        {   
            if (m_isUpdateBuilding == false)
                return;

            UpdateBuildingAreaIndication();
        }

        private void Update()
        {
            if (m_isUpdateBuilding == true && 
                Input.GetKeyDown(KeyCode.Mouse0) == true &&
                EventSystem.current.IsPointerOverGameObject() == false)
            {
                // Bắt đầu thực hiện xây dựng công trình tại vị trí nhấn chuột nếu hợp lệ.
                if (CanPlaneBuilding() == true)
                {
                    FunOccupyArea();
                    HandleBuidlingObject();
                    ResetBuilding();
                }
            }
        }

        private void OnDestroy()
        {
            m_mapHintBuilding.Clear();
            m_cachedRenderers.Clear();
        }

        // -----------------------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // ////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thêm công trình vào danh sách xây dựng. </summary>
        /// -----------------------------------------------------
        public void FunAddDataBuildingSystem(BuildingRaceDataSO buildingRaceData)
        {
            if (m_mapHintBuilding.ContainsKey(buildingRaceData.FlyweightData.NameBuilding) == true)
            {
                DebugUtils.FunLogError($"Error: This type {buildingRaceData.FlyweightData.NameBuilding} has existed. ");
                return;
            }

            var buildingObject = Instantiate(buildingRaceData.BuildingPrefab);
            buildingObject.AddComponent<BuildingPlacebleComp>();
            buildingObject.AddComponent<BuildingDragComp>();

            if (buildingObject.TryGetComponent<NavMeshObstacle>(out var navMeshObstacle) == true)
                Destroy(navMeshObstacle);

            // Dùng để lúc xây thì lấy dữ liệu này để lấy công trình cần xây.
            var buildingFlyweightComp = buildingObject.GetComponent<BuildingFlyweightComp>();
            buildingFlyweightComp.FunSetDataFlyweight(buildingRaceData.FlyweightData);

            var outlineComp = buildingObject.GetComponent<Outline>();
            if (outlineComp != null)
                Destroy(outlineComp);

            buildingObject.tag = "BuildingObjectSystem";    // Đế tránh phải xung đột hightlight 
            buildingObject.gameObject.SetActive(false);

            AddDataCachedRenderers(buildingRaceData.FlyweightData.NameBuilding, buildingObject);
            SetMaterialGhost(buildingRaceData.FlyweightData.NameBuilding, m_placeable);
            m_mapHintBuilding.Add(buildingRaceData.FlyweightData.NameBuilding, buildingObject.transform);
        }

        /// <summary>
        ///     Chuyển vị trí về lưới gần nhất để đảm bảo công trình khớp với lưới. </summary>
        /// ----------------------------------------------------------------------------------
        public Vector3 FunSnapToGrid(Vector3 position)
        {
            Vector3Int cellPos = m_gridLayout.WorldToCell(position);
            position = m_grid.GetCellCenterWorld(cellPos);

            Vector3 cellSize = m_gridLayout.cellSize;
            Vector3 offset = new Vector3(0.5f * cellSize.x, 0.0f, 0.5f * cellSize.z);

            return position - offset;
        }

        /// <summary>
        ///     Đánh dấu trên tilemap một công trình mới được xây dựng.</summary>
        /// ---------------------------------------------------------------------
        public void FunOccupyArea()
        {
            Vector3Int startCell = m_gridLayout.WorldToCell(m_BuildingPlaceComp.FunGetStartPosition());
            Vector3Int size = m_BuildingPlaceComp.FunGetSize();

            for (int x = 0; x < size.x; ++x)
            {
                for (int y = 0; y < size.y; ++y)
                {
                    Vector3Int tilePosition = new Vector3Int(startCell.x + x, startCell.y + y, startCell.z);
                    m_tilemapOccupied.SetTile(tilePosition, m_tileHint); 
                }
            }
        }

        /// <summary>
        ///     Chuyển tọa độ thể giới sang tọa độ lưới. </summary>
        /// ------------------------------------------------------- 
        public Vector3Int FunWorldToGridCell(Vector3 worldPos) => m_gridLayout.WorldToCell(worldPos);


        // ---------------------------------------------------------------------------------
        // FUNCTION HELPER
        // ---------------
        // //////////////////////////////////////////////////////////////////////////////////

        // Gợi ý nơi có thể đặt khi di chuyển.
        // -----------------------------------
        public void UpdateBuildingAreaIndication()
        {
            // Nếu không di chuyển thì không nên tính toán. Chỉ nên tính toán 1 lần.
            if (m_prePosBuilding == m_buildingDragComp.FunGetPosPlaceableBuilding())
                return;

            ClearPreviousHintArea(); 
            RecalculateAreaIndication();
        }

        // Xóa vùng gợi ý trước đó.
        // ------------------------
        private void ClearPreviousHintArea()
        {
            if (m_previousArea == null)
                return;

            TileBase[] toClear = new TileBase[m_previousArea.size.x * m_previousArea.size.y * m_previousArea.size.z];
            FillTiles(toClear, null);
            m_tilemapHint.SetTilesBlock(m_previousArea, toClear);
        }

        // Tính toán lại khu vực được chỉ định xây công trình.
        // ---------------------------------------------------
        private void RecalculateAreaIndication()
        {
            // Lấy phạm vi của công trình tại vị trí hiện tại.
            m_BuildingPlaceComp.Area.position = m_gridLayout.WorldToCell(m_BuildingPlaceComp.FunGetStartPosition());;
            BoundsInt buildingArea = m_BuildingPlaceComp.Area;
            
            // Kiểm tra và báo hiệu dựa trên khu vực cần kiểm tra.
            TileBase[] areaIndicaion = GetTilesBlock(buildingArea, m_tilemapOccupied);
            TileBase[] arrHint = new TileBase[areaIndicaion.Length];

            bool isBuild = false;
            for (int i = 0; i < areaIndicaion.Length; ++i)
            {
                // Báo hiệu nếu trong vùng đó nằm trong phạm vi 
                // của công trình đã tạo trước đó.
                if (areaIndicaion[i] != null && isBuild == false)  
                {
                    isBuild = true;
                    if (m_isPlaceable == true)
                    {
                        m_isPlaceable = false;
                        SetMaterialGhost(m_nameBuilding, m_noPlaceable);
                    }
                }
                arrHint[i] = m_tileHint;
            }

            // Thiết lập hiệu ứng có thể đặt được.
            if (isBuild == false && m_isPlaceable == false)
            {
                m_isPlaceable = true;
                SetMaterialGhost(m_nameBuilding, m_placeable);
            }

            // Hiển thị vùng gợi ý cho người chơi.
            m_tilemapHint.SetTilesBlock(buildingArea, arrHint);
            m_previousArea = buildingArea;
            m_prePosBuilding = m_buildingDragComp.FunGetPosPlaceableBuilding();
        }

        // Kiểm tra xem có thể đặt công trình ở đây ko.
        // --------------------------------------------
        private bool CanPlaneBuilding()
        {
            BoundsInt newArea = new BoundsInt
            {
                position = m_gridLayout.WorldToCell(m_BuildingPlaceComp.FunGetStartPosition()),
                size = m_BuildingPlaceComp.FunGetSize()
            };

            // Kiểm tra trong vùng này có ô nào được sử dụng hay chưa.
            foreach (var cellPos in newArea.allPositionsWithin)
            {
                if (m_tilemapOccupied.HasTile(cellPos))
                    return false;
            }
            return true;
        }

        // Lấy tất cả các ô trong một vùng 
        // -------------------------------
        private TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
        {
            int requiredSize = area.size.x * area.size.y * area.size.z;
            
            // Nếu mảng cache chưa tồn tại hoặc quá nhỏ thì tạo mới.
            if (m_cachedTiles == null || m_cachedTiles.Length < requiredSize)
            {
                m_cachedTiles = new TileBase[requiredSize];
            }

            // Điền dữ liệu vào mảng.
            int counter = 0;
            foreach (var cellPos in area.allPositionsWithin)
                m_cachedTiles[counter++] = tilemap.GetTile(cellPos);

            return m_cachedTiles;
        }

        // Đánh dấu vùng gợi ý.
        // --------------------
        private void FillTiles(TileBase[] arr, TileBase type)
        {
            for (int i = 0; i < arr.Length; ++i)
                arr[i] = type;
        }

        // Làm mới khi đặt công trình thành công.
        // --------------------------------------
        private void ResetBuilding()
        {
            ClearPreviousHintArea();
            m_tilemapHint.gameObject.SetActive(false);
            m_buildingCurrent.SetActive(false);

            m_buildingCurrent = null;
            m_BuildingPlaceComp = null;
            m_buildingDragComp = null;

            m_isUpdateBuilding = false;
            m_nameBuilding = TypeNameBuilding.None;
        }

        // Thiết lập hiệu ứng gợi ý.
        // -------------------------
        private void SetMaterialGhost(TypeNameBuilding nameBuilding, Material ghost)
        {
            if (m_cachedRenderers.TryGetValue(nameBuilding, out MeshRenderer[] renderers))
            {
                foreach (var renderer in renderers)
                    renderer.material = ghost;
            }
        }

        // Đặt công trình tại vị trí nhấn chuột và chờ công nhân đến xây dựng.
        // ------------------------------------------------------------------
        private void HandleBuidlingObject()
        {
            var flyweightBuildingComp = m_buildingCurrent.GetComponent<BuildingFlyweightComp>();

            // Tiến hành xây công trình.
            var buildingRTS = FactorySystem.Instance.RaceFactory.
                FunGetBuildingRace(flyweightBuildingComp.FunGetNameBuilding(), flyweightBuildingComp.FunGetRaceBuilding(), flyweightBuildingComp.FunGetRaceRTS());

            // Lấy vị trí để đặt công trình xây dựng.
            Vector3 posPlaceable = m_buildingCurrent.GetComponent<BuildingDragComp>().FunGetPosPlaceableBuilding();
            buildingRTS.ObjectUnderConstruction.transform.position = new Vector3(posPlaceable.x, -0.52f, posPlaceable.z);
            
            buildingRTS.ObjectBuilding.SetActive(false);

            // Thêm thành phần xây dựng.
            buildingRTS.ObjectUnderConstruction.SetActive(true);
            var underConstructioncomp = buildingRTS.ObjectUnderConstruction.AddComponent<UnderConstructionComp>();
            underConstructioncomp.FunSetDataBuildingPlace(buildingRTS, posPlaceable);
        }

        private void AddDataCachedRenderers(TypeNameBuilding name, GameObject building)
        {
            MeshRenderer[] meshRenderers = building.GetComponentsInChildren<MeshRenderer>();
            if (m_cachedRenderers.ContainsKey(name) == false)
                m_cachedRenderers.Add(name, meshRenderers);
        }


        // ---------------------------------------------------------------------------------
        // HANDLE MESSAGE
        // ---------------
        // //////////////////////////////////////////////////////////////////////////////////

        // Nhận lệnh xây công trình mới.
        // ----------------------------
        private bool OnStartCreateBuilding(IMessage message)
        {
            var messageResult = message as MessageInitializeBuilding;
            if (m_mapHintBuilding.TryGetValue(messageResult.TypeNameBuiding, out Transform objBuilding) == false)
            {
                Debug.Log("Error, Could not find object for Type: " + messageResult.TypeNameBuiding);
                return false;
            }

            m_nameBuilding = messageResult.TypeNameBuiding;
            m_buildingCurrent = objBuilding.gameObject;

            m_BuildingPlaceComp = m_buildingCurrent.GetComponent<BuildingPlacebleComp>();
            m_buildingDragComp = m_buildingCurrent.GetComponent<BuildingDragComp>();
            m_buildingCurrent.transform.position = FunSnapToGrid(InputUtils.FunGetMouseWorldPosition());
            m_buildingCurrent.SetActive(true);

            m_isPlaceable = true;
            m_isUpdateBuilding = true;
            m_tilemapHint.gameObject.SetActive(true);
            return true;
        }
    }
}