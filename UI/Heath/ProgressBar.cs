using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace FireNBM
{
    /// <summary>
    ///     Chịu tránh nhiệm quản lý ui liên quan đến Heath.
    /// </summary>
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image m_imageProgress;
        [SerializeField] private Gradient m_colorGradient;  // Dùng để thiết lập màu sắc cho heathBar

        private bool m_isHandle = false;
        private float m_progressHandle;
        private float m_progressCurrent;

        private float m_speed;


        // ----------------------------------------------------------------------
        // API UNITY
        // ---------
        /////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            if (m_imageProgress == null)
            {
                DebugUtils.FunLog("Lỗi, image chứa thanh heathBar không được thêm vào.");
                return;
            }
            m_isHandle = false;
            m_progressHandle = 0.0f;
            m_progressCurrent = 0.0f;
            m_speed = 3.0f;
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Giảm một lượng progress của đối tượng.
        /// </summary>
        public void FunSetProgress(float progress)
        {
            if (progress < 0)
            {
                DebugUtils.FunLog("Lỗi, giá trị progress phải nên nằm trong khoảng: [0, 1]. Nhưng giá trị bạn đưa là: " + progress);
                return;
            }

            if (progress > 1)
                progress = 1;

            if (m_isHandle == true)
            {
                StopAllCoroutines();

                m_isHandle = false;
                m_progressHandle = m_progressHandle - m_progressCurrent;
                m_progressCurrent = 0.0f;
            }

            m_progressHandle += progress;
            StartCoroutine(AnimateProgress());
        }

        public void FunSetSpeed(float speed)
        {
            m_speed = speed;
        }

        public void FunReset()
        {
            StopAllCoroutines();
            m_imageProgress.fillAmount = 1.0f;
            m_colorGradient.Evaluate(0.0f);
            m_progressCurrent = 0.0f;
            m_progressHandle = 0.0f;
            m_isHandle = false;
        }


        // --------------------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        ///////////////////////////////////////////////////////////////////////////////////

        private IEnumerator AnimateProgress()
        {
            m_isHandle = true;
            float timeCount = 0;
            float fillAmount = m_imageProgress.fillAmount;

            while (timeCount < 1)
            {
                m_progressCurrent = Mathf.Lerp(0f, m_progressHandle, timeCount);
                timeCount += Time.deltaTime * m_speed;

                m_imageProgress.fillAmount = fillAmount - m_progressCurrent;
                m_imageProgress.color = m_colorGradient.Evaluate(1 - m_imageProgress.fillAmount);
                yield return null;
            }

            m_isHandle = false;
            m_progressCurrent = 0.0f;
            m_progressHandle = 0.0f;
        }
    }
}   