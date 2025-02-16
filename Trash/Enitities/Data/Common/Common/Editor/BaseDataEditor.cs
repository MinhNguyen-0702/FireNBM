// using FireNBM.Custom;
// using UnityEditor;
// using UnityEngine;

// namespace FireNBM
// {
//     [CustomEditor(typeof(BaseData), true), CanEditMultipleObjects]
//     public class BaseDataEditor : Editor
//     {
//         private SerializedProperty State;
//         private SerializedProperty Controller;
//         private SerializedProperty Name;
//         // private SerializedProperty Level;
//         // private SerializedProperty LevelMultiplier;
//         private SerializedProperty Health;

//         // ----------------------------------------------------------------------------------
//         // API UNITY
//         // ---------
//         // ///////////////////////////////////////////////////////////////////////////////////

//         protected virtual void OnEnable()
//         {
//             State = serializedObject.FindProperty("State");
//             Controller = serializedObject.FindProperty("Controller");
//             // Level = serializedObject.FindProperty("Level");
//             // LevelMultiplier = serializedObject.FindProperty("LevelMultiplier");
//             Health = serializedObject.FindProperty("Health");
//         }


//         // -----------------------------------------------------------------------------------
//         // FUNCTION PROTECTED
//         // ------------------
//         // ////////////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Khởi tạo các thiết lập cơ bản. </summary>
//         /// ---------------------------------------------
//         protected void FunInitialize()
//         {
//         }

//         /// <summary>
//         ///     Cập nhật các thông tin cơ bản. </summary>
//         /// ---------------------------------------------
//         protected void FunUpdateDataDefault(BaseData data)
//         {
//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("Setting Data Default");

//             UpdateDataDetail(data);

//             EditorGUILayout.Space(5);
//             EditorGUILayout.EndVertical(); 
//             FunSpaceGroupBox();
//         }

//         /// <summary>
//         ///     Cập nhật các thay đổi từ target. </summary>
//         /// ----------------------------------------------- 
//         protected void FunSaveDataChanged(BaseData data)
//         {
//             if (GUI.changed)
//             {
//                 EditorUtility.SetDirty(data);   // Thông báo nó đã thay đổi và chưa được lưu.
//                 AssetDatabase.SaveAssets();     // Sẽ lưu vào ổ đĩa tất cả asset chưa được lưu.
//                 AssetDatabase.Refresh();        // Báo cho Unity Editor biết và lưu chúng.
//             }
//         }

//         /// <summary>
//         ///     Khoảng cách giữa hai GroupBox.</summary>
//         /// -------------------------------------------- 
//         protected void FunSpaceGroupBox()
//         {
//             EditorGUILayout.Space(8);
//         }


//         // ---------------------------------------------------------------------------------
//         // FUNCTION HELPER
//         // ---------------
//         // //////////////////////////////////////////////////////////////////////////////////

//         private void UpdateDataDetail(BaseData data)
//         {
//             EditorGUILayout.BeginVertical("GroupBox");

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("State Object", 110);
//             EditorGUILayout.PropertyField(State, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("Controller", 110);
//             EditorGUILayout.PropertyField(Controller, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             // EditorGUILayout.BeginHorizontal();
//             // GUIStyleCustom.Label.FunSetLabel("Level", 110);
//             // EditorGUILayout.PropertyField(Level, GUIContent.none);
//             // EditorGUILayout.EndHorizontal();

//             // EditorGUILayout.BeginHorizontal();
//             // GUIStyleCustom.Label.FunSetLabel("LevelMultiplier", 110);
//             // EditorGUILayout.PropertyField(LevelMultiplier, GUIContent.none);
//             // EditorGUILayout.EndHorizontal();

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("Health", 110);
//             EditorGUILayout.PropertyField(Health, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.Space(3);
//             EditorGUILayout.EndVertical();
//         }
//     }
// }