using System;
using System.Collections;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Trạng thái tấn công của đơn vị.
    ///     <para>Đại diện một giá trị từ enum: <see cref="TypeRaceUnitBase"/></para>
    /// </summary>
    public class StateUnitBaseAttack : IUnitState
    {
        private UnitDataComp m_data;
        private UnitControllerComp m_controller;
        private Vector3 m_posMoveToPosPlace;
        private bool m_isMoveToPosPlace;


        // --------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        //////////////////////////////////////////////////////////////////

        public StateUnitBaseAttack(GameObject owner)
        {
            m_data = owner.GetComponent<UnitDataComp>();
            m_controller = owner.GetComponent<UnitControllerComp>();
            if (m_data == null || m_controller == null)
            {
                Debug.Log("In AttackState: Error, Unit Component or Unit Controller is missing!");
                return;
            }
            m_posMoveToPosPlace = Vector3.zero;
            m_isMoveToPosPlace = true;
        }


        // -----------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy tên kiểu trạng thái tấn công.</summary>
        /// -----------------------------------------------
        public Enum FunGetTypeState() => TypeRaceUnitBase.Attack; 
        public void FunOnEnter() 
        {
            m_isMoveToPosPlace = true;
        }

        public void FunOnExit() 
        {
            m_isMoveToPosPlace = false;
            m_posMoveToPosPlace = Vector3.zero;
            m_controller.FunResetDefaultController();
        }

        public void FunHandle()
        {
            // Lấy vị trí cần di chuyển đến.
            if (m_controller.NewDestination == true)
            {
                m_isMoveToPosPlace = false;
                m_controller.NewDestination = false;
                m_posMoveToPosPlace = m_controller.FunGetPosMouseClick();
            }

            // Di chuyển đến ví trí chỉ định nếu không có enemy.
            if (m_isMoveToPosPlace == false && 
                m_controller.FunIsHaveEnemy() == false)
            {
                TryMoveToPosPlace();
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


        // ------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        //////////////////////////////////////////////////////////////////////

        // Di chuyển đến vị trí đã dánh dấu tấn công.
        // ------------------------------------------
        private void TryMoveToPosPlace()
        {
            m_controller.FunSetMoving(true);
            m_controller.FunMoveTo(m_posMoveToPosPlace);

            if (m_controller.FunIsCloseToTarget(m_posMoveToPosPlace) == true)
            {
                m_isMoveToPosPlace = true;
                m_controller.FunSetMoving(false);
                m_data.FunSetAnimState(TypeUnitAnimState.Idle);
            }
        }
    }
}