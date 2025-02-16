// using UnityEngine;

// namespace FireNBM
// {
//     // Nơi cung cấp tài nguyền mà phe mình đã khai thác được.
//     public class ResourceManager : MonoBehaviour
//     {
//         private int m_minerals;
//         private int m_gas;
//         private int m_supply;

//         static private ResourceManager m_instance;
//         static public ResourceManager Instance { get{ return m_instance; }}


//         private void Awake()
//         {   
//             if (m_instance == null)
//                 m_instance = this;
//             if (m_instance != this)
//                 Destroy(gameObject);

//             m_minerals = 0;
//             m_gas = 0;
//             m_supply = 0;
//         }


//         public void FunResourceMinerals(int count)
//         {

//         }

//         public void FunResourceGas(int count)
//         {

//         }

//         public void FunResouceSupply()
//         {

//         }
//     }
// }