using UnityEngine;

namespace FireNBM
{
    public class UnitWorkerComp : MonoBehaviour
    {
        [SerializeField] private int m_maxExploitRes = 5;       // Lượng tài nguyên tối đa mà công nhân có thể khai thác được trong một phiên.
        [SerializeField] private int m_timeExploit = 4;         // Thời gian khai thác xong một phiên làm việc.
        [SerializeField] private int m_timeReturnTownhall = 3;  // Thời gian trao trả lại tài nguyên khai thác cho công trình.

        private GameObject m_townhall;                          // Công trình chứa worker.
        private ResourceMineralsManager m_resourceMinerals;     // Nơi chứa khoáng sản mà công nhân cần thu nhập.


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        private void Start()
        {
            m_resourceMinerals = ResourceMineralsManager.Instance;
            if (m_resourceMinerals == null)
            {
                DebugUtils.FunLogError("Lỗi, không thể tham chiếu đến chỗ quản lý tài nguyên.");
                return;
            }
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thiết lập công trình khai thác tài nguyên mà công nhân thuộc về. </summary>
        /// ------------------------------------------------------------------------------
        public void FunSetTownhall(GameObject townhall) => m_townhall = townhall;

        /// <summary>
        ///     Lấy công trình khai thác tài nguyên mà công nhân thuộc về.</summary>
        /// ------------------------------------------------------------------------
        public GameObject FunGetTownhall() => m_townhall;

        /// <summary>
        ///     Lấy lượng tài nguyên tối đa mà unit woker có thể khai thác trong một phiên. </summary>
        /// ------------------------------------------------------------------------------------------
        public int FunGetMaxExploitRes() => m_maxExploitRes;

        /// <summary>
        ///     Lấy Thời gian khai thác xong một phiên làm việc. </summary>
        /// ----------------------------------------------------------------
        public int FunGetTimeExploit() => m_timeExploit;

        /// <summary>
        ///     Thời gian trao trả lại tài nguyên khai thác cho công trình. </summary>
        /// --------------------------------------------------------------------------
        public int FunGetTimeReturnTownhall() => m_timeReturnTownhall;

        /// <summary>
        ///     Cái này chỉ tìm tài nguyên gần nhất thôi. </summary>
        // --------------------------------------------------------
        public GameObject FunGetNearestResourceMinerals()
        {
            return m_resourceMinerals.FunGetNearestResourceMinerals(gameObject.transform.position);
        }
    }
}