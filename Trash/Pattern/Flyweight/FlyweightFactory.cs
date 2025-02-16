// using System;
// using System.Collections.Generic;
// using UnityEngine;

// namespace FireNBM.Pattern
// {
//     /// <summary>
//     ///     Lớp đảm nhận lưu trữ dữ liệu chung của các đối tượng từ đầu vào.
//     /// </summary>
//     [Serializable]
//     public class FlyweightSetting
//     {
//         [Tooltip("Tên của dữ liệu dùng chung. Không được đặt tên trùng với tên gốc 'Root'.")]
//         public string Name;      

//         [Tooltip("Tên của node cha mà node hiện tại được phân nhánh từ đó. Với 'Root' là tên của node gốc.")]       
//         public string NameParent;

//         [Tooltip("Dữ liệu mà node lưu trữ.")]
//         public FlyweightData Data;
//     }

//     /// <summary>
//     ///     Được dùng để thêm dữ liệu dùng chung vào những đối tượng hỗ trợ lúc chúng được tạo ra.
//     /// </summary>
//     public class FlyweightFactory : Singleton<FlyweightFactory>
//     {
//         [SerializeField] private List<FlyweightSetting> m_settings;         // Lưu trữ dữ liệu từ bên ngoài đưa vào.

//         private CompositeManager m_compositeManager = new CompositeManager();                        // Dùng để thêm dữ liệu vào cây composite.
//         private Dictionary<GameObject, Composite> m_objToCompositeMap;      // Dùng để thêm thành phần composite vào đối tượng nếu hỗ trợ.

//         public static FlyweightFactory Instance { get => InstanceSingleton; }



//         // ---------------------------------------------------------------------------------
//         // API UNITY
//         // ---------
//         // /////////////////////////////////////////////////////////////////////////////////

//         protected override void Awake()
//         {
//             base.Awake();
//             m_objToCompositeMap = new Dictionary<GameObject, Composite>();
//         }

//         private void OnEnable()
//         {
//             if (m_settings == null || m_settings.Count == 0)
//             {
//                 Debug.LogError("No Flyweight settings provided.");
//                 return;
//             }

//             // Tạo cây composite.
//             foreach (var setting in m_settings)
//             {
//                 if (string.IsNullOrEmpty(setting.Name) || string.IsNullOrEmpty(setting.NameParent))
//                 {
//                     Debug.LogWarning($"FlyweightSetting is missing Name or NameParent: {setting}");
//                     continue;
//                 }
//                 setting.Data?.FunInitialize();
//                 CompositeLeaf composite = new CompositeLeaf(setting.Name, null, setting.Data);
//                 m_compositeManager.FunAddComposite(composite, setting.NameParent);
//             }

//             // Thêm giữ liệu vào map.
//             // Tại sao không thêm vào trước vì cần thiết lập cây composite để định hình rồi mới thêm vào map.
//             foreach (var setting in m_settings)
//             {
//                 if (string.IsNullOrEmpty(setting.Name) || string.IsNullOrEmpty(setting.NameParent))
//                 {
//                     Debug.LogWarning($"FlyweightSetting is missing Name or NameParent: {setting}");
//                     continue;
//                 }

//                 var composite = m_compositeManager.FunGetComposite(setting.Name);
//                 if (composite != null)
//                     m_objToCompositeMap.Add(setting.Data.Prefab, composite);
//             }
//         }

//         // ---------------------------------------------------------------------------------
//         // METHOD PUBLIC
//         // -------------
//         // /////////////////////////////////////////////////////////////////////////////////

//         public Composite FunTryGetFlyweightData(GameObject obj)
//         {
//             if (m_objToCompositeMap.TryGetValue(obj, out var composite) == false)
//             {
//                 Debug.Log($"Object '{obj.name}' no have data shared (flyweight data)");
//                 return null;
//             }
//             return composite;
//         }

//         public CompositeGroup FunGetRoot() => m_compositeManager.FunGetRoot();
//     }   
// }