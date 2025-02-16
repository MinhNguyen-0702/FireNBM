using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Trạng thái bảo vệ căn cứ của đơn vị.
    ///     <para>Đại diện một giá trị từ enum: <see cref="TypeRaceUnitBase"/></para>
    /// </summary>
    public class StateUnitBaseHoldPosition : IUnitState
    {
        private UnitDataComp m_data;
        private UnitControllerComp m_controller;
        private bool m_isMoveToHoldPos;
        private Vector3 m_posHold;


        // ------------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ///////////////////////////////////////////////////////////////////////////////////
         
        public StateUnitBaseHoldPosition(GameObject owner)
        {
            m_data = owner.GetComponent<UnitDataComp>();
            m_controller = owner.GetComponent<UnitControllerComp>();

            if (m_data == null || m_controller == null)
            {
                Debug.LogError("In PatrolState: Error, data or controller is NULL!");
                return;
            }
            m_posHold = Vector3.zero;
            m_isMoveToHoldPos = true;
        }


        // --------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ////////////////////////////////////////////////////////////////////////////////////

        public Enum FunGetTypeState() => TypeRaceUnitBase.HoldPosition;
        public void FunOnEnter() {}

        public void FunOnExit()
        {
            m_isMoveToHoldPos = true;
            m_controller.FunResetDefaultController();
        }

        public void FunHandle()
        {
            // Nhận lệnh đến vị trí phòng thủ.
            if (m_controller.NewDestination == true)
            {
                m_isMoveToHoldPos = false;
                m_controller.NewDestination = false;
                m_posHold = m_controller.FunGetPosMouseClick();
            }

            // Di chuyển nếu chưa đến điểm phòng thủ.
            if (m_isMoveToHoldPos == false)
            {
                MoveToHoldPosition();
                return;
            }

            // Tìm enemy trong lúc di chuyển đến vị trí đặt.
            if (m_controller.FunIsHaveEnemy() == false)
            {
                m_controller.FunTryFindEnemy();
            }
            else // Nếu tìm thấy thì thực hiện tấn công.
            {
                m_controller.FunHandleAttackEnemy();
            }
        }


        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        // Di chuyển đến vị trí phòng thủ.
        // ------------------------------
        private void MoveToHoldPosition()
        {
            m_controller.FunSetMoving(true);
            m_controller.FunMoveTo(m_posHold);

            if (m_controller.FunIsCloseToTarget(m_posHold) == true)
            {
                m_isMoveToHoldPos = true;
                m_controller.FunSetMoving(false);
                m_data.FunSetAnimState(TypeUnitAnimState.Idle);
            }
        }
    }
}