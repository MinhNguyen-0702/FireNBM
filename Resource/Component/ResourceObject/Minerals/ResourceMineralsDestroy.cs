using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    public class ResourceMineralsDestroy : MonoBehaviour
    {
        private float m_duration;
        private float m_elapsedTime;
        private bool m_isUpdateDestroy;
        private HashSet<Action> m_listActionDestroys;  // Danh sách chứa các hành động sẽ được gọi khi tài nguyên cạn kiệt.


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_duration = 0f;
            m_elapsedTime = 3f;
            m_isUpdateDestroy = false;
            m_listActionDestroys = new HashSet<Action>();
        }
        
        private void FixedUpdate()
        {
            if (m_isUpdateDestroy == false)
                return;
            
            UpdateDestroyMinerals();
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Hết tài nguyên khai thác, phá hủy đối tượng. </summary>
        /// -----------------------------------------------------------
        public void FunDestroyMinerals()
        {
            m_isUpdateDestroy = true;
            ResourceMineralsManager.Instance.FunRemoveResourceMinerals(gameObject);

            // Thông báo cho các công nhân khai thác rằng tài nguyên đã cạn kiệt.
            foreach (var action in m_listActionDestroys)
                action.Invoke();
            
            m_listActionDestroys.Clear();
        }

        /// <summary>
        ///     Đăng ký sự kiện khi tài nguyên bị phá hủy.</summary>
        // ---------------------------------------------------------
        public void FunAddActionDestroy(Action action)
        {
            if (action == null || m_listActionDestroys.Contains(action) == true)
                return;

            m_listActionDestroys.Add(action);
        }


        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        private void UpdateDestroyMinerals()
        {
            transform.Translate(-Vector3.up * Time.deltaTime);
            if (m_duration >= m_elapsedTime)
            {
                Destroy(gameObject);
            }
            m_duration += Time.deltaTime;
        }
    }
}