using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class InfoObjectButtonManagerHUD : Singleton<InfoObjectButtonManagerHUD>
    {
        private List<InfoObjectButtonHUD> m_listButtonObject;
        private List<InfoObjectButtonHUD> m_listActive;
        public static InfoObjectButtonManagerHUD Instance { get { return InstanceSingletonInScene; } }


        // -----------------------------------------------------------------------------
        // API UNITY
        // ---------
        // //////////////////////////////////////////////////////////////////////////////

        protected override void Awake()
        {
            base.Awake();
            m_listButtonObject = new List<InfoObjectButtonHUD>();
            m_listActive = new List<InfoObjectButtonHUD>();
        }

        private void OnEnable()
        {
            MessagingSystem.Instance.FunAttachListener(typeof(MessageDisplayInfoButtonsDetails), OnDisplayObjectsDetails);
            MessagingSystem.Instance.FunAttachListener(typeof(MessageDisableInfoObjectsSelectorHUD), OnDisableListObjectSelector);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.FunDetachListener(typeof(MessageDisplayInfoButtonsDetails), OnDisplayObjectsDetails);
            MessagingSystem.Instance.FunDetachListener(typeof(MessageDisableInfoObjectsSelectorHUD), OnDisableListObjectSelector);
        }

        public void FunAddObjectButton(InfoObjectButtonHUD infoObj)
        {
            if (m_listButtonObject.Contains(infoObj) == false)
                m_listButtonObject.Add(infoObj);
        }

        public void FunDisableAllInfoObjSelector()
        {
            foreach (var selector in m_listActive)
                selector.gameObject.SetActive(false);
        }

        // -----------------------------------------------------------------------------
        // HANDLE MESSAGE
        // --------------
        // //////////////////////////////////////////////////////////////////////////////

        private bool OnDisplayObjectsDetails(IMessage message)
        {
            var messageResult = message as MessageDisplayInfoButtonsDetails;

            int countResult;
            int countButtonInfo = m_listButtonObject.Count;
            int countObjectSelector = messageResult.ListObjectSelector.Count;

            if (countButtonInfo >= countObjectSelector)
                countResult = countObjectSelector;
            else 
                countResult = countObjectSelector - countButtonInfo;
            
            for (int i = 0; i < countResult; ++i)
            {   
                m_listButtonObject[i].FunSetObjectSelector(messageResult.ListObjectSelector[i]);
                m_listButtonObject[i].gameObject.SetActive(true);
                m_listActive.Add(m_listButtonObject[i]);
            }
            return true;
        }

        private bool OnDisableListObjectSelector(IMessage message)
        {
            FunDisableAllInfoObjSelector();
            return true;
        }
    }
}