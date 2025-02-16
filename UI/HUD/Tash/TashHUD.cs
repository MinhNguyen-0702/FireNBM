using FireNBM.Pattern;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace FireNBM
{
    public class TashHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_text;

        private int m_maxEnemy;
        private int m_currEnemy;

        private void Awake()
        {
            if (m_text == null)
            {
                DebugUtils.FunLogError("Lỗi, thành phần text chưa được thêm vào.");
                return;
            }
        } 

        private void Start()
        {
            var listEnemy = EnemyManager.Instance.FunGetListEnemy();
            if (listEnemy.Count == 0)
            {
                DebugUtils.FunLog("Lỗi danh sách quái enemy không có quái nào.");
                return;
            }

            m_maxEnemy = listEnemy.Count;
            m_currEnemy = 0;

            foreach (var enemy in listEnemy)
            {
                var deathComp = enemy.GetComponent<UnitDeathComp>();
                deathComp.FunAddAction(OnEnemyDeath);
            }
        }

        private void OnEnable()
        {
            MessagingSystem.Instance.FunAttachListener(typeof(MessageTimeUp), OnTimeUp);
            MessagingSystem.Instance.FunAttachListener(typeof(MessageDisableEndGame), OnEndGame);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.FunDetachListener(typeof(MessageTimeUp), OnTimeUp);
            MessagingSystem.Instance.FunDetachListenerLate(typeof(MessageDisableEndGame), OnEndGame);
        }

        private bool IsCheckEnemyPath()
        {
            return m_currEnemy >= m_maxEnemy;
        }

        private void EndGame()
        {
            MessagingSystem.Instance.FunTriggerMessage(new MessageUpdateEndGame
            { 
               IsVictory = IsCheckEnemyPath(),
               TimeLeft =  TimeLeftHUD.Instace.FunGetStrTimeLeft(),
               EnemyCount = m_text.text
            }, false);

            TimeLeftHUD.Instace.FunSetActive(false);
        }


        private bool OnTimeUp(IMessage message)
        {
            EndGame();
            return true;
        }

        private void OnEnemyDeath()
        {
            m_currEnemy += 1;
            m_text.text = $"{m_currEnemy}/{m_maxEnemy}";

            if (IsCheckEnemyPath() == true)
                EndGame();
        }

        private bool OnEndGame(IMessage message)
        {
            gameObject.SetActive(false);
            return true;
        }
    }
}