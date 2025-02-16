using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    public class UnitDeathComp : MonoBehaviour
    {
        private HashSet<Action> m_listAction;

        private void Awake()
        {
            m_listAction = new HashSet<Action>();
        }

        public void FunOnTriggerUnitDeath()
        {
            var stateComp = gameObject.GetComponent<UnitStateComp>();
            stateComp.FunChangeState(TypeRaceUnitBase.Free);
            stateComp.FunSetActive(false);

            gameObject.GetComponent<UnitHighlightComp>().FunDisableSelectedState();
            gameObject.GetComponent<UnitDataComp>().FunSetAnimState(TypeUnitAnimState.Death);
            gameObject.GetComponent<UnitControllerComp>().FunSetMoving(false);

            foreach (var action in m_listAction)
                action?.Invoke();
        
            m_listAction.Clear();
            StartCoroutine(HandleDeath());
        }

        public void FunAddAction(Action action)
        {
            if (m_listAction.Contains(action) == false)
                m_listAction.Add(action);
        }

        public void FunRemoveAction(Action action)
        {
            if (m_listAction.Contains(action) == false)
                return;
            
            m_listAction.Remove(action);
        }

        private IEnumerator HandleDeath()
        {
            yield return new WaitForSeconds(2.0f);
            UnitManager.Instance.FunDisableUnit(gameObject);
        }
    }
}