// using UnityEngine;
// using UnityEditor;
// using FireNBM.Custom;

// namespace FireNBM
// {
//     [CustomEditor(typeof(SpawnDataUnitSO)), CanEditMultipleObjects]
//     public class SpawnDataUnitSOEditor : Editor
//     {
//         private SerializedProperty PrefabUnit;
//         private SerializedProperty Data;


//         private void OnEnable()
//         {
//             PrefabUnit = serializedObject.FindProperty("PrefabUnit");
//             Data = serializedObject.FindProperty("Data");
//         }

//         public override void OnInspectorGUI()
//         {
//             GUIStyleCustom.Label.FunSetTitleScript("Unit - Spawn Data");
//             serializedObject.Update();
//             EditorGUILayout.BeginVertical("GroupBox");

//             {
//                 GUIStyleCustom.Label.FunSetTitleGroupBox("Setting Data");

//                 EditorGUILayout.BeginHorizontal();
//                 GUIStyleCustom.Label.FunSetLabel("Prefab Unit");
//                 EditorGUILayout.PropertyField(PrefabUnit, GUIContent.none);
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