using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FireNBM
{
    public class BeginGame : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_text;
        [SerializeField] private float m_timeFadeDuration = 2f;

        [Space(5)]
        [SerializeField] private Image m_backgroundIsClick;
        // [SerializeField] private float m_timeFadeDurationBackground = 2f;
        [SerializeField] private float m_fadeAlphaBackground = 0.5f;

        [Space(5)]
        [SerializeField] private SceneAsset m_sceneMainGame; 
        [SerializeField] private float m_timeLoadMainGame = 2.5f;

        [Space(5)]
        [SerializeField] private GameObject m_uiLoad;

        [Space(5)]
        [SerializeField] private AudioClip m_audioClick;
        [SerializeField] private AudioSource m_audioSourceClick;

        [Space(5)]
        [SerializeField] private AudioClip m_audioBackground;
        [SerializeField] private AudioSource m_audioSourceBackground;


        private bool m_isHandle;
        private bool m_isReduceOpacity;     
        private bool m_isStartGame = false;


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            if (m_audioSourceClick == null)
            {
                DebugUtils.FunLog("Audio Source for click is Null");
                return;
            }

            if (m_audioSourceBackground == null)
            {
                DebugUtils.FunLog("Audio Source for click is Null");
                return;
            }
        }

        private void Start()
        {
            m_isHandle = false;
            m_isReduceOpacity = true;
            m_uiLoad.SetActive(false);

            m_audioSourceClick.clip = m_audioClick;

            m_audioSourceBackground.clip = m_audioBackground;
            m_audioSourceBackground.loop = true;
            m_audioSourceBackground.Play();
        }

        private void Update()
        {
            if (m_isStartGame == true)
                return;

            if (IsClickStartGame() == true)
            {
                HandleStartGame();
                return;
            }
            UpdateFadeText(); 
        }


        // ---------------------------------------------------------------------------------
        // FUNCTION HELPER
        // ---------------
        // //////////////////////////////////////////////////////////////////////////////////

        private bool IsClickStartGame()
        {
            if (Input.GetMouseButtonDown(ConstantFireNBM.MOUSE_LEFT) == true)
            {
                if (IsPointerOverUIElement(out GameObject clickedObject))
                {
                    if (clickedObject.tag == ConstantFireNBM.BACKGROUND_UI)
                        return true;
                }
            }
            return false;
        }

        private void HandleStartGame()
        {
            m_isStartGame = true;
            m_audioSourceClick.Play();
            m_uiLoad.SetActive (true);
            StopAllCoroutines();
            
            Color color = m_backgroundIsClick.color;
            color.a = m_fadeAlphaBackground;
            m_backgroundIsClick.color = color;

            StartCoroutine(LoadMainGame());
        }

        private void UpdateFadeText()
        {
            if (m_isHandle == true)
                return;
            
            m_isHandle = true;
            if (m_isReduceOpacity == true)
                StartCoroutine(FadeText(1f, 0f, false));
            else
                StartCoroutine(FadeText(0f, 1f, true));
        }

        private IEnumerator FadeText(float startAlpha, float endAlpha, bool reduceOpacity)
        {
            Color color = m_text.color;
            float elapsedTime = 0f;

            while (elapsedTime <= m_timeFadeDuration)
            {
                if (m_isStartGame == true)
                    yield break;

                elapsedTime += Time.deltaTime;
                color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime/m_timeFadeDuration);
                m_text.color = color;

                yield return null;  
            }

            m_isHandle = false;
            m_isReduceOpacity = reduceOpacity;
        }

        private bool IsPointerOverUIElement(out GameObject clickedObject)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            if (results.Count > 0)
            {
                clickedObject = results[0].gameObject;
                return true;
            }

            clickedObject = null;
            return false;
        }

        private IEnumerator LoadMainGame()
        {
            AsyncOperation asyncLoad = GameSystem.Instance.FunLoadSceneAsync(m_sceneMainGame.name);
            asyncLoad.allowSceneActivation = false;

            while (asyncLoad.progress < 0.9f)
            {
                DebugUtils.FunLog("Loading progress: " + (asyncLoad.progress * 100) + "%");
                yield return null;
            }

            yield return new WaitForSeconds(m_timeLoadMainGame);
            asyncLoad.allowSceneActivation = true;
        }
    }
}