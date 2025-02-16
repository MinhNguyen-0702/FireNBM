// using UnityEngine;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Tạo ra cấp độ dựa trên file ScriptableObject được cấu hình.</summary>
//     public class LevelComponent : MonoBehaviour
//     {
//         [SerializeField] private GameObject m_plane;
//         [SerializeField] private LevelData m_levelData;

//         private void Start()
//         {
//             // // Báo lỗi nếu thành viên dữ liệu chưa được gắn.
//             // if (m_plane == null || m_levelData == null)
//             // {
//             //     Debug.LogError("Missing LevelData or Plane");
//             //     return;
//             // }

//             // // Lấy thành phần va chạm từ phane.
//             // Collider planeCollider = m_plane.GetComponent<Collider>();
//             // // Trả về kích thước gấp đối kích thước có trong cảnh.
//             // Vector3 planeSize = planeCollider.bounds.size; // (200f, 0f, 200f)

//             // // Lấy vị trí bắt đầu
//             // Vector3 startPos = new Vector3(-planeSize.x / 2, 0, planeSize.z / 2); // (-100f, 0f, 100f)

//             // // Kích thước chiều rộng và chiều dài của mỗi ô.
//             // float offsetX = planeSize.x / (m_levelData.Columns - 1); // 50
//             // float offsetZ = planeSize.z / (m_levelData.Rows - 1);

//             // // Khởi tạo các đối tượng vào cảnh.
//             // Initialize(startPos, offsetX, offsetZ);



//             // Lấy Mesh Collider của đối tượng
//             MeshCollider meshCollider = m_plane.GetComponent<MeshCollider>();

//             // Lấy kích thước của bounds
//             Vector3 colliderSize = meshCollider.bounds.size;

//             // Lấy tỉ lệ scale của đối tượng
//             Vector3 objectScale = m_plane.transform.localScale;

//             // Tính kích thước thực tế của đối tượng với scale
//             Vector3 actualSize = new Vector3(
//                 colliderSize.x * objectScale.x,
//                 colliderSize.y * objectScale.y,
//                 colliderSize.z * objectScale.z
//             );

//             // Hiển thị kích thước thực sự trong Console
//             Debug.Log("Kích thước thực sự của Mesh Collider: " + actualSize);
//         }

//         private void Initialize(Vector3 start, float offsetX, float offsetZ)
//         {
//             // Lặp qua danh sách slot.
//             foreach (LevelSlot levelSlot in m_levelData.Slots)
//             {
//                 // Lấy đối tượng bằng cách tìm kiếm dựa trên loại của chúng được gán.
//                 LevelItem levelItem = m_levelData.Configuration.FunFindByType(levelSlot.ItemType);
                
//                 // Bỏ qua nếu chúng không tồn tại
//                 if (levelItem == null)
//                 {
//                     continue;
//                 }

//                 // Lấy tọa độ 2D của ô.
//                 float x = start.x + (levelSlot.Coordinates.y * offsetX);  // 1; -75
//                 float z = start.z - (levelSlot.Coordinates.x * offsetZ);  // 1; 75

//                 // Tạo vị trí của đối tượng dựa trên tọa độ.
//                 Vector3 position = new Vector3(x, 0, z);
//                 // Tạo bản sao của đối tượng để chúng xuất hiện trong cảnh của chúng ta.
//                 Instantiate(levelItem.Prefab, position, Quaternion.identity, transform);
//             }
//         }
//     }
// }