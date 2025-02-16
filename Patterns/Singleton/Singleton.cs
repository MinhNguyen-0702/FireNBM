using UnityEngine;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     Đảm bảo chỉ có một phiên bản duy nhất của lớp T được tạo ra.
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T m_instanceForDontDestroyOnLoad;
        private static T m_instanceInScene;

        /// <summary>
        ///     Singletion ở chế độ DontDestroyOnLoad.
        /// </summary>
        protected static T InstanceSingleton
        {
            get
            { 
                if (m_instanceForDontDestroyOnLoad == null)
                    SetUpSingleton(ref m_instanceForDontDestroyOnLoad, true);

                return m_instanceForDontDestroyOnLoad;
            }
        }

        /// <summary>
        ///     Chỉ có một phiên bản Singleton duy nhất trong scene. 
        /// </summary>
        protected static T InstanceSingletonInScene
        {
            get
            {
                if (m_instanceInScene == null)
                    SetUpSingleton(ref m_instanceInScene, false);

                return m_instanceInScene;
            }
        }

        protected virtual void Awake()
        {
            if (m_instanceForDontDestroyOnLoad != null && m_instanceForDontDestroyOnLoad != this)
            {
                DebugUtils.FunLog($"Attempting to create multiple instances of {typeof(T).Name}. Destroying new instance.");
                Destroy(this.gameObject);
                return;
            }

            m_instanceInScene = (T)this;
            m_instanceForDontDestroyOnLoad = (T)this;
        }


        private static void SetUpSingleton(ref T instance, bool useDontDestroyOnLoad)
        {
            // Tìm đối tượng trong scene.
            T[] managers = FindObjectsOfType<T>();

            if (managers.Length == 1)  
            {
                instance = managers[0];
            }
            else if (managers.Length > 1) 
            {
                DebugUtils.FunLog($"More than one {typeof(T).Name} found in the scene. Destroying all instances.");

                for (int i = 1; i < managers.Length; i++) 
                    Destroy(managers[i].gameObject);

                instance = managers[0];
            }
            else  
            {
                GameObject singletonObject = new GameObject(typeof(T).Name, typeof(T));
                instance = singletonObject.GetComponent<T>();
            }

            if (useDontDestroyOnLoad == true)
                DontDestroyOnLoad(instance.gameObject);
        }
    }
}