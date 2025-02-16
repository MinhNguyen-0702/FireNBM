using UnityEditor;

namespace FireNBM.Custom
{
    public static class GUIStyleCustom
    {
        public static GUIStyleCustomLabel Label = new GUIStyleCustomLabel();

        /// <summary>
        ///     Khoảng cách giữa hai GroupBox.</summary>
        /// -------------------------------------------- 
        public static void FunSpaceGroupBox()
        {
            EditorGUILayout.Space(8);
        }
    }
}