using UnityEngine;

namespace FireNBM
{
    public abstract class ButtonTypeGameUI : MonoBehaviour
    {
        [SerializeField] private AudioSource m_audioSourceClick;

        protected virtual void Awake()
        {
            if (m_audioSourceClick == null)
            {
                DebugUtils.FunLog("Audio Source for click is Null");
                return;
            }
            if (m_audioSourceClick.clip == null)
            {
                DebugUtils.FunLog("Audio Clip for click is Null");
                return;
            }
        }

        protected void FunPlayOnclick()
        {
            m_audioSourceClick.Play();
        }

        public abstract void FunHandleOnclick();
    }
}