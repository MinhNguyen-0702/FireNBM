using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public abstract class BuildingRaceTypeFactory
    {
        private BuildingRaceDataSO m_dataCurrent;                          
        private Dictionary<TypeNameBuilding, BuildingRaceDataSO> m_dataMap; 


        // ------------------------------------------------------------------------------------------
        //  CONSTRUCTOR
        // ------------
        // ///////////////////////////////////////////////////////////////////////////////////////////

        public BuildingRaceTypeFactory()
        {
            m_dataMap = new Dictionary<TypeNameBuilding, BuildingRaceDataSO>();
            m_dataCurrent = null;
        }


        // --------------------------------------------------------------------------------
        // FUNSTION ABSTRACT
        // -----------------
        // /////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        ///     Phương thức trừu tượng để khởi tạo dữ liệu cụ thể cho loại building. </summary> 
        /// -----------------------------------------------------------------------------------
        protected abstract bool InitializeDataTypeBuilding(GameObject newUnit);



        // --------------------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Khởi tạo dữ liệu cho các building dựa trên danh sách đầu vào. </summary>
        /// ---------------------------------------------------------------------------- 
        public bool FunInitializeData(List<BuildingRaceDataSO> listBuildingDataSO)
        {
            MessagingSystem.Instance.FunAttachListener(typeof(MessageGetNewObject), OnGetObjectBuildingToInitBase);
            foreach (var buildingDataSO in listBuildingDataSO)
            {
                if (m_dataMap.ContainsKey(buildingDataSO.FlyweightData.NameBuilding) == true)
                {
                    Debug.LogWarning("Building đã tồn tại trong hệ thống.");
                    continue;
                }
                m_dataCurrent = buildingDataSO;
                m_dataMap.Add(buildingDataSO.FlyweightData.NameBuilding, buildingDataSO);

                PrefabPoolingSystem.FunPrespawn(buildingDataSO.BuildingPrefab, buildingDataSO.SizePoolBuilding);
                BuildingSystem.Instance.FunAddDataBuildingSystem(buildingDataSO);
            }
            MessagingSystem.Instance.FunDetachListener(typeof(MessageGetNewObject), OnGetObjectBuildingToInitBase);

            // Khởi tạo dữ liệu cho Under Construction.
            foreach (var buildingDataSO in listBuildingDataSO)
            {
                PrefabPoolingSystem.FunPrespawn(buildingDataSO.UnderConstructionPrefab, buildingDataSO.SizePoolUnderConstruction);
            }

            return true;
        }

        /// <summary>
        ///     Tạo một building dựa trên tên loại building. </summary>
        /// -----------------------------------------------------------
        public GameObject FunCreateBuilding(TypeNameBuilding name)
        {
            if (m_dataMap.TryGetValue(name, out var dataBuilding) == false)
                return null;

            var buildingSpawn = PrefabPoolingSystem.FunSpawn(dataBuilding.BuildingPrefab);
            var buildingData = buildingSpawn.GetComponent<BuildingDataComp>();
            buildingData.FunSetData(m_dataCurrent.Data);

            return buildingSpawn;
        }

        /// <summary>
        ///     Tạo một công trình đang xây dựng cho chủng tộc. </summary>
        /// -------------------------------------------------------------- 
        public GameObject FunCreateUnderConstruction(TypeNameBuilding name)
        {
            if (m_dataMap.TryGetValue(name, out var dataBuilding) == false)
                return null;
            
            return PrefabPoolingSystem.FunSpawn(dataBuilding.UnderConstructionPrefab);
        }

        /// <summary>
        ///     Hủy một building đã được tạo. </summary>
        /// --------------------------------------------
        public bool FunDisposeBuilding(GameObject building)
        {
            if (PrefabPoolingSystem.FunDespawn(building) == false)
            {
                Debug.LogWarning("Không thể hủy đối tượng building.");
                return false;
            }
            return true;
        }

        /// <summary>
        ///     Hủy một công trình building đang được xây dựng. </summary>
        /// ---------------------------------------------------------------
        public bool FunDisposeUnderConstruction(GameObject underConstruction)
        {
            if (PrefabPoolingSystem.FunDespawn(underConstruction) == false)
            {
                Debug.LogWarning("Không thể hủy đối tượng underConstruction.");
                return false;
            }
            return true;
        }


        // --------------------------------------------------------------------------------
        // FUNCTOR HELPER 
        // --------------
        ///////////////////////////////////////////////////////////////////////////////////

        /// Xử lý nhận object building từ hệ thống khởi tạo.
        // -------------------------------------------------
        private bool OnGetObjectBuildingToInitBase(IMessage message)
        {
            var messageResult = message as MessageGetNewObject;
            if (m_dataCurrent == null || m_dataCurrent.BuildingPrefab != messageResult.Prefab)
            {
                Debug.LogError("Dữ liệu hiện tại không khớp với prefab trong tin nhắn.");
                return false;
            }
            GameObject building = messageResult.NewObject;
            if (SetDataBuildingInitializeBase(building, messageResult.Prefab) == false)
            {
                DebugUtils.FunLog("Thiết lập dữ liệu cho building được tạo ra thất bại.");
                return false;
            }
            
            InitializeDataTypeBuilding(building);
            return true;
        } 

        // Thiết lập dữ liệu cơ bản cho một Building cần có.
        // ------------------------------------------------
        private bool SetDataBuildingInitializeBase(GameObject building, GameObject prefab)
        {
            var buildingActionComp = building.GetComponent<BuildingActionComp>();   
            buildingActionComp.FunSetActionData(m_dataCurrent.ActionData);
            
            var buildingFlyweightComp = building.AddComponent<BuildingFlyweightComp>();
            buildingFlyweightComp.FunSetDataFlyweight(m_dataCurrent.FlyweightData);

            // Thiết lập trạng thái rảnh.
            var BuildingStateComp = building.GetComponent<BuildingStateComp>();
            BuildingStateComp.FunRegisterState(new StateBuildingBaseFree(building));
            return true;
        }        
    }
}
