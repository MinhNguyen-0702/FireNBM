using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Trạng thái tuần tra của đơn vị.
    ///     <para>Đại diện một giá trị từ enum: <see cref="TypeRaceUnitBase"/></para>
    /// </summary>
    public class StateUnitBasePatrol : IUnitState
    {
        // Owner
        private GameObject m_owner;
        private UnitDataComp m_data;
        private UnitControllerComp m_controller;

        // Patrol
        private Vector3 m_startPos;
        private Vector3 m_endPos;

        private bool m_isStartPatrol;
        private bool m_isPaused;

        // ------------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ///////////////////////////////////////////////////////////////////////////////////

        public StateUnitBasePatrol(GameObject owner)
        {
            m_owner = owner;
            m_data = owner.GetComponent<UnitDataComp>();
            m_controller = owner.GetComponent<UnitControllerComp>();
            if (m_data == null || m_controller == null)
            {
                Debug.LogError("In PatrolState: Error, data or controller is NULL!");
                return;
            }

            m_isPaused = true;         
            m_startPos = Vector3.zero;
            m_endPos = Vector3.one;
            m_isStartPatrol = true;
        }


        // --------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy tên kiểu của trạng thái Patrol.</summary>
        /// -------------------------------------------------
        public Enum FunGetTypeState() => TypeRaceUnitBase.Patrol;
        public void FunOnEnter() 
        {
            m_isPaused = true;
        }

        public void FunOnExit()
        {
            m_startPos = m_endPos = Vector3.one;      
            m_controller.FunResetDefaultController();
        }

        public void FunHandle()
        {
            // Kiểm tra nếu đơn vị có lệnh di chuyển
            if (m_controller.NewDestination)
            {
                m_isStartPatrol = false;
                m_controller.FunSetMoving(true);
                m_controller.NewDestination = false;
                m_startPos = m_owner.transform.position;
                m_endPos = m_controller.FunGetPosMouseClick();
            }

            // Nếu không có enemy, tiếp tục tuần tra
            if (m_controller.FunIsHaveEnemy() == false)
            {
                m_isPaused = false;
                m_controller.FunTryFindEnemy(); 
            }
            
            // Nếu có enemy, dừng tuần tra và tấn công
            if (m_controller.FunIsHaveEnemy())
            {
                m_isPaused = true;
                m_controller.FunHandleAttackEnemy();
                return; // Thoát khỏi hàm để không chạy tuần tra khi đang tấn công
            }

            // Nếu không có enemy, tiếp tục tuần tra
            if (m_isPaused == false && m_isStartPatrol == false)
            {
                PerformPatrol();
            }
        }



        // --------------------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        ///////////////////////////////////////////////////////////////////////////////////

        // Di chuyển theo lộ trình nếu ko có mục tiêu cần tấn công.
        // -------------------------------------------------------
        private void PerformPatrol()
        {
            m_controller.FunSetMoving(true);
            m_controller.FunMoveTo(m_endPos);

            // Nếu gần đến điểm tuần tra, kiểm tra enemy trước khi đổi hướng
            if (m_controller.FunIsCloseToTarget(m_endPos, 1.5f))
            {                
                // Trước khi đổi hướng, kiểm tra enemy lần nữa
                if (!m_controller.FunIsHaveEnemy())
                {
                    Vector3 temp = m_startPos;
                    m_startPos = m_endPos;
                    m_endPos = temp;
                    m_isPaused = false;
                }
                else
                {
                    m_isPaused = true;
                    m_controller.FunHandleAttackEnemy();
                }
            }
        }

        // // Tuần tra giữa các điểm đã chỉ định nếu có một mình.
        // // --------------------------------------------------
        // private IEnumerator PauseObserve()
        // {
        //     m_isPaused = true;
        //     m_data.FunSetAnimState(TypeUnitAnimState.Idle);

        //     float timeCount = 1f;
        //     while (timeCount >= 0.0f)
        //     {
        //         timeCount -= Time.deltaTime;
        //         if (m_controller.FunIsHaveEnemy() == true)
        //         {
        //             yield break;
        //         }   
        //         yield return null;
        //     }

        //     Vector3 temp = m_startPos;
        //     m_startPos = m_endPos;
        //     m_endPos = temp;
        //     m_isPaused = false;
        // }
    } 
}