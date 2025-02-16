using System;
using System.Collections;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Lệnh tạo lính worker.
    /// </summary>
    public class StateBuildingCreateUnitWorker : IBuildingState
    {
        private float m_timeCreateUnitWorker;
        private bool m_startCreateUnitWorker;
        // private bool m_isUpdateCreateUnitWorker;

        private GameObject m_owner;
        // private Vector3 m_targetPos;
        // private BuildingStateComp m_state;
        private BuildingControllerComp m_controller;

        // private UnitDataComp m_unitData;
        // private UnitControllerComp m_constrollerUnit;


        // --------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        //////////////////////////////////////////////////////////////////

        public StateBuildingCreateUnitWorker(GameObject owner)
        {
            m_startCreateUnitWorker = false;
            // m_isUpdateCreateUnitWorker = false;
            m_timeCreateUnitWorker = 5f;

            m_owner = owner;
            m_controller = owner.GetComponent<BuildingControllerComp>();
            // m_state = owner.GetComponent<BuildingStateComp>();
        }
        

        // -----------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ////////////////////////////////////////////////////////////////////

        public Enum FunGetTypeState() => TypeRaceBuildingTownhall.CreateUnitWorker; 
        public void FunOnEnter() {}

        public void FunOnExit() 
        {
            m_startCreateUnitWorker = false;
        }

        public void FunHandle()
        {
            if (CheckCanCreateUnitWorker() == true)
                HandleUnitWorker();

            // if (m_isUpdateCreateUnitWorker == true)
            //     UpdateHandleUnitWorker();
        }


        // ------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        //////////////////////////////////////////////////////////////////////

        // Kiểm tra xem công trình hiện tại có thể tạo được lính hay không.
        // ---------------------------------------------------------------
        private bool CheckCanCreateUnitWorker()
        {
            if (m_startCreateUnitWorker == false)
            {
                m_startCreateUnitWorker = true; 
                return true;
            }
            return false;
        }

        // Logic bắt đầu việc tạo lính.
        // ----------------------------
        private void HandleUnitWorker()
        {
            m_controller.FunStartCoroutine(CreateUnitWorker());
        }

        // // Cập nhật việc đưa lính đã được tạo đến vị trí đã chỉ định.
        // // ----------------------------------------------------------
        // private void UpdateHandleUnitWorker()
        // {    
        //     DebugUtils.FunDrawLine(m_owner.transform.position, m_targetPos, Color.green);
        //     m_constrollerUnit.FunMoveToDefault(m_targetPos);
        //     if (m_constrollerUnit.FunIsCloseToTarget(m_targetPos) == true)
        //     {
        //         m_constrollerUnit.FunSetMoving(false);
        //         m_unitData.FunSetAnimState(TypeUnitAnimState.Idle);
        //         m_isUpdateCreateUnitWorker = false;
        //         m_state.FunChangeState(TypeRaceBuildingTownhall.Free);
        //     }
        // }

        // Thực hiện việc tạo lính worker trong khoảng thời gian chỉ định.
        // ---------------------------------------------------------------
        private IEnumerator CreateUnitWorker()
        {
            yield return new WaitForSeconds(m_timeCreateUnitWorker);
            
            var unitWorker = UnitManager.Instance.FunSpawnUnit(TypeNameUnit.Mage, TypeRaceUnit.Worker, TypeRaceRTS.Terran);
            unitWorker.transform.position = m_controller.FunGetUnitSpawnLocation(2f);

            // m_targetPos = ;
            // m_constrollerUnit.FunSetMoving(true);
            
            // Thiết lập vị trí nhà townhall cho unit worker.
            var workerComp = unitWorker.GetComponent<UnitWorkerComp>();
            workerComp.FunSetTownhall(m_owner);
            
            // m_unitData = unitWorker.GetComponent<UnitDataComp>();
            // m_constrollerUnit = unitWorker.GetComponent<UnitControllerComp>();
            // m_isUpdateCreateUnitWorker = true;
        }
    }
}