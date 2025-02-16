using System.Collections;
using UnityEngine;

namespace FireNBM
{
    public class ButtonStartGameUI : ButtonTypeGameUI
    {
        [SerializeField] private GameObject m_objectIsClickStart;
        [SerializeField] private GameObject m_mainMenuUI;

        protected override void Awake()
        {
            base.Awake();
            if (m_objectIsClickStart == null)
            {
                DebugUtils.FunLog("Lỗi, Đối tượng chưa được gán");
                return;
            }
            m_objectIsClickStart.SetActive(false); 
        }

        public override void FunHandleOnclick()
        {
            base.FunPlayOnclick();
            StartCoroutine(PlayClickSuccess());
        }

        private IEnumerator PlayClickSuccess()
        {
            yield return new WaitForSeconds(0.5f);
            m_objectIsClickStart.SetActive(true);
            m_mainMenuUI.SetActive(false);
        }
    }
} 