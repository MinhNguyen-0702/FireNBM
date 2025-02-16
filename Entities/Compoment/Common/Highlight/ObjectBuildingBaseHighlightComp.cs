using UnityEngine;

namespace FireNBM
{
    public class ObjectBuildingBaseHighlightComp : ObjectTypeBaseHighlightComp
    {
        private Outline m_outline;

        private void Awake()
        {
            m_outline = GetComponent<Outline>();
            if (m_outline == null)
            {
                Debug.Log("Component Outline in Package not assigned");
                return;
            }
            m_outline.enabled = false;
        }


        public override void FunHighlightColor()
        {
            m_outline.enabled = true;
        }
        
        public override void FunSelectedColor()
        {
            m_outline.enabled = true;
        }

        public override void FunCheckColor()
        {
        }

        public override void FunDisableSelectedState()
        {
            m_outline.enabled = false;
        }
    }
}