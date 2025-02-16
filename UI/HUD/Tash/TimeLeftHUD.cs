using FireNBM.Pattern;
using TMPro;
using UnityEngine;

namespace FireNBM
{
    public class TimeLeftHUD : Singleton<TimeLeftHUD>
    {
        [SerializeField] private TextMeshProUGUI m_text;
        
        private bool m_active;
        private float m_timeLeft;
        public static TimeLeftHUD Instace { get{ return InstanceSingletonInScene; }}


        protected override void Awake()
        {
            if (m_text == null)
            {
                DebugUtils.FunLogError("Lỗi, thành phần text chưa được thêm vào.");
                return;
            }
            m_active = false;
            m_timeLeft = GameSystem.Instance.FunGetSettingGame().FunGetTimeLeft();
        }

        private void OnEnable()
        {
            MessagingSystem.Instance.FunAttachListener(typeof(MessageStartGame), OnStartGame);
            MessagingSystem.Instance.FunAttachListener(typeof(MessageDisableEndGame), OnEndGame);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.FunDetachListener(typeof(MessageStartGame), OnStartGame);
            MessagingSystem.Instance.FunDetachListenerLate(typeof(MessageDisableEndGame), OnEndGame);
        }

        private void Update()
        {
            if (m_active == true && m_timeLeft > 0)
            {
                m_timeLeft -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(m_timeLeft / 60);
                int seconds = Mathf.FloorToInt(m_timeLeft % 60);

                m_text.text = $"{minutes:D2}:{seconds:D2}"; // mm:ss
            }

            if (m_timeLeft <= 0)
            {
                m_timeLeft = 0f;
                m_text.text = $"00:00";
                m_active = false;
                
                OnTimeUp();
            }
        }

        public string FunGetStrTimeLeft() => m_text.text;
        public void FunSetActive(bool active) => m_active = active;

        private void OnTimeUp()
        {
            MessagingSystem.Instance.FunTriggerMessage(new MessageTimeUp{ TimeLeft = m_text.text }, true);
        }

        private bool OnStartGame(IMessage message)
        {
            m_active = true;
            return true;
        }

        private bool OnEndGame(IMessage message)
        {
            gameObject.SetActive(false);
            return true;
        }
    }
}