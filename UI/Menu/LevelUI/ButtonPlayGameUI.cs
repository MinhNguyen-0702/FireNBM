using System.Collections;
using UnityEditor;
using UnityEngine;

namespace FireNBM
{
    public class ButtonPlayGameUI : ButtonTypeGameUI
    {
        [SerializeField] private SceneAsset m_scenePlay;
        [SerializeField] private GameObject m_uiLoadPlay;
        [SerializeField] private GameObject m_uiLevelManager;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void FunHandleOnclick()
        {
            base.FunPlayOnclick();
            StartCoroutine(PlayClickSuccess());
        }

        private IEnumerator LoadScenePlay()
        {
            AsyncOperation asyncLoad = GameSystem.Instance.FunLoadSceneAsync(m_scenePlay.name);
            asyncLoad.allowSceneActivation = false;

            // Tiến trình tải diễn ra theo thời gian
            while (asyncLoad.progress < 0.9f)
            {
                DebugUtils.FunLog("Loading progress: " + (asyncLoad.progress * 100) + "%");
                yield return null;
            }

            yield return new WaitForSeconds(2.5f);
            m_uiLevelManager.SetActive(false);

            asyncLoad.allowSceneActivation = true;
        }

        private IEnumerator PlayClickSuccess()
        {
            yield return new WaitForSeconds(0.5f);

            m_uiLoadPlay.SetActive(true);
            StartCoroutine(LoadScenePlay());           
        }
    }
} 