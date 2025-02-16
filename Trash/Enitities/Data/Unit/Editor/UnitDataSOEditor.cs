// using UnityEngine;
// using UnityEditor;
// using FireNBM.Custom;

// namespace FireNBM
// {
//     [CustomEditor(typeof(UnitDataSO)), CanEditMultipleObjects]
//     public class UnitDataSOEditor : BaseDataEditor
//     {
//         private SerializedProperty NameUnit;
//         private SerializedProperty Attack;
//         private SerializedProperty Defense;
//         private SerializedProperty WalkSpeed;
//         private SerializedProperty AttackSpeed;
//         private SerializedProperty RangeAttack;

//         // ----------------------------------------------------------------------------------
//         // API UNITY
//         // ---------
//         // ///////////////////////////////////////////////////////////////////////////////////

//         protected override void OnEnable()
//         {
//             base.OnEnable();
//             NameUnit = serializedObject.FindProperty("NameUnit");
//             Attack = serializedObject.FindProperty("Attack");
//             Defense = serializedObject.FindProperty("Defense");
//             WalkSpeed = serializedObject.FindProperty("WalkSpeed");
//             AttackSpeed = serializedObject.FindProperty("AttackSpeed");
//             RangeAttack = serializedObject.FindProperty("RangeAttack");
//         }

//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update(); 
//             base.FunInitialize();

//             GUIStyleCustom.Label.FunSetTitleScript("Unit Data");
//             UpdateInspectorGUI();

//             // base.FunSaveDataChanged((UnitDataSO)target);
//             serializedObject.ApplyModifiedProperties();
//         }

//         // ---------------------------------------------------------------------------------
//         // FUNCTION HELPER
//         // ---------------
//         // //////////////////////////////////////////////////////////////////////////////////

//         private void UpdateInspectorGUI()
//         {
//             var unitData = (UnitDataSO)target;
//             base.FunUpdateDataDefault(unitData);
//             UpdateDataUnit(unitData);
//         }

//         private void UpdateDataUnit(UnitDataSO dataUnit)
//         {
//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("Setup Data Unit");

//             UpdateDataDetail(dataUnit);

//             EditorGUILayout.Space(8);
//             EditorGUILayout.EndVertical(); 
//             base.FunSpaceGroupBox();
//         }

//         private void UpdateDataDetail(UnitDataSO dataUnit)
//         {
//             EditorGUILayout.BeginVertical("GroupBox");

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("NameUnit", 110);
//             EditorGUILayout.PropertyField(NameUnit, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("Attack", 110);
//             EditorGUILayout.PropertyField(Attack, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("Defense", 110);
//             EditorGUILayout.PropertyField(Defense, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("WalkSpeed", 110);
//             EditorGUILayout.PropertyField(WalkSpeed, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("AttackSpeed", 110);
//             EditorGUILayout.PropertyField(AttackSpeed, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("RangeAttack", 110);
//             EditorGUILayout.PropertyField(RangeAttack, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.Space(5);
//             EditorGUILayout.EndVertical();
//         }
//     }
// }