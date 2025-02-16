using UnityEngine;

namespace FireNBM
{
    public abstract class ObjectTypeBaseHealthComp : MonoBehaviour
    {
        [SerializeField] private GameObject m_objectProgress;

        private ProgressBar m_progress;
        private CanvasGroup m_canvasGroup;
        private float m_maxHP;
        private float m_currentHP;
        private bool m_isDeath;
        private bool m_isActiveCanvas;


        // ----------------------------------------------------------------------
        // API UNITY
        // ---------
        /////////////////////////////////////////////////////////////////////////

        protected virtual void Awake()
        {
            m_progress = m_objectProgress.GetComponent<ProgressBar>();
            if (m_progress == null)
            {
                DebugUtils.FunLog("Lỗi đối tượng được gắn không có thành phần 'ProgressBar'.");
                return;
            }
            m_canvasGroup = m_objectProgress.GetComponent<CanvasGroup>();
            if (m_canvasGroup == null)
            {
                DebugUtils.FunLog("Lỗi đối tượng UI không có thành phần CanvasGroup.");
                return;
            }

            m_maxHP = 1.0f;
            m_currentHP = m_maxHP;
            m_isDeath = false;
            m_isActiveCanvas = true;
        }

        protected virtual void Update()
        {
            if (m_canvasGroup.alpha == 1)
            {
                m_objectProgress.transform.LookAt(m_objectProgress.transform.position + Camera.main.transform.rotation * Vector3.forward, 
                                  Camera.main.transform.rotation * Vector3.up);
            }

            if (m_currentHP <= 0.0f && m_isDeath == false)
            {
                m_isDeath = true;
                Death();
            }
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        public void FunSetData(float health)
        {
            m_maxHP = health;
            m_currentHP = health;
            m_isDeath = false;
        }

        public void FunSetHealth(float health) 
        {
            if (m_isDeath == true)
                return;

            m_progress.FunSetProgress(health / m_maxHP);
            m_currentHP -= health;
            if (m_currentHP <= 0)
                m_currentHP = 0;
        }

        public float FunGetMaxHP() => m_maxHP;
        public float FunGetCurrentHP() => m_currentHP;
        public void FunSetSpeed(float speed) => m_progress.FunSetSpeed(speed);

        /// <summary>
        ///     Ẩn hiện UI (false = ẩn), (true - hiện) </summary>
        /// -----------------------------------------------------
        public void FunSetActiveHealth(bool active)
        {
            if (m_isActiveCanvas == active)
                return;
            
            m_isActiveCanvas = active;
            float alpha = active ? 1.0f : 0.0f; 
            m_canvasGroup.alpha = alpha;
        }

        
        // ----------------------------------------------------------------------
        // FUNSTION ABSTRACT
        // -----------------
        /////////////////////////////////////////////////////////////////////////

        protected abstract void Death();
    }
}