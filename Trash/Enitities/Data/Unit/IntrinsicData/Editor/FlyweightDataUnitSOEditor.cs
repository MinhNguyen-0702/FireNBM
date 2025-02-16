// using UnityEditor;
// using UnityEngine;
// using FireNBM.Custom;

// namespace FireNBM
// {
//     [CustomEditor(typeof(FlyweightDataUnitSO))]
//     public class FlyweightDataUnitSOEditor : FlyweightDataUnitEditor
//     {

//         // ----------------------------------------------------------------------------------
//         // API UNITY
//         // ---------
//         // ///////////////////////////////////////////////////////////////////////////////////
    
//         protected override void OnEnable()
//         {
//             base.OnEnable();
//         }

//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update();
//             base.FunInitialize();
            
//             GUIStyleCustom.Label.FunSetTitleScript("Unit - Flyweight Data");
//             UpdateInspectorGUI();

//             base.FunSaveDataChanged((FlyweightDataUnitSO)target);
//             serializedObject.ApplyModifiedProperties();
//         }


//         // ---------------------------------------------------------------------------------
//         // FUNCTION HELPER
//         // ---------------
//         // //////////////////////////////////////////////////////////////////////////////////

//         private void UpdateInspectorGUI()
//         {
//             var unitData = (FlyweightDataUnitSO)target;
//             base.FunUpdateDataDefault(unitData);
//             UpdateDataUnitMage(unitData);
//         }

//         private void UpdateDataUnitMage(FlyweightDataUnitSO dataUnitMage)
//         {
//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("Setting Unit");

//             UpdateController(dataUnitMage);

//             EditorGUILayout.Space(8);
//             EditorGUILayout.EndVertical(); 
//             base.FunSpaceGroupBox();
//         }

//         private void UpdateController(FlyweightDataUnitSO dataUnitMage)
//         {
//             EditorGUILayout.BeginVertical("GroupBox");

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetTitleHeader("Type Name Unit");
//             dataUnitMage.TypeUnit = (TypeNameUnit)EditorGUILayout.EnumPopup(dataUnitMage.TypeUnit);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.Space(5);
//             EditorGUILayout.EndVertical();
//         }
//     }
// }
