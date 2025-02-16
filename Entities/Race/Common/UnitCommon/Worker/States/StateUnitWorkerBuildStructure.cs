using System;
using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Trạng thái xây công trình.
    /// </summary>
    public class StateUnitWorkerBuildStructure : IUnitState
    {
        private GameObject m_owner;
        private UnitDataComp m_data;                   
        private UnitControllerComp m_controller;        

        private int m_buildProgress;                      // Lượng công việc mà công nhân có thể làm trong một lần
        private bool m_hasAssignedBuilding;             // Tìm thấy công trình nào cần xây dựng ko.
        private bool m_isInBuildingRange;               // Đã đến nơi để xây dựng công trình chưa.

        private UnderConstructionComp m_underConstruction;
        private MessagingSystem m_messagingSystem;      


        // ---------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ////////////////////////////////////////////////////////////////////////

        public StateUnitWorkerBuildStructure(GameObject owner)
        {
            m_owner = owner;
            m_data = owner.GetComponent<UnitDataComp>();
            m_controller = owner.GetComponent<UnitControllerComp>();
            if (m_data == null || m_controller == null)
            {
                Debug.LogError("In BuildStructureState, " + 
                               "Error: Could not find 'UnitDataComponent' or 'UnitControllerComponent'.");
            }

            Reset();
            m_messagingSystem = MessagingSystem.Instance;
        }   


        // -----------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy tên kiểu trạng thái xây dựng công trình.</summary>
        /// ----------------------------------------------------------
        public Enum FunGetTypeState() => TypeRaceUnitWorker.BuildStructure;

        public void FunOnEnter()
        {
            m_messagingSystem.FunAttachListener(typeof(MessageBuildStructure), OnGetBuilding);
        }

        public void FunHandle()
        {
            if (m_hasAssignedBuilding == false)
                return;

            if (m_isInBuildingRange == true)
                HandleBuilding();
        }

        public void FunOnExit()
        {
            // Xóa thông điệp nếu chưa tìm được công trình mà chuyển trạng thái.
            if (m_hasAssignedBuilding == false)
                m_messagingSystem.FunDetachListener(typeof(MessageBuildStructure), OnGetBuilding);

            Reset();
            m_controller.FunResetDefaultController();
        }


        // --------------------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        ///////////////////////////////////////////////////////////////////////////////////
        
        // Thiết lập các hành động nếu đến công trình xây dựng.
        // ---------------------------------------------------
        private void UnderConstructionTarget()
        {
            m_isInBuildingRange = true;
            m_controller.FunSetMoving(false);                
            m_data.FunSetAnimState(TypeUnitAnimState.Worker);
        }

        // Bắt đầu xây dựng công trình.
        // ----------------------------
        private void HandleBuilding()
        {
            m_buildProgress++;
            if (m_buildProgress == ConstantFireNBM.MAX_BUILD_PROGRESS)
            {
                m_buildProgress = 0;
                m_underConstruction.FunUpdateResourceBuilding();
            }
        }


        // ---------------------------------------------------------------------------------
        // HANDLE MESSAGE
        // ---------------
        // //////////////////////////////////////////////////////////////////////////////////

        // Xử lý thông điệp: Lấy vị trí khi có builing mới.
        // ------------------------------------------------
        private bool OnGetBuilding(IMessage message)
        {
            var messageResult = message as MessageBuildStructure;

            m_underConstruction = messageResult.UnderConstruction;
            m_underConstruction.FunUnitWorkerStartBuid();
            m_underConstruction.FunAttachActionCreateBuildingSuccess(IsBuildingSuccess);

            m_controller.FunSetMoving(true);
            m_controller.FunMoveTo(m_underConstruction.FunGetPosUnderConstruction());
            m_controller.FunSetOnTriggerEnter(m_underConstruction.gameObject, UnderConstructionTarget);

            m_messagingSystem.FunDetachListenerLate(typeof(MessageBuildStructure), OnGetBuilding);
            m_hasAssignedBuilding = true;
            m_isInBuildingRange = false;
            return true;
        }

        // Xử lý thông điệp: dừng xây khi công trình hoàn thành.
        // -----------------------------------------------------
        private void IsBuildingSuccess()
        {
            var stateComp = m_owner.GetComponent<UnitStateComp>();
            stateComp.FunChangeState(TypeRaceUnitBase.Free);
        }

        private void Reset()
        {
            m_underConstruction = null;
            m_hasAssignedBuilding = false;
            m_isInBuildingRange = false;
        }
    }
}