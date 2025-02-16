// using UnityEngine;
// using UnityEditor;
// using System;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Là một lớp chỉnh sửa giao diện trong phần Unity Editor cho lớp ActionUIComponent
//     /// </summary>
//     [CustomEditor(typeof(ActionUIComponent))]
//     public class ActionUIEditor : Editor
//     {
//         // --------------------------------------------------------------
//         // API UNITY
//         // ---------
//         //////////////////////////////////////////////////////////////////
        
//         public override void OnInspectorGUI()
//         {
//             var actionUi = (ActionUIComponent)target;
//             UpdateTypeAction(actionUi);
//             AddButtonUpdate(actionUi);
//         }

//         // --------------------------------------------------------------
//         // FUNCTION HELPER
//         // ---------------
//         //////////////////////////////////////////////////////////////////
        
//         void UpdateTypeAction(ActionUIComponent actionUI)
//         {
//             EditorGUILayout.LabelField("• Type Unit for:   " + actionUI.Mode.ToString().ToUpper());
//             EditorGUILayout.LabelField("--------------");

//             EditorGUILayout.LabelField("");
//             actionUI.Mode = (TypeActionUnit)EditorGUILayout.EnumPopup("Set Mode: ", actionUI.Mode);

//             switch (actionUI.Mode)
//             {
//             case TypeActionUnit.Base: 
//                 actionUI.Base = (TypeActionUnitBase)EditorGUILayout.EnumPopup("Set Action Base: ", actionUI.Base);
//                 break;

//             case TypeActionUnit.Worker:
//                 actionUI.Worker = (TypeActionUnitWorker)EditorGUILayout.EnumPopup("Set Action Worker: ", actionUI.Worker);
//                 break;
 
//             default:
//                 actionUI.Action = TypeActionUnit.None;  
//                 break;
//             }
//         }

//         // Thêm một nút "Cập nhật" với chức năng lưu lại quá trình thay đổi.
//         private void AddButtonUpdate(ActionUIComponent data)
//         {
//             EditorGUILayout.LabelField("");
//             if (GUILayout.Button("Update"))
//             {
//                 // Thông báo nó đã thay đổi và chưa được lưu.
//                 EditorUtility.SetDirty(data);
//                 // Sẽ lưu vào ổ đĩa tất cả asset chưa được lưu.
//                 AssetDatabase.SaveAssets();
//                 // Báo cho Unity Editor biết và lưu chúng.
//                 AssetDatabase.Refresh();
//             }
//         }
//     }
// }