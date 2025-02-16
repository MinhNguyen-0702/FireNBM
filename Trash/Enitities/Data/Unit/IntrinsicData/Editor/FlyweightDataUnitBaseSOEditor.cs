// using FireNBM.Custom;
// using UnityEditor;
// using UnityEngine;

// namespace FireNBM
// {
//     [CustomEditor(typeof(FlyweightDataUnitBaseSO))]
//     public class FlyweightDataUnitBaseEditor : FlyweightDataUnitEditor
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

//             GUIStyleCustom.Label.FunSetTitleScript("Unit Base - Flyweight Data");
//             UpdateInspectorGUI();

//             base.FunSaveDataChanged((FlyweightDataUnit)target);
//             serializedObject.ApplyModifiedProperties(); 
//         }


//         // ---------------------------------------------------------------------------------
//         // FUNCTION HELPER
//         // ---------------
//         // //////////////////////////////////////////////////////////////////////////////////

//         private void UpdateInspectorGUI()
//         {
//             var unitData = (FlyweightDataUnitBaseSO)target;
//             base.FunUpdateDataDefault(unitData);
//             UpdateDataUnitBase(unitData);
//         }


//         private void UpdateDataUnitBase(FlyweightDataUnitBaseSO dataUnitBase)
//         {
//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("Setting - Unit Base");

//             UpdateController(dataUnitBase);

//             EditorGUILayout.Space(8);
//             EditorGUILayout.EndVertical(); 
//             base.FunSpaceGroupBox();
//         }

//         private void UpdateController(FlyweightDataUnitBaseSO dataUnitBase)
//         {
//             EditorGUILayout.BeginVertical("GroupBox");

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetTitleHeader("Controller Type");
//             dataUnitBase.Controller = (TypeController)EditorGUILayout.EnumPopup(dataUnitBase.Controller);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.Space(5);
//             EditorGUILayout.EndVertical();
//         }
//     }
// }
