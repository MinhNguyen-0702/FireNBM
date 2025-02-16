using System.Collections;
using FireNBM.Pattern;
using TMPro;
using UnityEngine;

namespace FireNBM
{
    public class EndGameHUD : MonoBehaviour
    {
        [SerializeField] private GameObject m_container;
        [Space(7)]
        [SerializeField] private Color m_colorFailed;
        [SerializeField] private Color m_colorVictory;
        [SerializeField] private TextMeshProUGUI m_textTitle;
        [Space(7)]
        [SerializeField] private TextMeshProUGUI m_textTimeLeft;
        [SerializeField] private TextMeshProUGUI m_textCountEnemy;
        [Space(7)]
        [SerializeField] private RectTransform m_rectTransform;

        private Vector2 m_posDisplay;


        private void Start()
        {
            m_container.SetActive(false);
            m_posDisplay = new Vector2(0f, 0f);
        }

        private void OnEnable()
        {
            MessagingSystem.Instance.FunAttachListener(typeof(MessageUpdateEndGame), OnUpdateEndGameHUD);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.FunDetachListener(typeof(MessageUpdateEndGame), OnUpdateEndGameHUD);
        }

        private IEnumerator DisplayResult(float delay)
        {
            Vector2 startPos = m_rectTransform.anchoredPosition;
            float timeCount = 0f;

            while (timeCount < delay)
            {
                timeCount += Time.deltaTime;
                float t = Mathf.Clamp01(timeCount / delay);  // Giữ giá trị từ 0 đến 1
                m_rectTransform.anchoredPosition = Vector2.Lerp(startPos, m_posDisplay, t);
                yield return null;
            }

            // Đảm bảo UI về đúng vị trí cuối cùng
            m_rectTransform.anchoredPosition = m_posDisplay;
        }

        private bool OnUpdateEndGameHUD(IMessage message)
        {
            var messageResult = message as MessageUpdateEndGame;
            if (messageResult.IsVictory == true)
            {
                m_textTitle.text = "VICTORY";
                // m_textTitle.color = m_colorVictory;
            }
            else 
            {
                m_textTitle.text = "FAILED";
                // m_textTitle.color = m_colorFailed;
            }

            m_textTimeLeft.text = messageResult.TimeLeft;
            m_textCountEnemy.text = messageResult.EnemyCount;

            m_container.SetActive(true);
            StartCoroutine(DisplayResult(0.7f));
            return true;
        }
    }
}