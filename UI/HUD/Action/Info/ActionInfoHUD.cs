using TMPro;
using UnityEngine;

namespace FireNBM
{
    public class ActionInfoHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_textAction;
        [SerializeField] private TextMeshProUGUI m_textInfo;
        [SerializeField] private GameObject m_objActionInfoBuy;

        private ActionInfoBuyHUD m_actionInfoBuy;

        private void Awake()
        {
            m_actionInfoBuy = m_objActionInfoBuy.GetComponent<ActionInfoBuyHUD>();
            if (m_actionInfoBuy == null)
            {
                DebugUtils.FunLog("Lỗi action buy ko được thêm vào.");
                return;
            }
        }

        public void FunSetTextAction(string textAction)
        {
            m_textAction.text = textAction;
        }

        public void FunSetTextInfo(string info)
        {
            m_textInfo.text = info;
        }

        public void FunSetCountActionBuy(int minerals, int vespeneGas, int supply)
        {
            m_actionInfoBuy.FunSetResourceMinerals(minerals);
            m_actionInfoBuy.FunSetResourceVespeneGas(vespeneGas);
            m_actionInfoBuy.FunSetResourceSupplys(supply);
            m_objActionInfoBuy.SetActive(true);
        }

        public void FunDisableActionBuy()
        {
            m_objActionInfoBuy.SetActive(false);
        }
    }
}