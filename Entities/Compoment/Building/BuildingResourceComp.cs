using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Đây là nơi tiếp nhận tài nguyên từ worker để xử lý.
    /// </summary>
    [AddComponentMenu("FireNBM/RaceRTS/Building/Building Resource Comp")]
    public class BuildingResourceComp : MonoBehaviour
    {
        private MessagingSystem m_messagingSystem;

        // ----------------------------------------------------------------------
        // API UNITY
        // ---------
        /////////////////////////////////////////////////////////////////////////

        private void Start()
        {
            m_messagingSystem = MessagingSystem.Instance;
            if (m_messagingSystem == null)
            {
                DebugUtils.FunLogError("Lỗi hệ thông Thông điệp MessaginSystem chưa tồn tại.");
                return;
            }
        }

        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Nơi worker tiếp nhận để đưa tài nguyên cho công trình để xử lý.
        /// </summary>
        /// <param name="countResource"></param>
        public void FunHandleResource(int countResource)
        {
            m_messagingSystem.FunTriggerMessage(new MessageUpdateResourceMineralsHUD(countResource), false);
        }   
    }
}