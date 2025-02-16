// using System;
// using UnityEngine;
// using UnityEditor;
// using FireNBM.Custom;

// namespace FireNBM
// {
//     /// <summary>
//     ///        Lớp Editor prarent chứa các hàm cơ bản về Data Flyweight mà các lớp con có thể sử dụng.
//     /// </summary>
//     [CustomEditor(typeof(FlyweightDataUnit), true), CanEditMultipleObjects]     // 'true' cho phép áp dụng cho lớp kế thừa
//     public class FlyweightDataUnitEditor : Editor
//     {
//         private SerializedProperty Prefab;
//         // private SerializedProperty UnitAction;
//         // private SerializedProperty AnimStates;
//         // private SerializedProperty NameAnimsUnit;


//         // ----------------------------------------------------------------------------------
//         // API UNITY
//         // ---------
//         // ///////////////////////////////////////////////////////////////////////////////////

//         protected virtual void OnEnable()
//         {
//             // Lấy tham chiếu đến các biến. (lỗi nếu khởi tạo GUI ở đây)
//             Prefab = serializedObject.FindProperty("Prefab");
//             // UnitAction = serializedObject.FindProperty("UnitAction");
//             // AnimStates = serializedObject.FindProperty("AnimStates");
//             // NameAnimsUnit = serializedObject.FindProperty("NameAnimsUnit");
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
//         protected void FunUpdateDataDefault(FlyweightDataUnit data)
//         {
//             GUILayout.BeginVertical("", "GroupBox");
//             GUIStyleCustom.Label.FunSetTitleGroupBox("Setting Data Default");

//             UpdateDataPrefab(data);
//             // UpdateDataAction(data);
//             UpdateDataNameAnim(data);

//             EditorGUILayout.Space(5);
//             EditorGUILayout.EndVertical(); 
//             FunSpaceGroupBox();
//         }

//         /// <summary>
//         ///     Khoảng cách giữa hai GroupBox.</summary>
//         /// -------------------------------------------- 
//         protected void FunSpaceGroupBox()
//         {
//             EditorGUILayout.Space(8);
//         }

//         /// <summary>
//         ///     Cập nhật các thay đổi từ target. </summary>
//         /// ----------------------------------------------- 
//         protected void FunSaveDataChanged(FlyweightDataUnit data)
//         {
//             if (GUI.changed)
//             {
//                 EditorUtility.SetDirty(data);   // Thông báo nó đã thay đổi và chưa được lưu.
//                 AssetDatabase.SaveAssets();     // Sẽ lưu vào ổ đĩa tất cả asset chưa được lưu.
//                 AssetDatabase.Refresh();        // Báo cho Unity Editor biết và lưu chúng.
//             }
//         }

//         // ---------------------------------------------------------------------------------
//         // FUNCTION HELPER
//         // ---------------
//         // //////////////////////////////////////////////////////////////////////////////////

//         private void UpdateDataPrefab(FlyweightDataUnit data)
//         {
//             EditorGUILayout.BeginVertical("GroupBox");

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetTitleHeader("Prefab Object");
//             EditorGUILayout.PropertyField(Prefab, GUIContent.none);
//             EditorGUILayout.EndHorizontal();

//             EditorGUILayout.Space(3);
//             EditorGUILayout.EndVertical();
//         }

//         // private void UpdateDataAction(FlyweightDataUnit unitData)
//         // {
//         //     EditorGUILayout.BeginVertical("GroupBox");
//         //     GUIStyleCustom.Label.FunSetTitleHeader("Setup Action State");
//         //     EditorGUILayout.Space(3);
//         //     EditorGUI.indentLevel++;

            
//         //     unitData.UnitAction.Base = (TypeActionUnitBase)EditorGUILayout.EnumFlagsField(unitData.UnitAction.Base);
//         //     unitData.UnitAction.Attribute = (TypeActionUnitAttribute)EditorGUILayout.EnumFlagsField(unitData.UnitAction.Attribute);
//         //     unitData.UnitAction.Ability = (TypeActionUnitAbility)EditorGUILayout.EnumFlagsField(unitData.UnitAction.Ability);


//         //     EditorGUI.indentLevel--;
//         //     EditorGUILayout.Space(8);
//         //     EditorGUILayout.EndVertical();
//         // }

//         private void UpdateDataNameAnim(FlyweightDataUnit unitData)
//         {
//             EditorGUILayout.BeginVertical("GroupBox");
//             GUIStyleCustom.Label.FunSetTitleHeader("Name Anims");
//             EditorGUILayout.Space(3);
//             EditorGUI.indentLevel++;  

//             EditorGUILayout.BeginHorizontal();
//             GUIStyleCustom.Label.FunSetLabel("Select type Anims", 150);
//             unitData.AnimStates = (TypeUnitAnimState)EditorGUILayout.EnumFlagsField(unitData.AnimStates);
//             EditorGUILayout.EndHorizontal();

//             if (unitData.AnimStates != TypeUnitAnimState.None)
//             {
//                 EditorGUILayout.Space(10);
//                 GUIStyleCustom.Label.FunSetLabel("[ Enter the name for the anim selected ]", 250);
//                 EditorGUILayout.Space(3);
//                 EditorGUI.indentLevel++;

//                 // Duyệt qua các loại có trong enum.
//                 foreach (TypeUnitAnimState state in Enum.GetValues(typeof(TypeUnitAnimState)))
//                 {
//                     if (state == TypeUnitAnimState.None)
//                         continue;

//                     // kiểm tra giá trị của unitData có trong enum ko.
//                     if (unitData.AnimStates.HasFlag(state))
//                     {                                      
//                         AnimStateNamePair animPair = unitData.NameAnimsUnit.Find(p => p.AnimState == state);
//                         if (animPair == null)
//                         {
//                             animPair = new AnimStateNamePair { AnimState = state, AnimName = "" };
//                             unitData.NameAnimsUnit.Add(animPair);
//                         }

//                         EditorGUILayout.BeginHorizontal();
//                         GUIStyleCustom.Label.FunSetLabel("• " + state.ToString(), 100);
//                         animPair.AnimName = EditorGUILayout.TextField(animPair.AnimName); 
//                         EditorGUILayout.EndHorizontal();
//                     }
//                 }
//                 EditorGUI.indentLevel--;
//             }

//             EditorGUI.indentLevel--;
//             EditorGUILayout.Space(5);
//             EditorGUILayout.EndVertical();
//         }
//     }
// }




















// // private TEnum HandlerAction<TEnum>(string nameTitle, TEnum typeBase, int sizeWidth = 110) where TEnum : Enum
// // {
// //     EditorGUILayout.BeginHorizontal();
// //     GUIStyleCustom.Label.FunSetLabel(nameTitle, sizeWidth);
// //     typeBase = (TEnum)EditorGUILayout.EnumFlagsField(typeBase);
// //     EditorGUILayout.EndHorizontal();

// //     return typeBase;
// // }

// // m_isUseAllActionBase = EditorPrefs.GetBool(m_isUseAllActionBaseKey, m_isUseAllActionBase);

// // private bool m_isUseAllActionBase;
// //         private string m_isUseAllActionBaseKey = "Editor_m_isUseAllActionBaseKey";


// // switch (unitData.UnitAction.Type)
// //             {
// //                 case TypeActionUnit.Base:
// //                     unitData.UnitAction.ActionsUnitHelper.Base = HandlerAction("Set Actions", unitData.UnitAction.ActionsUnitHelper.Base);
// //                     break;
                
// //                 case TypeActionUnit.Worker:
// //                 {
// //                     unitData.UnitAction.ActionsUnitHelper.Attribute = HandlerAction("Set Actions", unitData.UnitAction.ActionsUnitHelper.Attribute);
                    
// //                     EditorGUILayout.Space(10);
// //                     {
// //                         EditorGUILayout.BeginHorizontal();
// //                         GUIStyleCustom.Label.FunSetLabel("Use Default Action Base", 165);
// //                         m_isUseAllActionBase = EditorGUILayout.Toggle(m_isUseAllActionBase);
// //                         EditorGUILayout.EndHorizontal();

// //                         if (m_isUseAllActionBase == false)
// //                         {
// //                             EditorGUI.indentLevel++;
// //                             unitData.UnitAction.ActionsUnitHelper.Base = HandlerAction("Actions Base", unitData.UnitAction.ActionsUnitHelper.Base, 130);
// //                             EditorGUI.indentLevel--;
// //                         }

// //                         // Lưu trạng thái vào EditorPrefs khi thay đổi
// //                         if (GUI.changed)
// //                             EditorPrefs.SetBool(m_isUseAllActionBaseKey, m_isUseAllActionBase);
                            
// //                         break;
// //                     }
// //                 }
                
// //                 default: 
// //                     break;
// //             }




// // // Nút cập nhật lại dữ liệu.
//         // protected void FunAddButtonUpdate(FlyweightDataUnit data)
//         // {
//         //     EditorGUILayout.LabelField("");
//         //     EditorGUILayout.LabelField("");

//         //     // Căn giữa bắt đầu.
//         //     GUILayout.BeginHorizontal();
//         //     GUILayout.FlexibleSpace();

//         //     // Đặt màu nền tạm thời cho GUI
//         //     GUI.backgroundColor = new Color(0.827451f, 0.945098f, 0.874509f); // Màu xanh lá cây

//         //     if (GUILayout.Button("Update", GUILayout.Width(250), GUILayout.Height(35)))
//         //     {
//         //         EditorUtility.SetDirty(data);   // Thông báo nó đã thay đổi và chưa được lưu.
//         //         AssetDatabase.SaveAssets();     // Sẽ lưu vào ổ đĩa tất cả asset chưa được lưu.
//         //         AssetDatabase.Refresh();        // Báo cho Unity Editor biết và lưu chúng.
//         //     }

//         //     // Khôi phục màu nền mặc định sau khi vẽ nút
//         //     GUI.backgroundColor = Color.white;

//         //     // Căn giữa kết thúc.
//         //     GUILayout.FlexibleSpace();
//         //     GUILayout.EndHorizontal();
//         // }
