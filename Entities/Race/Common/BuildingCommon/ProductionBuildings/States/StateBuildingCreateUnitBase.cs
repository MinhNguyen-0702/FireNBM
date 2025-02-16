using System;
using System.Collections;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class StateBuildingCreateUnitBase : IBuildingState
    {
        private float m_timeCreateUnitBase;
        private bool m_startCreateUnitBase;
        private BuildingControllerComp m_controller;

        private int m_countMineralsToCreateUnitBase;

        // --------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        //////////////////////////////////////////////////////////////////

        public StateBuildingCreateUnitBase(GameObject owner)
        {
            m_countMineralsToCreateUnitBase = 20;
            m_startCreateUnitBase = false;
            m_timeCreateUnitBase = 5f;
            m_controller = owner.GetComponent<BuildingControllerComp>();
        }
        

        // -----------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ////////////////////////////////////////////////////////////////////

        public Enum FunGetTypeState() => TypeRaceBuildingProductionBuildings.CreateUnitBase; 
        public void FunOnEnter() {}

        public void FunOnExit() 
        {
            m_startCreateUnitBase = false;
        }

        public void FunHandle()
        {
            if (CheckCanCreateUnitBase() == true)
                HandleUnitBase();
        }

        // ------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        //////////////////////////////////////////////////////////////////////

        // Kiểm tra xem công trình hiện tại có thể tạo được lính hay không.
        // ---------------------------------------------------------------
        private bool CheckCanCreateUnitBase()
        {
            if (m_startCreateUnitBase == false)
            {
                // Test Use Singleton.
                var resourceSupplys = ResourceMineralsManager.Instance.FunGetCurrResourceMinerals();
                if (resourceSupplys >= m_countMineralsToCreateUnitBase)
                {
                    m_startCreateUnitBase = true; 
                    MessagingSystem.Instance.FunTriggerMessage(new MessageUpdateResourceMineralsHUD(-m_countMineralsToCreateUnitBase), false);
                    return true;
                }
            }
            return false;
        }

        // Logic bắt đầu việc tạo lính.
        // ----------------------------
        private void HandleUnitBase()
        {
            m_controller.FunStartCoroutine(CreateUnitBase());
        }

        // Thực hiện việc tạo lính base trong khoảng thời gian chỉ định.
        // ---------------------------------------------------------------
        private IEnumerator CreateUnitBase()
        {
            yield return new WaitForSeconds(m_timeCreateUnitBase);
            
            var unitBase = UnitManager.Instance.FunSpawnUnit(TypeNameUnit.Warrior, TypeRaceUnit.BasicCombat, TypeRaceRTS.Terran);
            unitBase.transform.position = m_controller.FunGetUnitSpawnLocation(2f);
        }
    }
}