using UnityEngine;

namespace FireNBM
{
    public class ButtonQuitOpitionGameUI : ButtonTypeGameUI
    {
        [SerializeField] private GameObject m_objInfo;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void FunHandleOnclick()
        {
            base.FunPlayOnclick();
            m_objInfo.SetActive(false);
        }
    }
}