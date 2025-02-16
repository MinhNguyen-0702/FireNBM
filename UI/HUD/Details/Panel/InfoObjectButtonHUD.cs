using FireNBM.Pattern;
using UnityEngine;
using UnityEngine.UI;

namespace FireNBM
{
    public class InfoObjectButtonHUD : MonoBehaviour
    {
        private GameObject m_objectSelector;
        private Image m_imageSelectorInfo;

        private void Awake()
        {
            m_imageSelectorInfo = GetComponent<Image>();
            if (m_imageSelectorInfo == null)
            {
                DebugUtils.FunLog("Lỗi đối tượng này không có thành phần Image.");
                return;
            }
            InfoObjectButtonManagerHUD.Instance.FunAddObjectButton(this);
            gameObject.SetActive(false);
        }

        public void FunOnClickButtonDetails()
        {
            MessagingSystem.Instance.FunTriggerMessage(new MessageDisplayObjectSelector(m_objectSelector), true);
            InfoObjectButtonManagerHUD.Instance.FunDisableAllInfoObjSelector();
        }

        public void FunSetObjectSelector(GameObject obj)
        {
            // For Unit
            var flyweightUnitComp = obj.GetComponent<UnitFlyweightComp>();
            m_imageSelectorInfo.sprite = flyweightUnitComp.FunGetAvartar();
            m_objectSelector = obj;
        }
    }
}