using System.Collections.Generic;
using UnityEngine;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     Lưu trữ các thành phần cần reset lại dữ liệu của đối tượng khi spawn.
    /// </summary>
    public struct PoolablePrefabData
    {
        public GameObject Owner;
        public IPoolableComp[] PoolableComponents;
    }

    /// <summary>
    ///     Quản lý một nhóm đối tượng cụ thể.
    /// </summary>
    public class PrefabPool
    {
        private Dictionary<GameObject, PoolablePrefabData> m_activeList;
        private Queue<PoolablePrefabData> m_inactiveList;

        private MessagingSystem m_messagingSystem;


        // --------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        //////////////////////////////////////////////////////////////////

        public PrefabPool()
        {
            m_activeList = new Dictionary<GameObject, PoolablePrefabData>();
            m_inactiveList = new Queue<PoolablePrefabData>();
            m_messagingSystem = MessagingSystem.Instance;
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Sản sinh đối tượng thuộc nhóm prefab.</summary>
        /// ---------------------------------------------------
        public GameObject FunSpawn(GameObject prefab, Vector3 pos, Quaternion rotation)
        {
            PoolablePrefabData data;

            // Kiểm tra xem có đối tượng nào trong trạng thái chờ ko.
            if (m_inactiveList.Count > 0)
            {
                data = m_inactiveList.Dequeue();
            }
            else 
            {
                GameObject newObj = GameObject.Instantiate(prefab, pos, rotation) as GameObject;
                data = new PoolablePrefabData();
                data.Owner = newObj;
                data.PoolableComponents = newObj.GetComponents<IPoolableComp>();

                m_messagingSystem.FunTriggerMessage(new MessageGetNewObject(newObj, prefab), false);
            }
            data.Owner.SetActive(true);
            data.Owner.transform.position = pos;
            data.Owner.transform.rotation = rotation;

            // Cung cấp lại dữ liệu cho các thành phần của đối tượng.
            for (int i = 0; i < data.PoolableComponents.Length; ++i)
                data.PoolableComponents[i].FunSpawned();

            m_activeList.Add(data.Owner, data);
            return data.Owner;
        }

        /// <summary>
        ///     Thu hồi đối tượng và đặ về trạng thái trờ.</summary>
        /// ---------------------------------------------------------
        public bool FunDespawn(GameObject objToDespawn)
        {
            // Thoát nếu đối tượng cần hủy không nằm trong pool.
            if (m_activeList.ContainsKey(objToDespawn) == false)
            {
                Debug.LogError("This object is not managed by this object pool!");
                return false;
            }

            // Lấy đối tượng cần thu hồi.
            PoolablePrefabData data = m_activeList[objToDespawn];
            
            // Xóa hết dữ liệu cho các thành phần của đối tượng.
            for (int i = 0; i < data.PoolableComponents.Length; ++i)
                data.PoolableComponents[i].FunDespawned();

            data.Owner.SetActive(false);
            m_activeList.Remove(objToDespawn);
            m_inactiveList.Enqueue(data);
            return true;
        }
    }
}