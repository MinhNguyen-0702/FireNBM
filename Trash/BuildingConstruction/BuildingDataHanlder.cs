// using System;
// using System.Collections.Generic;
// using UnityEngine;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Lớp trợ giúp chứa các dữ liệu cần xây dựng.
//     /// </summary>
//     public class BuildingDataHanlder
//     {
//         private Vector3 m_posStartBuilding;
//         private Vector3 m_posEndBuilding;

//         private MessagingSystem m_messageManager;
//         private FormationSystem m_formationSystem;

//         private Dictionary<TypeNameBuilding, Action> m_mapUnderConstruction;
//         private Dictionary<TypeNameBuilding, Action> m_mapEndBuilding;
//         private Dictionary<TypeNameBuilding, Action> m_mapMemberBuilding;

//         private const int MEMBER_DEFAULT = 3;


//         // -------------------------------------------------------------------------------------------
//         //  CONSTRUCTOR
//         // ------------
//         // ////////////////////////////////////////////////////////////////////////////////////////////

//         public BuildingDataHanlder()
//         {
//             m_messageManager = MessagingSystem.Instance;

//             m_posStartBuilding = Vector3.zero;
//             m_mapUnderConstruction = new Dictionary<TypeNameBuilding, Action>
//             {
//                 { TypeNameBuilding.BuildingMage,   () => StartBuildingMage() },
//                 { TypeNameBuilding.BuildingWarror, () => StartBuildingWarror() },
//                 { TypeNameBuilding.CommandCenter,  () => StartBuildingCommandCenter() },
//             };

//             m_posEndBuilding = Vector3.zero;
//             m_mapEndBuilding = new Dictionary<TypeNameBuilding, Action>
//             {
//                 { TypeNameBuilding.BuildingMage,   () => EndBuildingMage() },
//                 { TypeNameBuilding.BuildingWarror, () => EndBuildingWarror() },
//                 { TypeNameBuilding.CommandCenter,  () => EndBuildingCommandCenter() },
//             };

//             // m_mapMemberBuilding = new Dictionary<TypeNameBuiding, Action>
//             // {
//             //     { TypeNameBuiding.BuildingMage,   () => CreateMemberMage() },
//             //     { TypeNameBuiding.BuildingWarror, () => CreateMemberWarror() },
//             //     { TypeNameBuiding.CommandCenter,  () => CreateMemberWorker() },
//             // };

//             // Nhận thông điệp chứa đối tượng mage khi sản sinh thành công.
//             // m_messageManager.FunAttachListener(typeof(MessageGetObjectMage), OnGetObjectMage);
//         }
        

//         // -------------------------------------------------------------------------------------------
//         // FUNCTION PUBLIC
//         // ---------------
//         // ////////////////////////////////////////////////////////////////////////////////////////////
 
//         /// <summary>
//         ///     Bắt đầu quá trình xây công trình. </summary>
//         /// ------------------------------------------------ 
//         public void FunHandlerStartBuilding(TypeNameBuilding typeNameBuilding, Vector3 pos)
//         {
//             m_posStartBuilding = pos;
//             if (m_mapUnderConstruction.ContainsKey(typeNameBuilding) == true)
//             {
//                 m_mapUnderConstruction[typeNameBuilding]();
//             }
//         }

//         /// <summary>
//         ///     Xử lý khi xây công trình thành công.</summary>
//         /// --------------------------------------------------
//         public void FunHandlerEndBuilding(TypeNameBuilding typeNameBuilding, Vector3 pos)
//         {
//             m_posEndBuilding = pos;
//             if (m_mapEndBuilding.ContainsKey(typeNameBuilding) == true)
//             {
//                 m_mapEndBuilding[typeNameBuilding]();
//             }
//         }

//         /// <summary>
//         ///     Xóa dữ liệu trước khi đối tượng bị hủy.</summary>
//         /// ----------------------------------------------------- 
//         public void FunShutdown()
//         {
//             m_mapUnderConstruction.Clear();
//             m_mapEndBuilding.Clear();
//             m_mapMemberBuilding.Clear();

//             m_messageManager.FunDetachListener(typeof(MessageGetObjectMage), OnGetObjectMage);
//         }


//         // --------------------------------------------------------------------------------------------
//         // FUNCTION HELPER
//         // ---------------
//         // ////////////////////////////////////////////////////////////////////////////////////////////

//         private void CreateMembersForBuilding(TypeNameBuilding typeNameBuiding)
//         {
//             // Tạo đội hình để quản lý các thành viên trong tháp.
//             // m_formationSystem.FunAddFormation(TypeActionUnitBase.Move, MEMBER_DEFAULT);

//             // Sản sinh các đối tượng tại vị trí hiện tại.
//             for (int i = 0; i < MEMBER_DEFAULT; ++i)
//             {
//                 m_mapMemberBuilding[typeNameBuiding]();
//             }

//             // Tạo vị trí xuất quân ngẫu nhiên
//             float distance = 8f;
//             Vector2 direction = UnityEngine.Random.insideUnitCircle.normalized;
//             Vector3 targetPos = m_posEndBuilding + new Vector3(direction.x * distance, 0f, direction.y * distance);

//             // Di chuyển đội hình.
//             m_formationSystem.FunUpdateFormCurrent(targetPos);
//         }

//         // For Mage
//         // --------
//         private void StartBuildingMage() =>
//             m_messageManager.FunTriggerMessage(new MessageUnderConstructorMage(m_posStartBuilding), false);

//         private void EndBuildingMage()
//         {
//             m_messageManager.FunTriggerMessage(new MessageBasicBuildingMageSpawn(m_posEndBuilding), false);
//             CreateMembersForBuilding(TypeNameBuilding.BuildingMage);
//         }

//         // private void CreateMemberMage() =>
//         //     m_messageManager.FunTriggerMessage(new MessageBasicMageSpawn(m_posEndBuilding), false);


//         // For Warror
//         // ----------
//         private void StartBuildingWarror() =>
//             m_messageManager.FunTriggerMessage(new MessageUnderConstructorMage(m_posStartBuilding), false);

//         private void EndBuildingWarror()
//         {
//             m_messageManager.FunTriggerMessage(new MessageBasicBuildingMageSpawn(m_posEndBuilding), false);
//             CreateMembersForBuilding(TypeNameBuilding.BuildingWarror);
//         }

//         // private void CreateMemberWarror() =>
//         //     m_messageManager.FunTriggerMessage(new MessageBasicMageSpawn(m_posEndBuilding), false);
    


//         // For Command Center
//         // ------------------
//         private void StartBuildingCommandCenter() => 
//             m_messageManager.FunTriggerMessage(new MessageUnderConstructorMage(m_posStartBuilding), false);

//         private void EndBuildingCommandCenter()
//         {
//             m_messageManager.FunTriggerMessage(new MessageBasicBuildingMageSpawn(m_posEndBuilding), false);
//             CreateMembersForBuilding(TypeNameBuilding.CommandCenter);
//         }

//         // private void CreateMemberWorker() =>
//         //     m_messageManager.FunTriggerMessage(new MessageBasicMageSpawn(m_posEndBuilding), false);




//         // ---------------------------------------------------------------------------------------------
//         // HANDLE MESSAGE
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////////////////

//         // Lấy đối tượng mage làm thành viên của nhóm.
//         private bool OnGetObjectMage(Message message)
//         {
//             // Thêm thành viên vào đội hình.
//             var messageResult = message as MessageGetObjectMage;
//             m_formationSystem.FunAddUnitToCurrentForm(messageResult.Mage);

//             // Thiết lập trạng thái di chuyển.
//             UnitStateComponent.FunChangeStateUnit(messageResult.Mage, TypeActionUnitBase.Move);

//             return true;
//         }

//         private bool OnGetObjectWarror(Message message) => true;
//         private bool OnGetObjectWorker(Message message) => true;
//     }
// }