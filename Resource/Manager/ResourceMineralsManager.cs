using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Chịu tránh nhiệm quản lý tài nguyên khoáng sản cho trò chơi.
    /// </summary>
    public class ResourceMineralsManager : Singleton<ResourceMineralsManager>
    {
        private ResourceMineralsHUD m_mineralsHUD;
        private HashSet<GameObject> m_mineralsList;
        static public ResourceMineralsManager Instance { get { return InstanceSingletonInScene; }}


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        protected override void Awake()
        {
            base.Awake();
            m_mineralsList = new HashSet<GameObject>();
        }

        private void OnEnable()
        {
            MessagingSystem.Instance.FunAttachListener(
                typeof(MessageUpdateResourceMineralsHUD), OnUpdateResourceMineralsHUD);
        }

        private void OnDisable()
        {
            MessagingSystem.Instance.FunDetachListener(
                typeof(MessageUpdateResourceMineralsHUD), OnUpdateResourceMineralsHUD);
        }


        // --------------------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thêm đối tượng minerals vào danh sách quản lý. </summary>
        /// ------------------------------------------------------------- 
        public void FunAddResouceMinerals(GameObject minerals)
        {
            if (m_mineralsList.Contains(minerals) == true)
            {
                Debug.Log("Tài nguyên này đã tồn tại.");
                return;
            }
            m_mineralsList.Add(minerals);
        }

        /// <summary>
        ///     Xóa minerals khi công nhân đã khai thác hết tài nguyên. </summary>
        /// ----------------------------------------------------------------------
        public void FunRemoveResourceMinerals(GameObject minerals)
        {
            if (m_mineralsList.Contains(minerals) == false)
            {
                Debug.Log("Tài nguyên này không tồn tại để xóa nó.");
                return;
            }
            m_mineralsList.Remove(minerals);
        }

        /// <summary>
        ///     Kết nối với đối tượng chịu trách nhiệm cập nhật HUD về minerals. </summary>
        /// ------------------------------------------------------------------------------- 
        public void FunSetResourceMineralsHUD(ResourceMineralsHUD hudMinerals)
        {
            m_mineralsHUD = hudMinerals;
        }

        /// <summary>
        ///     Tìm tài nguyên gần nhất với khoảnh cách được đưa vào.</summary>
        /// -------------------------------------------------------------------
        public GameObject FunGetNearestResourceMinerals(Vector3 posCurrent)
        {
            if (IsStillResourcesMinerals() == false)
                return null;

            float distance = float.MaxValue;
            GameObject minerals = null;

            foreach (var res in m_mineralsList)
            {
                float testDistance = Vector3.Distance(posCurrent, res.transform.position);
                if (distance > testDistance)
                {
                    distance = testDistance;
                    minerals = res;
                }
            }
            return minerals;
        }

        public int FunGetCurrResourceMinerals() => m_mineralsHUD.FunGetCurrentResourceMinerals();
        
        // --------------------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        ///////////////////////////////////////////////////////////////////////////////////
        
        // Kiểm tra xem bản đồ còn tài nguyên để khai thác không.
        // -----------------------------------------------------
        private bool IsStillResourcesMinerals()
        {
            return m_mineralsList.Count > 0;
        }
        
        // -----------------------------------------------------------------------------
        // HANDLE MESSAGE
        // --------------
        // //////////////////////////////////////////////////////////////////////////////

        private bool OnUpdateResourceMineralsHUD(IMessage message)
        {
            var messageResource = message as MessageUpdateResourceMineralsHUD;
            m_mineralsHUD.FunUpdateResource(messageResource.CountResourceMinerals);
            return true;
        }
    }
}
// Cung cấp vị trí cho unit worker gần nhất.
// Nếu có unit woker khai thác (tối đa 3 worker cho một khoáng sản) thì tìm vị trí khác.