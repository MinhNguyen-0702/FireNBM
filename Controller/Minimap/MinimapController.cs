using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace FireNBM.UI
{
    /// <summary>
    ///     Chịu trách nhiệm quản lý và hiện thị giao diện minimap trong game rts.
    /// </summary>
    [AddComponentMenu("FireNBM/Controller/Minimap/Minimap Controller Comp")]
    public class MinimapControllerComp : MonoBehaviour
    {
        [SerializeField] private GameObject m_imageIcon;            // Icon đại diện cho unit trên minimap.
        [SerializeField] private RectTransform m_panel;             // Panel chứa tất cả các biểu tượng (icon) của đơn vị trên minimap.
        [SerializeField] private RawImage m_fowImage;               // Hình ảnh đại hiện cho fog of war trên minimap.


        private float m_timeCount;                                  // Biến đếm thời gian.
        private readonly float TIME_UPDATE = 0.5f;                  // Thời gian định kỳ để cập nhật.

        private float m_mapMainSize;                                // Kích thước của map chính.
        private float m_final2DScale;                               // Tỷ lệ thu nhỏ giữa bản đồ chính và minimap, giúp chuyển đổi tọa độ.

        private Dictionary<GameObject, Image> m_unitsIcons;         // Chứa danh sách các đơn vị unit và biểu tượng tương ứng (Image) trên minimap.


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_unitsIcons = new Dictionary<GameObject, Image>();
        }

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void Update()
        {
            if (m_timeCount > 0f)
            {
                m_timeCount -= Time.deltaTime;
                return;
            }

            m_timeCount = TIME_UPDATE;
            RecalculateMinimap();
        }


        // -----------------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // //////////////////////////////////////////////////////////////////////////////

        // Cập nhật lại vị trí biểu tượng của các đơn vị trên minimap.
        private void RecalculateMinimap()
        {
            foreach (KeyValuePair<GameObject, Image> entry in m_unitsIcons)
            {

            }

            // // Loại bỏ các mục không hợp lệ (đơn vị đã null) tránh lãng phí tài nguyên.
            // m_unitsIcons = (from icon in m_unitsIcons
            //                 where (icon.key != null)
            //                 select icon).T

        }
        
        
        /// <summary>
        ///     Tính toán vị trí của một đơn vị trên minimap. </summary>
        /// -----------------------------------------------------------------
        public Vector2 FunGetUnitInMapPoint()
        {
            return new Vector2();
        }

        /// <summary>
        ///     Chuyển đổi tọa độ 3D trong game thành tọa độ 2D trên bản đồ minimap. </summary>
        /// ----------------------------------------------------------------------------------- 
        public Vector2 FunGetPosInMapCoords(Vector3 position)
        {
            return new Vector2();
        }


        // -----------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // //////////////////////////////////////////////////////////////////////////////




        // -----------------------------------------------------------------------------
        // HANDLE MESSAGE
        // --------------
        // //////////////////////////////////////////////////////////////////////////////



    }
}