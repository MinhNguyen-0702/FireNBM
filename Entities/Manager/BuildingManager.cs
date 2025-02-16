// using System.Collections.Generic;
// using UnityEngine;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Quản lý các công trình có trong cảnh.
//     /// </summary>
//     public class BuildingManager
//     {
//         // Danh sách các building hoạt động trong cảnh.
//         private HashSet<GameObject> m_buildingListPlayer;
//         private HashSet<GameObject> m_buildingListBotAI;

//         // Chỉ có một phiên bản duy nhất của lớp.
//         private static BuildingManager m_instance;
//         public static BuildingManager Instance
//         { 
//             get{ return m_instance ?? (m_instance = new BuildingManager()); }
//         }


//         // --------------------------------------------------------------------------------
//         // CONSTRUCTOR PRIVATE
//         // -------------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         private BuildingManager()
//         {
//             m_buildingListPlayer = new HashSet<GameObject>(50);
//             m_buildingListBotAI = new HashSet<GameObject>(50);
//         }


//         // --------------------------------------------------------------------------------
//         // FUNSTION PUBLIC
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Thêm công trình mới được sản sinh được tạo từ player hoặc botAI. </summary>
//         /// -------------------------------------------------------------------------------
//         public void FunAddBuildingActive(GameObject building, TypeController controller)
//         {
//             if (controller == TypeController.Player)
//                 m_buildingListPlayer.Add(building);
//             else 
//                 m_buildingListBotAI.Add(building);
//         }

//         /// <summary>
//         ///     Phá hủy công trình đã được tạo ra từ player hoặc botAI. </summary>
//         /// ----------------------------------------------------------------------
//         public void FunRemoveBuildingActive(GameObject building, TypeController controller)
//         {
//             if (controller == TypeController.Player)
//                 m_buildingListPlayer.Remove(building);
//             else 
//                 m_buildingListBotAI.Remove(building);
//         }

//         /// <summary>
//         ///     Lấy tất cả các building của người chơi hay BotAI đang hoạt động trong scene.</summary>
//         /// -------------------------------------------------------------------------------------------
//         public HashSet<GameObject> FunGetAllBuildingActive(TypeController controller)
//         {
//             return (controller == TypeController.Player)? m_buildingListPlayer : m_buildingListBotAI;
//         }
//     }
// }