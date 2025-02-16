// using UnityEngine;
// using UnityEditor;
// using FireNBM.Custom;

// namespace FireNBM
// {
//     [CustomEditor(typeof(SpawnDataBuildingSO)), CanEditMultipleObjects]
//     public class SpawnDataBuildingSOEditor : Editor
//     {
//         private SerializedProperty PrefabBuilding;
//         private SerializedProperty Data;


//         private void OnEnable()
//         {
//             PrefabBuilding = serializedObject.FindProperty("PrefabBuilding");
//             Data = serializedObject.FindProperty("Data");
//         }

//         public override void OnInspectorGUI()
//         {
//             GUIStyleCustom.Label.FunSetTitleScript("Building - Spawn Data");
//             serializedObject.Update();
//             EditorGUILayout.BeginVertical("GroupBox");

//             {
//                 GUIStyleCustom.Label.FunSetTitleGroupBox("Setting Data");

//                 EditorGUILayout.BeginHorizontal();
//                 GUIStyleCustom.Label.FunSetLabel("Prefab Building");
//                 EditorGUILayout.PropertyField(PrefabBuilding, GUIContent.none);
//                 EditorGUILayout.EndHorizontal();

//                 EditorGUILayout.BeginHorizontal();
//                 GUIStyleCustom.Label.FunSetLabel("Data");
//                 EditorGUILayout.PropertyField(Data, GUIContent.none);
//                 EditorGUILayout.EndHorizontal();          
//             }

//             EditorGUILayout.Space(3);
//             EditorGUILayout.EndVertical();
//             serializedObject.ApplyModifiedProperties();
//         }
//     }
// }