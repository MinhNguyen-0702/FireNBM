using System.Collections;
using UnityEngine;

namespace FireNBM
{
    public class ButtonBreakGameUI : ButtonTypeGameUI
    {
        [SerializeField] private GameObject m_levelManagerUI;
        [SerializeField] private GameObject m_mainMenuUI;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void FunHandleOnclick()
        {
            base.FunPlayOnclick();
            StartCoroutine(PlayClickSuccess());
        }

        private IEnumerator PlayClickSuccess()
        {
            yield return new WaitForSeconds(0.5f);
            m_mainMenuUI.SetActive(true);
            m_levelManagerUI.SetActive(false);
        }
    }
}