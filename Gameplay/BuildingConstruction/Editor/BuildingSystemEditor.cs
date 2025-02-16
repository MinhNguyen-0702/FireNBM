using UnityEngine;
using UnityEditor;
using FireNBM.Custom;
using System.Collections.Generic;

namespace FireNBM
{
    [CustomEditor(typeof(BuildingSystem)), CanEditMultipleObjects]
    public class BuildingSystemEditor : Editor
    {
        SerializedProperty tilemapHint;
        SerializedProperty tilemapOccupied;
        SerializedProperty tileHint;
        SerializedProperty placeable;
        SerializedProperty noPlaceable;    


        private void OnEnable()
        {
            tilemapHint = serializedObject.FindProperty("m_tilemapHint");
            tilemapOccupied = serializedObject.FindProperty("m_tilemapOccupied");

            tileHint = serializedObject.FindProperty("m_tileHint");

            placeable = serializedObject.FindProperty("m_placeable");
            noPlaceable = serializedObject.FindProperty("m_noPlaceable");
        }

        public override void OnInspectorGUI()
        {   
            var style2 = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 12,
            };
            
            GUIStyleCustom.Label.FunSetTitleScript("BUILDING SYSTEM");
            serializedObject.Update();

            // THIẾT LẬP GIÁ TRỊ CHO TILEMAP
            // -----------------------------
            {
                GUILayout.BeginVertical("", "GroupBox");
                EditorGUILayout.LabelField("Setting System", style2);
                EditorGUILayout.Space(8);

                EditorGUILayout.BeginVertical("box");
                EditorGUI.indentLevel++;
                EditorGUILayout.Space(3);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Guide", GUILayout.Width(100)); 
                EditorGUILayout.PropertyField(tilemapHint, GUIContent.none);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Occupancy", GUILayout.Width(100)); 
                EditorGUILayout.PropertyField(tilemapOccupied, GUIContent.none);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space(8);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Tile Hint", GUILayout.Width(100)); 
                EditorGUILayout.PropertyField(tileHint, GUIContent.none);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space(8);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Placeable", GUILayout.Width(100)); 
                EditorGUILayout.PropertyField(placeable, GUIContent.none);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("No Placeable", GUILayout.Width(100)); 
                EditorGUILayout.PropertyField(noPlaceable, GUIContent.none);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Space(3);
                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();

                EditorGUILayout.Space(10);
                EditorGUILayout.EndVertical();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}