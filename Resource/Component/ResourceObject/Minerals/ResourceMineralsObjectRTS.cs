using UnityEngine;

namespace FireNBM
{
    [RequireComponent(typeof(ResourceMineralsDestroy))]

    /// <summary>
    ///     Có chức năng cập nhật tài nguyên. khi worker đến thu thập tài nguyên.
    /// </summary>
    public class ResourceMineralsObjectRTS : ResourceTypeObjectRTS
    {
        [SerializeField] int m_maxResourceMinerals = 100; 
        private bool m_isHandleExhaustedResources;

        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        private void Start()
        {
            ResourceMineralsManager.Instance.FunAddResouceMinerals(gameObject);
            m_isHandleExhaustedResources = false;
        }

        private void FixedUpdate()
        {
            if (IsExhaustedResources() == false)
                return;
            
            if (m_isHandleExhaustedResources == false)
            {
                HandleExhaustedResources();
                m_isHandleExhaustedResources = true;
            }
        }

        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Nơi dành cho công nhân dùng để khai thác tài nguyên. </summary>
        /// ------------------------------------------------------------------- 
        public int FunExploitResources(int count)
        {
            int exploitRes = 0;
            if (m_maxResourceMinerals > count)
            {
                exploitRes = count;
                m_maxResourceMinerals -= count;
            }
            else 
            {
                exploitRes = count - m_maxResourceMinerals;
                m_maxResourceMinerals = 0;
            }
            return exploitRes;
        }

        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        // Phá hủy đối tượng nếu tài nguyên đã cạn.
        // ----------------------------------------
        private void HandleExhaustedResources()
        {
            if (m_maxResourceMinerals <= 0)
            {
                var destroyMinerals = GetComponent<ResourceMineralsDestroy>();
                if (destroyMinerals == null)
                {
                    Debug.Log("Không có thành phần Destroy");
                    return;
                }
                destroyMinerals.FunDestroyMinerals();
            }
        }

        // Kiểm tra xem tài nguyên đã cạn chưa.
        // -----------------------------------
        private bool IsExhaustedResources()
        {
            return m_maxResourceMinerals <= 0;
        }
    }
}