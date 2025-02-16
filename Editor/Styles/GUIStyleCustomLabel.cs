using UnityEngine;
using UnityEditor;

namespace FireNBM.Custom
{
    public class GUIStyleCustomLabel
    {
        private GUIStyle m_styleTitleGrouptScript;
        private GUIStyle m_styleTitleGroupt;
        private GUIStyle m_styleTitleHeader;
        private GUIStyle m_styleLabel;


        public GUIStyleCustomLabel()
        {
            m_styleTitleGrouptScript = new GUIStyle(GUI.skin.label)
            {
                fixedHeight = 25f,
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 20,
            };

            m_styleTitleGroupt = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontStyle = FontStyle.Bold,
                fontSize = 12,
            };

            m_styleTitleHeader = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold,
            };

            m_styleLabel = new GUIStyle(GUI.skin.label)
            {
                fontSize = 12,
                // margin = new RectOffset(0, 0, 0, 0) // Đặt lề = 0
            };
        }


        /// <summary>
        ///     Thiết lập style cho tiêu đề lớp. </summary>
        /// -----------------------------------------------
        public void FunSetTitleScript(string title)
        {
            EditorGUILayout.BeginVertical("GroupBox");
            EditorGUILayout.BeginVertical("textArea", GUILayout.Height(30));
            EditorGUILayout.LabelField(title, m_styleTitleGrouptScript, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        ///     Thiết lập tiêu đề chính cho GroupBox.</summary>
        /// ---------------------------------------------------
        public void FunSetTitleGroupBox(string label, int spaceTop = 3, TextAnchor textAnchor = TextAnchor.MiddleCenter, int spaceDown = 10)
        {
            EditorGUILayout.Space(spaceTop);
            m_styleTitleGroupt.alignment = textAnchor;
            m_styleTitleGroupt.normal.textColor = new Color(0.670588f, 0.729412f, 0.486274f); 
            EditorGUILayout.LabelField(label, m_styleTitleGroupt);
            EditorGUILayout.Space(spaceDown);

            // Khôi phục màu văn bản về mặc định
            GUI.contentColor = Color.white;
        }

        /// <summary>
        ///     Thiết lập cho Header chính. </summary>
        /// ------------------------------------------ 
        public void FunSetTitleHeader(string label, int sizeWidth = 120)
        {
            // m_styleTitleHeader.normal.textColor = new Color(0.5843137254901961f, 0.6470588235294118f, 0.6509803921568627f);
            EditorGUILayout.LabelField(label, m_styleTitleHeader, GUILayout.Width(sizeWidth));
            // GUI.contentColor = Color.white;
        }

        /// <summary>
        ///     Thiết lập cho label enum. </summary>
        /// ---------------------------------------- 
        public void FunSetLabel(string label, int sizeWidth = 120)
        {
            m_styleLabel.normal.textColor = new Color(0.5843137254901961f, 0.6470588235294118f, 0.6509803921568627f);
            EditorGUILayout.LabelField(label, m_styleLabel, GUILayout.Width(sizeWidth));
            GUI.contentColor = Color.white;
        }
    }
}
