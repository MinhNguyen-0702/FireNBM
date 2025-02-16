using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Trạng thái di chuyển của đơn vị.
    ///     <para>Đại diện một giá trị từ enum: <see cref="TypeRaceUnitBase"/></para>
    /// </summary>
    public class StateUnitBaseMove : IUnitState
    {
        private Vector3 m_posMoveTo;
        private UnitDataComp m_data;
        private UnitControllerComp m_controller;


        // -----------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ////////////////////////////////////////////////////////////////////////////////

        public StateUnitBaseMove(GameObject owner)
        {
            m_data = owner.GetComponent<UnitDataComp>();
            m_controller = owner.GetComponent<UnitControllerComp>();
            if (m_data == null || m_controller == null)
            {
                Debug.Log("In MoveState: Unit Component or Unit Controller is missing!");
                return;
            }

            m_posMoveTo = Vector3.zero;
        }


        // ------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy tên kiểu của trạng thái move.</summary>
        /// -----------------------------------------------
        public Enum FunGetTypeState() => TypeRaceUnitBase.Move;
        public void FunOnEnter() {}

        public void FunOnExit() 
        {
            m_posMoveTo = Vector3.zero;
            m_controller.FunSetMoving(false);
            m_controller.FunResetDefaultController();
        }

        public void FunHandle()
        {    
            // Lấy vị trí cần di chuyển nếu có. 
            if (m_controller.NewDestination == true)
            {
                m_controller.NewDestination = false;
                m_controller.FunSetMoving(true);
                m_posMoveTo = m_controller.FunGetPosMouseClick();
                m_controller.FunMoveTo(m_posMoveTo);
            }

            if (m_controller.FunIsMoving() == true)
                MoveToTarget();
        }

        private void MoveToTarget()
        {
            if (m_controller.FunIsCloseToTarget(m_posMoveTo) == true)
            {
                m_controller.FunSetMoving(false);
                m_data.FunSetAnimState(TypeUnitAnimState.Idle);

                // if (m_controller.FunHasForm() == true)
                //     m_controller.FunNotifyFormationArrivedTarget();
            }
        }
    }
}
