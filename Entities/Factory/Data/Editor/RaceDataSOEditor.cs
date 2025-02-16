using UnityEngine;
using UnityEditor;
using FireNBM.Custom;
using System.Collections.Generic;

namespace FireNBM
{
    [CustomEditor(typeof(RaceDataSO)), CanEditMultipleObjects]
    public class RaceDataSOEditor : Editor
    {
        private SerializedProperty m_nameRaceRTS;
        private SerializedProperty m_listRaceUnitData;
        private SerializedProperty m_listRaceBuildingData;

         // Lưu trữ trạng thái đóng mở của từng phần tử.
        private List<bool> m_foldoutUnitRaceStates;     
        private List<bool> m_foldoutBuildingRaceStates;


        private void OnEnable()
        {
            m_nameRaceRTS = serializedObject.FindProperty("NameRaceRTS");
            m_listRaceUnitData = serializedObject.FindProperty("ListRaceUnitData");
            m_listRaceBuildingData = serializedObject.FindProperty("ListRaceBuildingData");

            m_foldoutUnitRaceStates = new List<bool>(new bool[m_listRaceUnitData.arraySize]);
            m_foldoutBuildingRaceStates = new List<bool>(new bool[m_listRaceBuildingData.arraySize]);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var boxStyle = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(10, 10, 10, 10),    // Padding bên trong Box
                margin = new RectOffset(5, 5, 5, 5)          // Khoảng cách ngoài Box
            };

            var targetData = (RaceDataSO)target;
            GUIStyleCustom.Label.FunSetTitleScript(targetData.NameRaceRTS.ToString() + " - Data");
            GUILayout.Space(10);

            GUILayout.BeginVertical(boxStyle);
            GUILayout.Label("Name Race", GUILayout.Width(80));
            EditorGUILayout.PropertyField(m_nameRaceRTS, GUIContent.none);
            GUILayout.EndVertical(); 


            GUILayout.BeginVertical("", "GroupBox");
            GUIStyleCustom.Label.FunSetTitleGroupBox("List Unit Race Data", -3, TextAnchor.LowerLeft);
            SetStyleList(m_listRaceUnitData, m_foldoutUnitRaceStates, "Unit");
            GUILayout.EndVertical();

            GUILayout.BeginVertical("", "GroupBox");
            GUIStyleCustom.Label.FunSetTitleGroupBox("List Building Race Data", -3, TextAnchor.LowerLeft);
            SetStyleList(m_listRaceBuildingData, m_foldoutBuildingRaceStates, "Building");
            GUILayout.EndVertical();


            serializedObject.ApplyModifiedProperties();
        }



        // ---------------------------------------------------------------------------------
        // FUNCTION HELPER
        // ---------------
        // //////////////////////////////////////////////////////////////////////////////////

        // Hàm trợ giúp, thay đổi kiểu dáng list.
        private void SetStyleList(SerializedProperty list, List<bool> foldoutStates, string name)
        {
            EditorGUI.indentLevel++;

            // Truy cập các dữ liệu trong item của list.
            SerializedProperty item;
            SerializedProperty itemTypeName;
            SerializedProperty itemListDataSO;

            for (int i = 0; i < list.arraySize; ++i)
            {
                item = list.GetArrayElementAtIndex(i);
                itemTypeName = item.FindPropertyRelative("ObjectRace");
                itemListDataSO = item.FindPropertyRelative("ListDataSO");

                // Tên hiển thị tùy chỉnh
                string displayNameEnum = itemTypeName.enumDisplayNames[itemTypeName.enumValueIndex];
                string displayName = (i + 1) + ".  " + displayNameEnum;

                EditorGUILayout.BeginVertical("box");
                foldoutStates[i] = EditorGUILayout.Foldout(foldoutStates[i], displayName, true);

                if (foldoutStates[i] == false)
                {
                    EditorGUILayout.EndVertical();
                    continue;
                }
                EditorGUILayout.Space(7);

                // Tạo thành 2 khối bố cục.
                EditorGUILayout.BeginHorizontal();
                {
                    // Dành cho dữ liệu spawn.
                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUIStyleCustom.Label.FunSetLabel("Name", 41);
                        EditorGUILayout.PropertyField(itemTypeName, GUIContent.none);
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.BeginHorizontal();
                        GUIStyleCustom.Label.FunSetLabel("Data", 55);
                        EditorGUILayout.PropertyField(itemListDataSO, GUIContent.none);
                        EditorGUILayout.EndHorizontal();
                    }
                    EditorGUILayout.EndVertical();

                    // Cho Nút Remove.
                    EditorGUILayout.BeginVertical();
                    {
                        if (GUILayout.Button("Remove", GUILayout.Height(38)))
                        {
                            list.DeleteArrayElementAtIndex(i);
                            foldoutStates.RemoveAt(i);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space(10);  // Khoảng cách Phần tử cuối cùng với 'box'
                EditorGUILayout.EndVertical();

                // Khoảng cách giữa 2 item trong list.
                EditorGUILayout.Space(7);
            }

            // Khoảng cách giữa khối giữ liệu và Button 
            EditorGUILayout.Space(10);

            // Nút thêm phần tử
            if (GUILayout.Button("Add New " + name, GUILayout.Height(35)))
            {
                list.InsertArrayElementAtIndex(list.arraySize);
                foldoutStates.Add(false);
            }

            EditorGUI.indentLevel--;
        }
    }
}