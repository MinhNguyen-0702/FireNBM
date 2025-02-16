// using UnityEngine;
// using UnityEditor;
// using FireNBM.Custom;
// using System.Collections.Generic;

// namespace FireNBM
// {
//     [CustomEditor(typeof(SpawnManager)), CanEditMultipleObjects]
//     public class SpawnManagerEditor : Editor
//     {
//         private SerializedProperty m_unitPlayerSpawns;
//         private SerializedProperty m_buildingPlayerSpawns;
//         private SerializedProperty m_unitAISpawns;
//         private SerializedProperty m_buildingAISpawns;

//         // Lưu trữ trạng thái đóng mở của từng phần tử.
//         private List<bool> m_foldoutUnitPlayerStates;     
//         private List<bool> m_foldoutBuildingPlayerStates;     
//         private List<bool> m_foldoutUnitAIStates;     
//         private List<bool> m_foldoutBuildingAIStates;     


//         // ----------------------------------------------------------------------------------
//         // API UNITY
//         // ---------
//         // ///////////////////////////////////////////////////////////////////////////////////

//         private void OnEnable()
//         {
//             m_unitPlayerSpawns      = serializedObject.FindProperty("m_unitPlayerSpawns");
//             m_buildingPlayerSpawns  = serializedObject.FindProperty("m_buildingPlayerSpawns");
//             m_unitAISpawns          = serializedObject.FindProperty("m_unitAISpawns");
//             m_buildingAISpawns      = serializedObject.FindProperty("m_buildingAISpawns"); 
            
//             m_foldoutUnitPlayerStates       = new List<bool>(new bool[m_unitPlayerSpawns.arraySize]);
//             m_foldoutBuildingPlayerStates   = new List<bool>(new bool[m_buildingPlayerSpawns.arraySize]);
//             m_foldoutUnitAIStates           = new List<bool>(new bool[m_unitAISpawns.arraySize]);
//             m_foldoutBuildingAIStates       = new List<bool>(new bool[m_buildingAISpawns.arraySize]);
//         }


//         public override void OnInspectorGUI()
//         {
//             GUIStyleCustom.Label.FunSetTitleScript("SPAWN MANAGER");
//             serializedObject.Update();

//             // FOR UNIT SPAWN.
//             // --------------
//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("Unit - Prefabs Pool");
//             {
//                 // Dành cho unit spawn Player.
//                 GUILayout.BeginVertical("", "GroupBox");
//                 GUIStyleCustom.Label.FunSetTitleHeader("For Unit Player");
//                 EditorGUILayout.Space(15);
//                 SetStyleList(m_unitPlayerSpawns, m_foldoutUnitPlayerStates, "Player", "Unit");
//                 EditorGUILayout.EndVertical();

//                 GUIStyleCustom.FunSpaceGroupBox();

//                 // Dành cho unit spawn AI.
//                 GUILayout.BeginVertical("", "GroupBox");
//                 GUIStyleCustom.Label.FunSetTitleHeader("For Unit AI");
//                 EditorGUILayout.Space(15);
//                 SetStyleList(m_unitAISpawns, m_foldoutUnitAIStates, "AI", "Unit");
//                 EditorGUILayout.EndVertical();
//             }
//             EditorGUILayout.EndVertical();

//             GUIStyleCustom.FunSpaceGroupBox();

//             // FOR BUILDING SPAWN.
//             // ------------------
//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("Building - Prefabs Pool");
//             {
//                 // Dành cho unit spawn Player.
//                 GUILayout.BeginVertical("", "GroupBox");
//                 GUIStyleCustom.Label.FunSetTitleHeader("For Building Player");
//                 EditorGUILayout.Space(15);
//                 SetStyleList(m_buildingPlayerSpawns, m_foldoutBuildingPlayerStates, "Player", "Building");
//                 EditorGUILayout.EndVertical();

//                 // Dành cho unit spawn AI.
//                 GUIStyleCustom.FunSpaceGroupBox();
//                 GUILayout.BeginVertical("", "GroupBox");
//                 GUIStyleCustom.Label.FunSetTitleHeader("For Building AI");
//                 EditorGUILayout.Space(15);
//                 SetStyleList(m_buildingAISpawns, m_foldoutBuildingAIStates, "AI", "Building");
//                 EditorGUILayout.EndVertical();
//             }
//             EditorGUILayout.EndVertical();

//             serializedObject.ApplyModifiedProperties();
//         }

        
//         // ---------------------------------------------------------------------------------
//         // FUNCTION HELPER
//         // ---------------
//         // //////////////////////////////////////////////////////////////////////////////////

//         // Hàm trợ giúp, thay đổi kiểu dáng list.
//         private void SetStyleList(SerializedProperty list, List<bool> foldoutStates, string name, string nameItem)
//         {
//             EditorGUI.indentLevel++;

//             // Truy cập các dữ liệu trong item của list.
//             SerializedProperty item;
//             SerializedProperty itemTypeName;
//             SerializedProperty itemDataSpawn;
//             SerializedProperty itemSizePool;

//             for (int i = 0; i < list.arraySize; ++i)
//             {
//                 item = list.GetArrayElementAtIndex(i);
//                 itemTypeName = item.FindPropertyRelative("TypeName");
//                 itemDataSpawn = item.FindPropertyRelative("DataSpawn");
//                 itemSizePool = item.FindPropertyRelative("SizePool");

//                 // Tên hiển thị tùy chỉnh
//                 string displayNameEnum = itemTypeName.enumDisplayNames[itemTypeName.enumValueIndex];
//                 string displayName = (i + 1) + " - " + nameItem + " " + displayNameEnum;

//                 EditorGUILayout.BeginVertical("box");
//                 foldoutStates[i] = EditorGUILayout.Foldout(foldoutStates[i], displayName, true);
//                 EditorGUILayout.EndVertical();

//                 if (foldoutStates[i] == false)
//                     continue;
                

//                 // Tạo một box chứa dữ liệu spawn
//                 EditorGUILayout.BeginVertical("box");
//                 EditorGUILayout.Space(3);

//                 // Tạo thành 2 khối bố cục.
//                 EditorGUILayout.BeginHorizontal();
//                 {
//                     // Dành cho dữ liệu spawn.
//                     EditorGUILayout.BeginVertical();
//                     {
//                         EditorGUILayout.BeginHorizontal();
//                         GUIStyleCustom.Label.FunSetLabel("Name", 55);
//                         EditorGUILayout.PropertyField(itemTypeName, GUIContent.none);
//                         EditorGUILayout.EndHorizontal();

//                         EditorGUILayout.BeginHorizontal();
//                         GUIStyleCustom.Label.FunSetLabel("Data", 55);
//                         EditorGUILayout.PropertyField(itemDataSpawn, GUIContent.none);
//                         EditorGUILayout.EndHorizontal();

//                         EditorGUILayout.BeginHorizontal();
//                         GUIStyleCustom.Label.FunSetLabel("Size", 55);
//                         EditorGUILayout.PropertyField(itemSizePool, GUIContent.none);
//                         EditorGUILayout.EndHorizontal();
//                     }
//                     EditorGUILayout.EndVertical();

//                     // Cho Nút Remove.
//                     EditorGUILayout.BeginVertical();
//                     {
//                         if (GUILayout.Button("Remove", GUILayout.Height(58)))
//                         {
//                             list.DeleteArrayElementAtIndex(i);
//                             foldoutStates.RemoveAt(i);
//                         }
//                     }
//                     EditorGUILayout.EndVertical();
//                 }
//                 EditorGUILayout.EndHorizontal();

//                 EditorGUILayout.Space(10);  // Khoảng cách Phần tử cuối cùng với 'box'
//                 EditorGUILayout.EndVertical();

//                 // Khoảng cách giữa 2 item trong list.
//                 EditorGUILayout.Space(7);
//             }

//             // Khoảng cách giữa khối giữ liệu và Button 
//             EditorGUILayout.Space(15);

//             // Nút thêm phần tử
//             if (GUILayout.Button("Add New " + name +  " - " + nameItem + " Spawn", GUILayout.Height(35)))
//             {
//                 list.InsertArrayElementAtIndex(list.arraySize);
//                 foldoutStates.Add(false);
//             }

//             EditorGUI.indentLevel--;
//         }
//     }
// }