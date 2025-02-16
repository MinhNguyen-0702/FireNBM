using System.Collections;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class EndGameButtonQuitHUD : MonoBehaviour
    {
        [SerializeField] private GameObject m_loadUIQuit;
        private float m_timeLoadSceneMain = 2f;


        private void Awake()
        {
            m_loadUIQuit.SetActive(false);
        }

        public void FunQuitGame()
        {
            m_loadUIQuit.SetActive(true);
            MessagingSystem.Instance.FunTriggerMessage(new MessageDisableEndGame(), true);
            StartCoroutine(LoadMainGame());
        }

        private IEnumerator LoadMainGame()
        {
            AsyncOperation asyncLoad = GameSystem.Instance.FunLoadSceneAsync(GameSystem.Instance.FunGetStrSceneMain());
            asyncLoad.allowSceneActivation = false;

            while (asyncLoad.progress < 0.9f)
            {
                DebugUtils.FunLog("Loading progress: " + (asyncLoad.progress * 100) + "%");
                yield return null;
            }

            yield return new WaitForSeconds(m_timeLoadSceneMain);
            asyncLoad.allowSceneActivation = true;
        }
    }
}