using System.Collections;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class StartGameHUD : Singleton<StartGameHUD>
    {
        [SerializeField] private RectTransform m_panelInfo; 
        public static StartGameHUD Instace { get { return InstanceSingletonInScene; } }

        protected override void Awake()
        {
            base.Awake();
        }

        public void FunHandleStartGame()
        {
            StartCoroutine(ClosePanel());
        }

        private IEnumerator ClosePanel()
        {
            float timeCount = 0f;
            float duration = 0.5f; // Di chuyển trong 3 giây
            Vector2 startPos = m_panelInfo.anchoredPosition;
            Vector2 targetPos = startPos + new Vector2(0, 800f); // Di chuyển lên trên 300px (có thể chỉnh)

            while (timeCount < duration)
            {
                timeCount += Time.deltaTime;
                float t = timeCount / duration;
                m_panelInfo.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
                yield return null;
            }

            m_panelInfo.anchoredPosition = targetPos; // Đảm bảo vị trí chính xác sau khi hoàn tất
            gameObject.SetActive(false);
            MessagingSystem.Instance.FunTriggerMessage(new MessageStartGame(), true);
        }
    }
}