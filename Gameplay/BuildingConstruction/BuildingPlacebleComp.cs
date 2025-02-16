using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Đại diện cho các đối tượng có thể đặt trên bản đồ trong hệ thông xây dựng.
    /// </summary>
    [AddComponentMenu("FireNBM/Building/Building Placeble Comp")]
    public class BuildingPlacebleComp : MonoBehaviour
    {
        public BoundsInt Area;          // Xác định khu vực mà đối tượng chiếm trong lưới.
        private Vector3Int m_size;      // Kích thước của đối tượng trong đơn vị lưới.
        private Vector3[] m_vertices;   // Một mảng Vector3 chứa vị trí đỉnh của đối tượng trong không gian cục bộ.
        

        // ----------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // ///////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            // Thiết lập cơ bản cho Building.
            GetColliderVertexPositionLocal();
            CalculateSizeInCells();
            InitializeArea(); 
        }


        // -----------------------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // ////////////////////////////////////////////////////////////////////////////////////

        public Vector3Int FunGetSize() => m_size;

        /// <summary>
        ///     Trả về vị trí bắt đầu của đối tượng trong không gian thế giới. </summary>
        /// -----------------------------------------------------------------------------
        public Vector3 FunGetStartPosition() => transform.TransformPoint(m_vertices[0]);
            

        /// <summary>
        ///     Đặt đối tượng là cố định sau khi đã xây xong. </summary>
        /// ------------------------------------------------------------
        public void FunPlace()
        {
            var dragComp = gameObject.GetComponent<BuildingDragComp>();
            if (dragComp != null) Destroy(dragComp);
        }


        // -----------------------------------------------------------------------------------
        // FUNCTION HELPER
        // ---------------
        // ////////////////////////////////////////////////////////////////////////////////////

        // Lấy vị trí các đỉnh của collider trong không gian cục bộ của đối tượng.
        // -----------------------------------------------------------------------
        private void GetColliderVertexPositionLocal()
        {
            // Lấy thành phần BoxCollider của đối tượng.
            BoxCollider box = gameObject.GetComponent<BoxCollider>();
            if (box == null)
            {
                Debug.LogError("In PlacedObjectComponent, Error: BoxCollider is null!");
                return;
            }

            m_vertices = new Vector3[4];

            // Lấy chiều rộng và dài của hộp (y trong trường hợp này ko cần thiết)
            float halfWidth = box.size.x * 0.5f;
            float halfHeight = box.size.z * 0.5f;
            Vector3 center = new Vector3(box.center.x, 0.1f, box.center.z);

            m_vertices[0] = center + new Vector3(-halfWidth, 0.0f, -halfHeight);   // Top right
            m_vertices[1] = center + new Vector3( halfWidth, 0.0f, -halfHeight);   // Top left
            m_vertices[2] = center + new Vector3( halfWidth, 0.0f,  halfHeight);   // Bottom left
            m_vertices[3] = center + new Vector3(-halfWidth, 0.0f,  halfHeight);   // Bottom right
        }

        // Tính toán kích thước của đối tượng trong đơn vị ô lưới.
        // -------------------------------------------------------
        private void CalculateSizeInCells()
        {
            // Tạo một mảng lưu trữ các đỉnh ô lưới
            Vector3Int[] tempVertices = new Vector3Int[m_vertices.Length];

            for (int i = 0; i < m_vertices.Length; ++i)
            {
                Vector3 worldPos = transform.TransformPoint(m_vertices[i]);
                Vector3Int gridPos = BuildingSystem.Instance.FunWorldToGridCell(worldPos);
                tempVertices[i] = gridPos; 
            }

            m_size = new Vector3Int(Mathf.Abs(tempVertices[0].x - tempVertices[1].x) + 1,
                                    Mathf.Abs(tempVertices[0].y - tempVertices[3].y) + 1,
                                    1); // Với lưới thì ko cần chiều sâu.
        }

        // Được sử dụng để kiểm tra diện tích mà mỗi đối tượng chiếm trong hệ thống xây dựng.
        // ----------------------------------------------------------------------------------
        private void InitializeArea()
        {
            Area = new BoundsInt(new Vector3Int(0, 0, 0), m_size);
        }
    }
}