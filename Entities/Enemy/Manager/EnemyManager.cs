using System;
using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class EnemyManager : Singleton<EnemyManager>
    {
        [SerializeField] 
        private List<GameObject> m_posPlaceEnemyList = new List<GameObject>();
        private List<GameObject> m_listEnemy;
        public static EnemyManager Instance { get { return InstanceSingletonInScene; }}


        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            m_listEnemy = new List<GameObject>();

            // Thiết lập chế độ tuần tra cho các unit rồi hiển thị chúng.
            foreach (var posPlace in m_posPlaceEnemyList)
            {
                // Test
                GameObject enemy = UnitManager.Instance.FunSpawnUnit(TypeNameUnit.Golem, TypeRaceUnit.BasicCombat, TypeRaceRTS.Zerg);
                enemy.transform.position = posPlace.transform.GetChild(0).transform.position;
                var enemyComp = enemy.GetComponent<UnitEnemyComp>();
                enemyComp.FunSetDataEnemy(posPlace.transform.GetChild(1).transform.position);

                m_listEnemy.Add(enemy);
            }

            foreach (var posPlace in m_posPlaceEnemyList)
            {
                Destroy(posPlace);
            }
        }

        public List<GameObject> FunGetListEnemy() => m_listEnemy;
    }
}