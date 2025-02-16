using UnityEngine;
using UnityEngine.EventSystems;

namespace FireNBM
{
    public class ActionInfoButtonHUD : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject m_actionInfo; // Gán đối tượng chứa Text vào Inspector
        [SerializeField] private string m_textAction;
        [SerializeField] private string m_textInfo;
        [SerializeField] ActionInfoButtonBuyHUD m_actionBuy;

        private ActionInfoHUD m_actionInfoHUD;

        private void Start()
        {
            m_actionInfoHUD = m_actionInfo.GetComponent<ActionInfoHUD>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (m_actionInfoHUD == null)
            {
                DebugUtils.FunLog("m_actionInfoHUD is null");
                return;
            }
            m_actionInfoHUD.FunSetTextAction(m_textAction);
            m_actionInfoHUD.FunSetTextInfo(m_textInfo);
            m_actionInfo.SetActive(true); 

            if (m_actionBuy != null)
            {
                m_actionInfoHUD.FunSetCountActionBuy(m_actionBuy.FunGetCountMinerals(), 
                    m_actionBuy.FunGetCountVespeneGas(), m_actionBuy.FunGetCountSupplys());
            }
            else 
            {
                m_actionInfoHUD.FunDisableActionBuy();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            m_actionInfo.SetActive(false); 
        }
    }
}