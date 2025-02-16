using UnityEngine;
using System.Collections.Generic;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     Một hệ thống quản lý các nhóm đối tượng (pooling) khác nhau.</summary>
    public static class PrefabPoolingSystem
    {
        // Truy suất đối tượng gốc prefab để lấy nhóm tương ứng.
        private static Dictionary<GameObject, PrefabPool> m_prefabToPoolMap = new 
            Dictionary<GameObject, PrefabPool>();
        
        // Truy suất đối tượng sản sinh để lấy nhóm.
        private static Dictionary<GameObject, PrefabPool> m_objectToPoolMap = new 
            Dictionary<GameObject, PrefabPool>();


        // ---------------------------------------------------------------------------------
        // FUNCTION PUBLIC 
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Sản sinh đối tượng từ nhóm pool, với bản gốc là từ prefab.</summary>
        /// ------------------------------------------------------------------------
        public static GameObject FunSpawn(GameObject prefab, Vector3 pos, Quaternion rotation)
        {
            // Thêm vào map nếu chưa có pool cho đối tượng prefab này.
            if (m_prefabToPoolMap.ContainsKey(prefab) == false)
            {
                m_prefabToPoolMap.Add(prefab, new PrefabPool());
            }

            // Lấy nhóm đối tượng (pool) cho loại prefab này.
            PrefabPool pool = m_prefabToPoolMap[prefab];
            // Lấy đối tượng có sẵn trong pool này.
            GameObject go = pool.FunSpawn(prefab, pos, rotation);

            m_objectToPoolMap.Add(go, pool);
            return go;
        }

        /// <summary>
        ///     Một hàm quá tải, sản sinh đối tượng với giá trị mặc định.</summary>
        /// ----------------------------------------------------------------------
        public static GameObject FunSpawn(GameObject prefab)
        {
            return FunSpawn(prefab, Vector3.zero, Quaternion.identity);
        }

        /// <summary>
        ///     Thu hồi đối tượng khi ko còn sử dụng.</summary>
        /// ---------------------------------------------------
        public static bool FunDespawn(GameObject obj)
        {
            // Báo lỗi và thoát nếu obj ko có nhóm.
            if (m_objectToPoolMap.ContainsKey(obj) == false)
            {
                Debug.LogError(string.Format("Object '{0}' not managed by pool system!", obj.name));
                return false;
            }

            // Lấy nhóm và thu hồi đối tượng.
            PrefabPool pool = m_objectToPoolMap[obj];
            if (pool.FunDespawn(obj))
            {
                m_objectToPoolMap.Remove(obj);
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Khởi tạo nhóm đối tượng.</summary>
        /// --------------------------------------
        public static void FunPrespawn(GameObject prefab,int numToSpawn)
        {
            List<GameObject> listSpawn = new List<GameObject>(numToSpawn);
            for (int i = 0; i < numToSpawn; ++i)
            {
                GameObject newSpawn = FunSpawn(prefab);
                listSpawn.Add(newSpawn);
            }

            foreach (var objSpawn in listSpawn)
            {
                FunDespawn(objSpawn);
            }
            listSpawn.Clear();
        }

        /// <summary>
        ///     Reset laị hệ thống pool mỗi khi chuyển cảnh khác.</summary>
        /// ---------------------------------------------------------------- 
        public static void FunReset()
        {
            m_prefabToPoolMap.Clear();
            m_objectToPoolMap.Clear();
        }
    }
}