using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FireNBM
{
    public class ButtonQuitGameUI : ButtonTypeGameUI
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void FunHandleOnclick()
        {
            base.FunPlayOnclick();

            #if UNITY_EDITOR
            EditorApplication.isPlaying = false; // Dừng Play Mode trong Unity Editor
            #else
            Application.Quit(); // Thoát game khi đã build
            #endif
        }
    }
}