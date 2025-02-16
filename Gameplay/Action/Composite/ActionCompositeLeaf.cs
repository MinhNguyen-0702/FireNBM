using UnityEngine;

namespace FireNBM
{
    [System.Serializable]
    public class ActionCompositeLeaf : ActionComposite
    {
        public ActionCompositeLeaf(ActionRTS action)
            : base(action)
        {
        }
    }
}