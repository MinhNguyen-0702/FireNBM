using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Một lớp tiện ích để xử lý đội hình di chuyển lặp lại một tuyến đường.
    /// </summary>
    public class FormationRepeat
    {
        public Vector3 PosCurrent;          // Lưu trữ vị trí hiện tại của đội hình.
        public Vector3 PosTarget;           // Lưu trữ ví trí cần đến của đội hình.
        public FormationManager Formation;  // Đội hình chứa các unit.

        public FormationRepeat(FormationManager formation)
        {
            PosCurrent = Vector3.zero;
            PosTarget = Vector3.one;
            Formation = formation;
        }

        public FormationRepeat() : this(null) {}
    }

    /// <summary>
    ///     Hệ thống quản lý các đội hình unit.
    /// </summary>
    [AddComponentMenu("FireNBM/System/Formation System")]
    public class FormationSystem : MonoBehaviour
    {    
        // private bool m_isRepeat;                 // Cho biết đội hình tại có ở chế độ lặp lại ko.
        // private bool m_isCurrentQuickSelect;     // Cho biết liệu đội hình hiện tái có ở chế độ chọn nhanh ko.
        // private FormationRepeat m_formCurrent;   // Lưu trữ đội hình hiện tại.     

        // private static int CREATE_ID_FORM = 0;   // Tạo ID cho đội hình.
        // private const int MIN_UNIT_COUNT = 2;    // Số lượng unit tối thiểu để tạo ra đội hình.

        private Dictionary<int, FormationManager> m_formsToTarget;   // Lưu trữ các đội hình đi đến mục tiêu.
        private Dictionary<int, FormationRepeat> m_formsRepeat;      // Lưu trữ các đội hình có cơ chế di chuyển lặp lại.

        private Dictionary<GameObject, int> m_mapUnitToID;           // Cho unit unit thuộc về đội nào.
        // private FormationPatternHandler m_formationPatternHandler;

        private MessagingSystem m_messagingSystem;


        // ---------------------------------------------------------------------------
        // API UNITY
        // ---------
        // ///////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            // m_isRepeat = false;
            // m_formCurrent = null;
            // m_isCurrentQuickSelect = false;

            m_mapUnitToID = new Dictionary<GameObject, int>();
            m_formsRepeat = new Dictionary<int, FormationRepeat>();
            m_formsToTarget = new Dictionary<int, FormationManager>();
            // m_formationPatternHandler = new FormationPatternHandler();

            m_messagingSystem = MessagingSystem.Instance;
            if (m_messagingSystem == null)
            {
                Debug.Log("In FormationSystem, m_messagingSystem = null");
                return;
            }
        }

        private void OnEnable()
        {
            // Đăng ký thông điệp để xử lý các trường hợp
            // m_messagingSystem.FunAttachListener(typeof(MessageStopSelectedUnit), OnHandleStopSelectedUnit);
            m_messagingSystem.FunAttachListener(typeof(MessageUpdateFormCurrMoveTarget), OnFormCurrentMoveToTarget);
            m_messagingSystem.FunAttachListener(typeof(MessageUpdateFormCurrState), OnUpdateStateFormCurrent);
            // m_messagingSystem.FunAttachListener(typeof(MessageGetFormUnit), OnGetFormationUnit);
        }

        private void OnDisable()
        {
            // Hủy các nhận thông điệp đã đăng ký.
            // m_messagingSystem.FunDetachListener(typeof(MessageStopSelectedUnit), OnHandleStopSelectedUnit);
            m_messagingSystem.FunDetachListener(typeof(MessageUpdateFormCurrMoveTarget), OnFormCurrentMoveToTarget);
            m_messagingSystem.FunDetachListener(typeof(MessageUpdateFormCurrState), OnUpdateStateFormCurrent);
            // m_messagingSystem.FunDetachListener(typeof(MessageGetFormUnit), OnGetFormationUnit);
        }

        // private void LateUpdate()
        // {
        //     UpdateFormToTarget();
        //     UpdateFormsRepeat();
        // }


        // ---------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        // ////////////////////////////////////////////////////////////////////////////

        // /// <summary>
        // ///     Chỉ được gọi khi cần đi đến mục tiêu mới.</summary>
        // /// -------------------------------------------------------
        // public void FunUpdateFormCurrent(Vector3 posTarget)
        // {
        //     if (m_formCurrent == null)
        //         return;

        //     // Cập nhật vị trí đường đi nếu đội hình hiện tại đang ở chế độ tuần tra.
        //     if (m_isRepeat == true)
        //     {
        //         m_formCurrent.PosCurrent = m_formCurrent.Formation.FunGetPosCurrent();
        //         m_formCurrent.PosTarget = posTarget;
        //     }
            
        //     // Di chuyển đến mục tiêu.
        //     m_formCurrent.Formation.FunUpdateFormation(posTarget);             
        // }
    
        // /// <summary>
        // ///     Tạo một đội hình mới quản lý các unit đã được chọn.</summary>
        // /// ----------------------------------------------------------------- 
        // public void FunCreateFormation(Enum action, int countUnit)
        // {
        //     if (countUnit < MIN_UNIT_COUNT) 
        //         return;

        //     // Lấy bố trí đội hình được gửi từ lệnh được nhấn.
        //     var formationPattern = GetFormationPattern(action);

        //     m_formCurrent = new FormationRepeat();
        //     m_formCurrent.Formation = new FormationManager(formationPattern, ++CREATE_ID_FORM);
        // }

        // / <summary>
        // ///     Thêm unit mới vào đội hình hiện tại.</summary>
        // /// -------------------------------------------------- 
        // public void FunAddUnitToCurrentForm(GameObject unit)
        // {
        //     if (m_formCurrent == null)
        //     {
        //         Debug.LogError("Error, in FormationSystem, m_formationCurrent is Null.");
        //         return;
        //     }

        //     // Kiểm tra xem unit này đã ở trong đội nào chưa.
        //     if (m_mapUnitToID.TryGetValue(unit, out int id) == true)
        //     {
        //         // Xóa unit trong danh sách cũ.
        //         if (m_formsToTarget.TryGetValue(id, out FormationManager formation) == true)
        //         {
        //             formation.FunRemoveCharacter(unit);

        //             // Xóa đội nếu trong đội đó ko còn thành viên.
        //             if (formation.FunGetCountMember() == 0) 
        //                 m_formsToTarget.Remove(id);

        //         }
        //         else if (m_formsRepeat.TryGetValue(id, out FormationRepeat formRepeat) == true)
        //         {
        //             formRepeat.Formation.FunRemoveCharacter(unit);

        //             // Xóa đội nếu trong đội đó ko còn thành viên.
        //             if (formRepeat.Formation.FunGetCountMember() == 0)
        //                 m_formsRepeat.Remove(id);
        //         }
        //         m_mapUnitToID.Remove(unit);
        //     }

        //     // Thêm unit mới vào đội hình.
        //     m_formCurrent.Formation.FunAddCharacter(unit);
        //     m_mapUnitToID.Add(unit, m_formCurrent.Formation.FunGetID());
        // }

        // /// <summary>
        // ///     Lấy đội hình hiện tại để thêm unit mới vào đội hình.</summary>
        // /// ------------------------------------------------------------------
        // public FormationManager FunGetFormationCurrent()
        // {
        //     if (m_formCurrent == null)
        //         return null;
            
        //     return m_formCurrent.Formation;
        // }

        // /// <summary>
        // ///     Thiết lập đội hình hiện tại và cho biết đội hình này có là đội hình cũ hay ko.</summary>
        // /// --------------------------------------------------------------------------------------------
        // public void FunSetFormationCurrent(FormationRepeat formation, bool isQuickSelecter)
        // {
        //     m_isCurrentQuickSelect = isQuickSelecter;
        //     m_formCurrent = formation;
        // }

        // /// <summary>
        // ///     Thiết lập đội hình hiện tại dựa trên id của đội.</summary>
        // /// --------------------------------------------------------------
        // public void FunSetFormationCurrent(int id, bool isQuickSelecter)
        // {
        //     m_isCurrentQuickSelect = isQuickSelecter;

        //     // Lấy đội hình qua id được gửi làm đội hình hiện tại.
        //     if (m_formsToTarget.TryGetValue(id, out FormationManager formation) == true)
        //     {
        //         var form = new FormationRepeat();
        //         form.Formation = formation;
        //         m_formCurrent = form;
        //     }
        //     else if (m_formsRepeat.TryGetValue(id, out FormationRepeat formRepeat) == true)
        //     {
        //         m_formCurrent = formRepeat;   
        //     }
        // }

        // /// <summary>
        // ///     Được dùng để cập nhật đội hình tuần tra hay đến mục tiêu khi người chơi
        // ///     vẫn chọn đội ý nhưng chỉ thay đổi lệnh.</summary>
        // /// --------------------------------------------------------------------------
        // public void FunSetModeCurrent(Enum action)
        // {
        //     // if ((TypeActionUnitBase)action != TypeActionUnitBase.Patrol)
        //     //     m_isRepeat = false;
        //     // else
        //     //     m_isRepeat = true;
        // }

        /// <summary>
        ///     Lấy đội hình mà unit thuộc về.</summary>
        /// --------------------------------------------
        public FormationManager FunGetFormationUnit(GameObject unit)
        {
            // Null, nếu unit này ko ở trong đội hình.
            if (m_mapUnitToID.TryGetValue(unit, out int id) == false)
                return null;

            // Tìm kiếm trong đội có ko.
            if (m_formsToTarget.TryGetValue(id, out FormationManager formation) == true)
            {
                return formation;
            }
            else if (m_formsRepeat.TryGetValue(id, out FormationRepeat formRepeat) == true)
            {
                return formRepeat.Formation;
            }

            // Nếu đội đó là đội hiện tại và chưa được thêm vào danh sách.
            return null;
        }


        // -----------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////

        // // Cập nhật đội hình di chuyển đến mục tiêu.
        // private void UpdateFormToTarget()
        // {
        //     // Thoát nếu hiện tại đội hình đang ở chế độ tuần tra.
        //     if (m_isRepeat == true || m_formCurrent == null)
        //         return;

        //     // Cập nhật lại đội hình nếu tất cả thành viên đến đích.
        //     if (m_formCurrent.Formation.FunIsAllMemberToTarget() == true)
        //     {
        //         m_formCurrent.Formation.FunLateUpdateFormation();
        //     }
        // }

        // // Cập nhật các đội hình có cơ chế lặp lại.
        // // --------------------------------------
        // private void UpdateFormsRepeat()
        // {
        //     // Xử lý nếu đội hình hiện tại ở cở chế tuần tra.
        //     if (m_isRepeat == true && m_formCurrent != null)
        //         HandleFormToRepeat(m_formCurrent);

        //     // Cập nhật các đội hình có trong danh sách.
        //     foreach (var formationRepeat in m_formsRepeat.Values)
        //     {                
        //         // Đội hình hiện tại đã được cập nhật, ko cần cập nhật lại.
        //         // Được dùng trong trường hợp cập nhật đội hình cũ.
        //         if (m_formCurrent == null || formationRepeat.Formation.FunGetID() != m_formCurrent.Formation.FunGetID())
        //             HandleFormToRepeat(formationRepeat);
        //     }
        // }

        // // Xử lý đội hình lặp lại.
        // // -----------------------
        // private void HandleFormToRepeat(FormationRepeat formRepeat)
        // {
        //     // Quay nhật lại nếu đội hình đi đến cuối đường tuần tra.
        //     if (formRepeat.Formation.FunIsAllMemberToTarget() == true)
        //     {
        //         formRepeat.Formation.FunResetNumberMemberToTarget();            

        //         // Hoán đổi vị trí mục tiêu.
        //         Vector3 temp = formRepeat.PosCurrent;
        //         formRepeat.PosCurrent = formRepeat.PosTarget;
        //         formRepeat.PosTarget = temp;

        //         // Cập nhật lại lộ trình.
        //         formRepeat.Formation.FunUpdateFormation(formRepeat.PosTarget);
        //     }
        // }

        // // Tạo các đội hình từ các lệnh tương ứng.
        // private IFormationPattern GetFormationPattern(Enum typeAction)
        // {
        //     var resultFormPattern = m_formationPatternHandler.FunCreate(typeAction);
        //     m_isRepeat = resultFormPattern.IsRepeat;

        //     return resultFormPattern.Pattern;
        // }


        // -----------------------------------------------------------------------------
        // HANDLE MESSAGE
        // --------------
        // //////////////////////////////////////////////////////////////////////////////

        // // Thêm đội hình vào danh sách khi người chơi ko sử dụng đội này nữa.
        // private bool OnHandleStopSelectedUnit(IMessage message)
        // {
        //     // Thoát nếu chưa có đội hình mới.
        //     if (m_formCurrent == null)
        //         return false;

        //     // Nếu hiện tại hệ thống sử dụng đội hình cũ.
        //     if (m_isCurrentQuickSelect == true)
        //     {
        //         int id = m_formCurrent.Formation.FunGetID();

        //         // Thử tìm trong đội hình đến mục tiêu.
        //         if (m_isRepeat == true && m_formsToTarget.ContainsKey(id) == true)
        //         {
        //             m_formsToTarget.Remove(id); // Xóa nếu tuần tra.
        //         }
        //         else if (m_isRepeat == false && m_formsRepeat.ContainsKey(id) == true)
        //         {
        //             m_formsRepeat.Remove(id); // Xóa nếu di chuyển đến mục tiêu.
        //         }
        //     }

        //     // Thêm đội hình mới vào danh sách tương ứng.
        //     int currentID = m_formCurrent.Formation.FunGetID();
        //     if (m_isRepeat == false && m_formsToTarget.ContainsKey(currentID) == false)
        //     {
        //         // Thêm khi di chuyển.
        //         m_formsToTarget.Add(currentID, m_formCurrent.Formation);
        //     }
        //     else if (m_isRepeat == true && m_formsRepeat.ContainsKey(currentID) == false)
        //     {
        //         // Thêm khi tuần tra.
        //         m_formsRepeat.Add(currentID, m_formCurrent);
        //     }

        //     // Làm mới dữ liệu
        //     m_formCurrent = null;
        //     m_isCurrentQuickSelect = false;
        //     return true;
        // } 

        // Di chuyển unit hoặc đội hình hiện tại đến vị trí mới.
        private bool OnFormCurrentMoveToTarget(IMessage message)
        {
            var messageResult = message as MessageUpdateFormCurrMoveTarget;

            if (messageResult.SelectedUnits.Count > 1)
            {
                foreach (var unit in messageResult.SelectedUnits)
                {
                    unit.GetComponent<UnitControllerComp>().FunSetPosMouseTarget(new Location(messageResult.PosTarget));
                }
            }
            else 
                messageResult.SelectedUnits.First().GetComponent<UnitControllerComp>().
                    FunSetPosMouseTarget(new Location(messageResult.PosTarget));

            return true;
        }

        // Cập nhật trạng thái cho đội hình hiện tại.
        private bool OnUpdateStateFormCurrent(IMessage message)
        {
            var messageResult = message as MessageUpdateFormCurrState;

            // Nếu chỉ có 1 unit không tạo đội hình mà chỉ cập nhật trạng thái.
            if (messageResult.SelectedUnits.Count == 1)
            {
                // Debug.Log("Change State: " + messageResult.TypeActionUnit); 
                UnitStateComp.FunChangeStateUnit(messageResult.SelectedUnits.First(), messageResult.TypeActionUnit);
                return true;
            }
            else if (messageResult.SelectedUnits.Count > 1)
            {
                foreach (var unit in messageResult.SelectedUnits)
                {
                    UnitStateComp.FunChangeStateUnit(unit, messageResult.TypeActionUnit);
                }
            }

            // // Nếu là đội hình hiện tại.
            // if (messageResult.IsFormCurrent == true)  
            //     FunSetModeCurrent(messageResult.TypeActionUnit);
            // else 
            //     // Tạo đội hình mới nếu đang chọn unit mới hoặc muốn tạo đội mới.
            //     FunCreateFormation(messageResult.TypeActionUnit, messageResult.SelectedUnits.Count);


            // // Thêm unit vào đội hình nếu chưa có và cập nhật trạng thái.
            // foreach (GameObject unit in messageResult.SelectedUnits) 
            // {
            //     // Thêm nếu đội hình ko chứa unit này.
            //     if (m_formCurrent.Formation.FunIsUnitInFormation(unit) == false)
            //         FunAddUnitToCurrentForm(unit);

            //     // Thay đổi trạng thái cho unit trong đội hình.
            //     UnitStateComp.FunChangeStateUnit(unit, messageResult.TypeActionUnit);
            // }

            return true;
        }

        // // Gửi đội hình dựa trên unit làm đại diện đến nơi muốn nhận.
        // private bool OnGetFormationUnit(IMessage message)
        // {
        //     var messageResult = message as MessageGetFormUnit;
        //     m_messagingSystem.FunTriggerMessage(new MessageNeedGetFormUnit(FunGetFormationUnit(messageResult.Unit)), false);
        //     return true;
        // }
    }
}