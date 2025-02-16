using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class InfoObjectManagerHUD : MonoBehaviour
    {
        [SerializeField] private InfoObjectHUD m_infoObject;

        private void Awake()
        {
            if (m_infoObject == null)
            {
                DebugUtils.FunLog("Lỗi thành phần InfoObjectHUD chưa được gằn vào.");
                return;
            }
        }

        private void OnEnable()
        {
            MessagingSystem.Instance.FunAttachListener(typeof(MessageDisplayInfoObjectHUD), OnDisplayInfoGameObjectInHUD);
            MessagingSystem.Instance.FunAttachListener(typeof(MessageDisableInfoObjectHUD), OnDisableInfoGameObjectInHUD);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.FunDetachListener(typeof(MessageDisplayInfoObjectHUD), OnDisplayInfoGameObjectInHUD);
            MessagingSystem.Instance.FunDetachListener(typeof(MessageDisableInfoObjectHUD), OnDisableInfoGameObjectInHUD);
        }

        private bool OnDisplayInfoGameObjectInHUD(IMessage message)
        {
            var messageResult = message as MessageDisplayInfoObjectHUD;
            m_infoObject.FunUpdateInfoObjectRTS(messageResult.ObjectDisplay, messageResult.TypeObject);
            m_infoObject.gameObject.SetActive(true);

            return true;
        }

        private bool OnDisableInfoGameObjectInHUD(IMessage message)
        {
            m_infoObject.gameObject.SetActive(false);
            return true;
        }
    }
}