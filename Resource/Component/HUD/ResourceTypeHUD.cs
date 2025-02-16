using TMPro;
using UnityEngine;

namespace FireNBM
{
    public class ResourceTypeHUD : MonoBehaviour
    {
        protected int m_currentResource;
        [SerializeField] protected TypeResourceRTS m_typeResource;
        [SerializeField] protected TextMeshProUGUI m_text;

        // ----------------------------------------------------------------------
        // API UNITY
        // ---------
        /////////////////////////////////////////////////////////////////////////

        private void Start()
        {
            if (m_text == null)
            {
                Debug.LogError("Component TextMeshProUGUI in ResourceTypeHUD is NULL");
                return;
            }
            m_currentResource = 0;
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Cập nhật HUD cho tài nguyên khoáng sản. </summary>
        /// ------------------------------------------------------ 
        public virtual void FunUpdateResource(int resource)
        {
            m_currentResource += resource;
            if (m_currentResource < 0)
            {
                FunResetResource();
                return;
            }
            m_text.text = m_currentResource.ToString();
        }

        /// <summary>
        ///     Làm mới HUD cho tài nguyên khoáng sản. </summary>
        /// ----------------------------------------------------- 
        public void FunResetResource()
        {
            m_currentResource = 0;
            m_text.text = m_currentResource.ToString();
        }

        public int FunGetCurrentResourceMinerals() => m_currentResource;
    }
}