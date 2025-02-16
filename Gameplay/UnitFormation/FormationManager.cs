using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Một lớp nhóm các unit được chọn để tạo thành 1 đội hình.
    /// </summary>
    public class FormationManager
    { 
        private int m_ID;                     // Định danh của đội hình.
        private IFormationPattern m_patter;    // Mẫu đội hình.
        private Location m_driftOffset;       // Độ lệch dùng để điều chỉnh vị trí nhân vật.
        private Location m_anchorPoint;       // Vị trí điểm neo (điểm trung tâm của đội hình).
        private int m_numberMemberToTarget;   // Số lượng thành viên đến đích.

        // Danh sách các vị trí đã được gán cho đối tượng.
        public Dictionary<GameObject, SlotAssignment> m_slotAssignments;    


        // --------------------------------------------------------------------
        // CONSTRUCTOR
        // ------------
        // ////////////////////////////////////////////////////////////////////

        public FormationManager(IFormationPattern pattern, int id)
        {
            m_ID = id;
            m_patter = pattern;
            m_slotAssignments = new Dictionary<GameObject, SlotAssignment>();

            m_driftOffset = new Location();
            m_anchorPoint = new Location();

            m_numberMemberToTarget = 0;
            MessagingSystem.Instance.
                FunAttachListener(typeof(MessageMemberAtTarget), OnMemberToTarget);
        }


        // --------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        // ///////////////////////////////////////////////////////////////

        /// <summary>
        ///     Đội hình đã di chuyển đến vị trí mục tiêu chưa.</summary>
        /// -------------------------------------------------------------
        public void FunUpdateFormation(Vector3 newAnchorPosition)
        {
            // Bỏ qua nếu vị trí mới là vị trí hiện tại.
            if (m_anchorPoint.Position == newAnchorPosition)
                return;

            UpdatePosAnchorPoint(newAnchorPosition);
            UpdateLocationTargetForMembers();
        }

        /// <summary>
        ///     Cập lại vị trí và hướng sau khi tất cả thành viên đến đích.</summary>
        /// -------------------------------------------------------------------------
        public void FunLateUpdateFormation()
        {
            UpdateLocationTargetForMembers(true);
            FunResetNumberMemberToTarget();
        }

        /// <summary>
        ///     Thiết lập mục tiêu mới, làm mới số lượng thành viên đến đích. </summary>
        /// ---------------------------------------------------------------------------- 
        public void FunResetNumberMemberToTarget() => m_numberMemberToTarget = 0;
 
        /// <summary>
        ///     Thêm một nhân vật mới vào đội hình.</summary>
        /// -------------------------------------------------
        public bool FunAddCharacter(GameObject character)
        {
            // Dừng lại khi nó đã ở trong đội hình.
            if (m_slotAssignments.ContainsKey(character) == true)
                return false;
            
            // Thoát nếu mẫu đội hình ko thêm được nhân vật mới.
            if (m_patter.FunSupportSlots(m_slotAssignments.Count + 1) == false)
                return false;

            // Thêm vị trí mới vào cập nhật lại vị trí đội hình.
            m_slotAssignments.Add(character, new SlotAssignment(character, -1));

            // Gắn ID của đội cho thành viên.
            var controllerComp = character.GetComponent<UnitControllerComp>();
            if (controllerComp == null)
            {
                Debug.LogError("In FormationManager, not add ID because member no have component: UnitControllerComponent!");
                return false;
            }

            // Cập nhật id của đội hình cho thành viên trong đội.
            // controllerComp.FunSetIdForm(m_ID);

            // Cập nhật lại vị trí đứng trong đội.
            UpdateSlotAssignment();
            return true;
        }

        /// <summary>
        ///     Xóa nhân vật khỏi vị trí của nó. </summary>
        /// -----------------------------------------------
        public void FunRemoveCharacter(GameObject character)
        {
            // Thoát nếu người cần xóa ko ở trong đội hình.
            if (m_slotAssignments.ContainsKey(character) == false)
                return;

            // Xóa nó khỏi vị trí và cập nhật lại vị trí đội hình.
            m_slotAssignments.Remove(character);
            UpdateSlotAssignment();
        }

        /// <summary> 
        ///     Lấy thông tin vị trí điểm neo của đội hình. </summary>
        /// ----------------------------------------------------------
        public Location FunGetAnchorPoint() => m_anchorPoint;

        /// <summary>
        ///     Lấy vị trí hiện tại đang di chuyển của đội hình.</summary>
        /// --------------------------------------------------------------
        public Vector3 FunGetPosCurrent() => m_slotAssignments.First().Key.transform.position;

        /// <summary> 
        ///     Điểm neo đã đến vị trí mục tiêu chưa.</summary>
        /// ---------------------------------------------------
        public bool FunIsAnchorToTarget(Vector3 posTarget)
        {
            return Vector3.Distance(m_anchorPoint.Position, posTarget) <= 0.5f;
        }

        /// <summary>
        ///     Kiểm tra xem tất cả các thành viên đã đến vị trí chỉ định của đội hình chưa. </summary>
        /// --------------------------------------------------------------------------------------------
        public bool FunIsAllMemberToTarget()
        {
            if (m_slotAssignments.Count == 0)
                return false;

            return m_numberMemberToTarget == m_slotAssignments.Count;
        }

        /// <summary>
        ///     Lấy số lượng thành viên di chuyển đến mục tiêu đã chỉ định. </summary>
        /// --------------------------------------------------------------------------
        public int FunGetNumberMemberToTarget() => m_numberMemberToTarget;

        /// <summary>
        ///     Lấy số lượng thành viên trong đội hình.</summary>
        /// -----------------------------------------------------
        public int FunGetCountMember() => m_slotAssignments.Count;

        /// <summary>
        ///     Lấy ID của đội hình.</summary>
        /// ----------------------------------
        public int FunGetID() => m_ID;

        /// <summary>
        ///     Lấy bản sao danh sách các thành viên trong đội hình.</summary>
        /// ------------------------------------------------------------------
        public HashSet<GameObject> FunGetUnitsInForm() => m_slotAssignments.Keys.ToHashSet(); 

        /// <summary>
        ///     Kiểm tra unit này có trong đội hình không. </summary>
        /// ---------------------------------------------------------
        public bool FunIsUnitInFormation(GameObject unit) => m_slotAssignments.ContainsKey(unit);


        // --------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        // ///////////////////////////////////////////////////////////////

        // Cập nhật điểm neo mới.
        private void UpdatePosAnchorPoint(Vector3 newAnchorPosition)
        {
            m_anchorPoint.Position = newAnchorPosition;
        }

        // Di chuyển tất cả thành viên.
        private void UpdateLocationTargetForMembers(bool isUpdateOrientation = false)
        {
            // Cập nhật vị trí đội hình mà thành viên cần di chuyển đến.
            foreach (SlotAssignment slot in m_slotAssignments.Values)
            {                
                // Lấy vị trí của nhân vật trong đội hình.
                Location slotLocation = m_patter.FunGetSlotLocation(slot.SlotNumber);

                // Biến đổi vị trí theo vị trí của điểm neo.
                Location newLocation = new Location();

                // Tính toán vị trí.
                newLocation.Position = m_anchorPoint.Position + slotLocation.Position;

                // Thêm thành phần drift (độ lệch)
                newLocation.Position -= m_driftOffset.Position;

                if (isUpdateOrientation == true)
                {
                    // Tính toán hướng từ điểm neo ra vị trí thành viên (quay ra ngoài).
                    // Vector3 directionOutward = (newLocation.Position - m_anchorPoint.Position).normalized;

                    // // Sử dụng Quaternion để tính góc quay cho hướng quay ra ngoài.
                    // newLocation.Orientation = Quaternion.LookRotation(directionOutward);
                }

                // Gán mục tiêu cho nhân vật. 
                slot.Character.GetComponent<UnitControllerComp>()
                    .FunSetPosMouseTarget(newLocation);
            }
        }

        // Cập nhật vị trí của đối tượng trong đội hình.
        private void UpdateSlotAssignment()
        {
            // Gán vị trí theo số thứ tự trong list.
            int count = -1;
            foreach (var key in m_slotAssignments.Keys.ToList())
            {
                m_slotAssignments[key].SlotNumber = ++count;
            }

            // Cập nhật độ lệch drift.
            m_driftOffset = m_patter.FunGetDriftOffset(m_slotAssignments);
        }
        

        // Thành viên trong đội báo đã đến đích.
        private bool OnMemberToTarget(IMessage message)
        {
            var messageResult = message as MessageMemberAtTarget;

            // Thoát nếu ko là thành viên trong đội ko.
            if (messageResult.IdForm != m_ID)
                return false;

            ++m_numberMemberToTarget;
            return true;
        }
    }
}