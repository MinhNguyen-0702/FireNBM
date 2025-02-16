// using UnityEngine;
// using UnityEditor;
// using FireNBM.Pattern;
// using FireNBM.Custom;

// namespace FireNBM
// {
//     [CustomEditor(typeof(FlyweightFactory))]
//     public class FlyweightFactoryEditor : Editor
//     {
//         // Được dùng để tránh truy cập node sau khi xóa.
//         private bool m_cleanActionComposite = false;

//         private GUIStyle m_boxStyle1;
//         private GUIStyle m_boxStyle2;
//         private GUIStyle m_boxStyle3;


//         public override void OnInspectorGUI()
//         {
//             m_boxStyle1 = new GUIStyle(GUI.skin.box)
//             {
//                 padding = new RectOffset(10, 10, 10, 10),    // Padding bên trong Box
//                 margin = new RectOffset(5, 5, 0, 0)          // Khoảng cách ngoài Box
//             };

//             m_boxStyle2 = new GUIStyle(GUI.skin.box)
//             {
//                 padding = new RectOffset(30, 30, 10, 10),    
//                 margin = new RectOffset(5, 5, 5, 5)         
//             };

//             m_boxStyle3 = new GUIStyle(GUI.skin.box)
//             {
//                 padding = new RectOffset(10, 10, 10, 10),    // Padding bên trong Box
//                 margin = new RectOffset(5, 5, 5, 5)          // Khoảng cách ngoài Box
//             };

//             base.OnInspectorGUI();
//             GUIStyleCustom.Label.FunSetTitleScript("Flyweight Factory");
//             var owner = (FlyweightFactory)target;

//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("Data", -3, TextAnchor.LowerLeft);
//             DisplayFlyweightData(owner, owner.FunGetRoot(), 0);
//             GUILayout.EndVertical();
//             GUILayout.Space(15);
//         }

//         // Hiện thị từng dữ liệu trong flyweight data.
//         // -------------------------------------------
//         private void DisplayFlyweightData(FlyweightFactory owner, CompositeGroup group, int indent = 0)
//         {
//             var listActionData = group.FunGetChildren();
//             for (int i = 0; i < listActionData.Count; ++i)
//             {
//                 GUILayout.BeginHorizontal();
//                 GUILayout.Space(indent);
//                 UpdateCompositeAction(owner, group, listActionData[i]);
//                 GUILayout.EndHorizontal();

//                 GUILayout.Space(7);
                
//                 if (m_cleanActionComposite == false && listActionData[i] is CompositeGroup flyweightGroup)
//                     DisplayFlyweightData(owner, flyweightGroup, indent + 15);
//             }
//         }  

//         // Cập nhật từng dữ liệu một.
//         // -------------------------
//         private void UpdateCompositeAction(FlyweightFactory owner, CompositeGroup compositeGroup, Composite composite)
//         {
//             if (composite == null)
//             {
//                 Debug.LogError("compositeAction is null!");
//                 return;
//             }

//             GUILayout.BeginVertical(m_boxStyle1);
//             GUILayout.Space(10);

//             // Info Action detailt
//             // -------------------
//             GUILayout.BeginVertical(m_boxStyle1);
//             GUILayout.BeginHorizontal();
//             EditorGUI.indentLevel++;

//             // Khối thứ nhất.
//             GUILayout.BeginVertical();
//             {
//                 GUILayout.BeginHorizontal();
//                 GUIStyleCustom.Label.FunSetLabel("Name", 48);
//                 owner. = EditorGUILayout.TextField(currentEnum);
//                 GUILayout.EndHorizontal();
                
                
//             }
//             GUILayout.EndVertical();

//             // Khối thứ hai.
//             EditorGUILayout.BeginVertical();
//             {
//                 if (childComposite is ActionCompositeLeaf)
//                 {
//                     if (GUILayout.Button("Remove", GUILayout.Height(38)))
//                     {
//                         compositeGroup.FunRemoveActionChild(childComposite);
//                         m_cleanActionComposite = true;
//                     }
//                 }
//                 else if (childComposite is ActionCompositeGroup group)
//                 {
//                     if (GUILayout.Button("Remove", GUILayout.Height(19)))
//                     {
//                         compositeGroup.FunRemoveActionChild(childComposite);
//                         m_cleanActionComposite = true;
//                     } 
//                     if (GUILayout.Button("Add", GUILayout.Height(19)))
//                     {
//                         actionData.ActionGroupChild = group;
//                     }
//                 }
//             }
//             EditorGUILayout.EndVertical();

//             EditorGUI.indentLevel--;
//             GUILayout.EndHorizontal();
//             GUILayout.EndVertical();

//             GUILayout.Space(3);
//             GUILayout.EndVertical();

//         }

//         // Xử lý việc hiển thị và logic của các button tạo action composite. 
//         // ----------------------------------------------------------------
//         private void UpdateButtonCreateAction(ActionData actionData)
//         {
//             GUILayout.BeginHorizontal();
//             if (GUILayout.Button("New Action Execute", GUILayout.Height(30)))
//             {
//                 actionData.ShowDropdown = true;
//                 actionData.IsCompositeActionLeaf = true;
//                 actionData.IsCompositeActionGroup = false;
//             }
//             GUILayout.Space(5);

//             if (GUILayout.Button("New Action Display", GUILayout.Height(30)))
//             {
//                 actionData.ShowDropdown = true;
//                 actionData.IsCompositeActionLeaf = false;
//                 actionData.IsCompositeActionGroup = true;

//             }
//             GUILayout.EndHorizontal();
//         }
//     }
// }