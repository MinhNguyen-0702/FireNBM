// using UnityEngine;
// using UnityEditor;
// using System.Collections.Generic;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Là một lớp chỉnh sửa giao diện trong Unity Editor, nhằm tùy chỉnh giao diện lớp LevelData.
//     /// </summary>
//     [CustomEditor(typeof(LevelData))]
//     public class LevelDataEditor : Editor
//     {

//         // --------------------------------------------------------------
//         // FUNCTOR HELPER
//         // --------------
//         //////////////////////////////////////////////////////////////////

//         public override void OnInspectorGUI()
//         {
//             // Lấy đối tượng cần chỉnh sửa.
//             LevelData levelData = (LevelData)target;
            
//             // Cập nhật enum chứa các loại đối tượng có trong cảnh 
//             // và số hàng và số cột.
//             AddLevelDetails(levelData);
//             // Cập nhật loại mà các đối tượng chọn.
//             AddLevelSlots(levelData);

//             // Thêm nút khởi tạo và cập nhật.
//             AddButtonInitialize(levelData);
//             AddButtonUpdate(levelData);
//         } 


//         // --------------------------------------------------------------
//         // FUNCTOR HELPER
//         // --------------
//         //////////////////////////////////////////////////////////////////
        

//         // Tạo các trường để cho người dùng nhập dữ liệu vào.
//         private void AddLevelDetails(LevelData levelData)
//         {
//             // Tạo ô nhập dữ liệu đầu vào liên quan đến LevelConfiguration.
//             levelData.Configuration = EditorGUILayout.ObjectField("Level", levelData.Configuration, 
//                 typeof(LevelConfiguration), false) as LevelConfiguration;

//             // Tạo thanh trượt để nhận giá trị cho số cột và hàng.
//             levelData.Rows = EditorGUILayout.IntSlider("Rows: ", levelData.Rows, 0, 25);
//             levelData.Columns = EditorGUILayout.IntSlider("Columns: ", levelData.Columns, 0, 25);
//         }

//         // Tạo mảng 2D và thêm các slot vào đấy.
//         private void AddLevelSlots(LevelData levelData)
//         {
//             // Tạo một dòng văn bản hiển thị.
//             EditorGUILayout.LabelField("Level Item per position: ");
//             for (int i = 0; i < levelData.Rows; ++i)
//             {
//                 // Với vòng lặp cột, ta hiển thị theo chiều ngang.
//                 GUILayout.BeginHorizontal();
//                 for (int j = 0; j < levelData.Columns; ++j)
//                 {
//                     LevelSlot slot = FindLevelSlot(levelData.Slots, i, j);
//                     // Tạo một danh sách thả xuống để chọn loại dữ liệu.
//                     slot.ItemType = (LevelItemType)EditorGUILayout.EnumPopup(slot.ItemType);
//                 }

//                 // Với danh sách cột đã được hiển thị, ta di chuyển xuống hàng mới.
//                 // Kết thúc hàng hiện tại.
//                 GUILayout.EndHorizontal();
//             }
//         }

//         // Tìm xem nguòi dùng đã đặt dũ liệu cho slot tại vị trí này ko.
//         private LevelSlot FindLevelSlot(List<LevelSlot> levelSlots, int x, int y)
//         {
//             // Tìm xem trong ô ý có chứa đối tượng không.
//             LevelSlot slot = levelSlots.Find(i => 
//                                             i.Coordinates.x == x && 
//                                             i.Coordinates.y == y);
//             // Tạo đối tượng mới nếu nó không tồn tại.
//             if (slot == null)
//             {
//                 // Đặt dữ liệu mặc định.
//                 slot = new LevelSlot(LevelItemType.None, new Vector2Int(x, y));
//                 // Thêm đối tượng mới vào danh sách.
//                 levelSlots.Add(slot);
//             }
//             return slot;
//         }

//         // Tạo một nút "Khởi tạo" với chức năng làm mới dữ liệu khi được nhấn.
//         private void AddButtonInitialize(LevelData levelData)
//         {
//             // Khởi tạo lại khi nút được ấn.
//             if (GUILayout.Button("Initialize"))
//             {
//                 Initialize(levelData);
//             }
//         }

//         // Một hàm hỗ chợ với chức năng làm mới dữ liệu.
//         private void Initialize(LevelData levelData)
//         {
//             // Xóa hết dữ liệu trước đó nếu có.
//             levelData.Slots.Clear();

//             // Thêm các đối tượng mới vào danh sách 
//             // dựa trên số hàng và cột đã được nhập.
//             for (int i = 0; i < levelData.Rows; ++i)
//             {
//                 for (int j = 0; j < levelData.Columns; ++j)
//                 {
//                     // Tạo đối tượng mới.
//                     LevelSlot levelSlot = 
//                         new LevelSlot(LevelItemType.None, new Vector2Int(i, j));
                    
//                     // Thêm chúng vào danh sách.
//                     levelData.Slots.Add(levelSlot);
//                 }
//             }
//         }

//         // Thêm một nút "Cập nhật" với chức năng lưu lại quá trình thay đổi.
//         private void AddButtonUpdate(LevelData levelData)
//         {
//             if (GUILayout.Button("Update"))
//             {
//                 // Thông báo nó đã thay đổi và chưa được lưu.
//                 EditorUtility.SetDirty(levelData);
//                 // Sẽ lưu vào ổ đĩa tất cả asset chưa được lưu.
//                 AssetDatabase.SaveAssets();
//                 // Báo cho Unity Editor biết và lưu chúng.
//                 AssetDatabase.Refresh();
//             }
//         }
//     }
// } 