using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    public class HealthManager
    {
        private Dictionary<GameObject, ObjectTypeBaseHealthComp> m_mapHealth;

        public HealthManager()
        {
            m_mapHealth = new Dictionary<GameObject, ObjectTypeBaseHealthComp>();
        }

        public void FunSetEnable(GameObject obj)
        {
            if (obj.tag.ToString() != ConstantFireNBM.UNIT && obj.tag.ToString() != ConstantFireNBM.ENEMY)
                return;

            if (m_mapHealth.ContainsKey(obj) == false)
            {
                var healthComp = obj.GetComponent<ObjectTypeBaseHealthComp>();
                if (healthComp != null)
                    m_mapHealth.Add(obj, healthComp);
            }

            m_mapHealth[obj].FunSetActiveHealth(true);
        }

        public void FunSetDisable(GameObject obj)
        {
            if (obj.tag.ToString() != ConstantFireNBM.UNIT && obj.tag.ToString() != ConstantFireNBM.ENEMY)
                return;

            if (m_mapHealth.ContainsKey(obj) == false)
            {
                var healthComp = obj.GetComponent<ObjectTypeBaseHealthComp>();
                if (healthComp != null)
                    m_mapHealth.Add(obj, healthComp);
            }

            m_mapHealth[obj].FunSetActiveHealth(false);
        }
    }
}