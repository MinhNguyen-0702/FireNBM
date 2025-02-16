using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Đây là lớp cơ sở để tạo ra các action cho đối tượng.
    /// </summary>
    /// <typeparam name="TEnum">Một danh sách enum chứa các lệnh action mà đối tượng có thể thực hiện.</typeparam>
    [System.Serializable]
    public class ActionTypeObjectRTS<TEnum> : ActionRTS where TEnum : Enum
    {
        [SerializeField] private TEnum m_typeAction;
        [SerializeField] private KeyCode m_keyInputAction;

        public ActionTypeObjectRTS(TEnum typeAction, KeyCode keyInputAction)
        {
            m_typeAction = typeAction;
            m_keyInputAction = keyInputAction;
        }

        public override Enum FunGetTypeAction() => m_typeAction;
        public override KeyCode FunGetKeyCodeAction() => m_keyInputAction;

        public override void FunSetTypeAction(Enum typeAction) => m_typeAction = (TEnum)typeAction;
        public override void FunSetKeyCodeAction(KeyCode keyInputAction) => m_keyInputAction = keyInputAction;
    }
}