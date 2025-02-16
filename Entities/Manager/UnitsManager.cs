using UnityEngine;
using FireNBM.Pattern;
using System.Collections.Generic;

namespace FireNBM
{
    /// <summary>
    ///     Quản lý các unit có trong cảnh.
    /// </summary>
    public class UnitManager : Singleton<UnitManager>
    {
        private RaceFactorySystem m_raceFactorySystem;
        public static UnitManager Instance{ get { return InstanceSingletonInScene; }}
        public static int CountName = 0;

        // ----------------------------------------------------------------------
        // API UNITY
        // ---------
        /////////////////////////////////////////////////////////////////////////

        protected override void Awake()
        {
            base.Awake();
            m_raceFactorySystem = FactorySystem.Instance.RaceFactory;
        }


        // --------------------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy tất cả các unit của một chủng tộc đang hoạt động trong scene.</summary>
        /// -------------------------------------------------------------------------------
        public List<GameObject> FunGetAllUnitActive(TypeRaceRTS race)
        {
            return m_raceFactorySystem.FunGetRaceFactory(race).FunGetUnitManagerFactory().FunGetListUnitActive();
        }   

        /// <summary>
        ///     Lấy danh sách các unit dựa theo loại công việc thuộc chủng tộc có trong scene. </summary>
        /// --------------------------------------------------------------------------------------------- 
        public List<GameObject> FunGetAllUnitRaceActive(TypeRaceUnit raceUnit, TypeRaceRTS race)
        {
            return m_raceFactorySystem.FunGetRaceFactory(race).FunGetUnitManagerFactory().FunGetListUnitRaceActive(raceUnit);
        }   

        /// <summary>
        ///     Sản sinh một đối tượng được lấy từ nhà máy Factory. </summary>
        /// ------------------------------------------------------------------
        public GameObject FunSpawnUnit(TypeNameUnit name, TypeRaceUnit raceUnit, TypeRaceRTS race)
        {
            GameObject newUnit =  m_raceFactorySystem.FunCreateUnitRace(name, raceUnit, race);
            newUnit.name += "_" + (CountName++).ToString();

            // Test
            if (race == TypeRaceRTS.Terran)
                MessagingSystem.Instance.FunTriggerMessage(new MessageUpdateResourceSupplysHUD(1), false);

            return newUnit;
        }

        /// <summary>
        ///     Thu hồi một đối tượng không còn được sử dụng nữa. </summary>
        /// ---------------------------------------------------------------- 
        public void FunDisableUnit(GameObject unit)
        {
            var flyweightComp = unit.GetComponent<UnitFlyweightComp>();
            m_raceFactorySystem.FunDisposeUnitRace(unit, flyweightComp.FunGetRaceUnit(), flyweightComp.FunGetRaceRTS());

            if (flyweightComp.FunGetRaceRTS() == TypeRaceRTS.Terran)
                MessagingSystem.Instance.FunTriggerMessage(new MessageUpdateResourceSupplysHUD(-1), false);
        }
    }
}