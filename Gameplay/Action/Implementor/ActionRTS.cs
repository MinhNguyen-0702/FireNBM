using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Nơi chứa lệnh Action mà unit có. 
    /// </summary>
    [System.Serializable]
    public abstract class ActionRTS
    { 
        public abstract Enum FunGetTypeAction();
        public abstract KeyCode FunGetKeyCodeAction();

        public abstract void FunSetTypeAction(Enum typeAction);
        public abstract void FunSetKeyCodeAction(KeyCode keyInputAction);
    }
}