using System;
using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    [Serializable]
    public class PortraitUnit 
    {
        public TypeNameUnit Name;
        public GameObject Unit;
    }

    public class PortraitManagerHUD : MonoBehaviour
    {
        [SerializeField] private List<PortraitUnit> m_listPortraitUnit = new List<PortraitUnit>();

        private GameObject m_portraitCurrent;
        private Dictionary<TypeNameUnit, GameObject> m_mapPortrait;


        private void Start()
        {
            m_mapPortrait = new Dictionary<TypeNameUnit, GameObject>();
            foreach (var unitPortrait in m_listPortraitUnit)
            {
                if (m_mapPortrait.ContainsKey(unitPortrait.Name) == false)
                {
                    m_mapPortrait.Add(unitPortrait.Name, unitPortrait.Unit);
                }
                unitPortrait.Unit.SetActive(false);
            }
        }

        private void OnEnable()
        {
            MessagingSystem.Instance.FunAttachListener(typeof(MessageDisplayPortaitObjectHUD), OnDisplayPortraitUnit);
            MessagingSystem.Instance.FunAttachListener(typeof(MessageDisablePortraitObjectHUD), OnDisablePortraitUnit);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.FunDetachListener(typeof(MessageDisplayPortaitObjectHUD), OnDisplayPortraitUnit);
            MessagingSystem.Instance.FunDetachListener(typeof(MessageDisablePortraitObjectHUD), OnDisablePortraitUnit);
        }

        private bool OnDisplayPortraitUnit(IMessage message)
        {
            var messageResutl = message as MessageDisplayPortaitObjectHUD;
            if (m_mapPortrait.ContainsKey(messageResutl.NameUnit) == false)
            {
                DebugUtils.FunLog("Không có Portrait cho đối tượng có tên: " + messageResutl.NameUnit);
                return false;
            }

            if (m_portraitCurrent != null)
                m_portraitCurrent.SetActive(false);
            
            m_portraitCurrent = m_mapPortrait[messageResutl.NameUnit];
            m_portraitCurrent.SetActive(true);
            return true;
        }

        private bool OnDisablePortraitUnit(IMessage message)
        {
            if (m_portraitCurrent != null)
                m_portraitCurrent.SetActive(false);
            
            m_portraitCurrent = null;   
            return true;
        }
    }
}