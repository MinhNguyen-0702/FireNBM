using UnityEngine;

namespace FireNBM
{
    [System.Serializable]
    public abstract class ActionComposite
    {
        [SerializeReference] private ActionRTS m_action;

        public ActionComposite(ActionRTS action) => m_action = action;
        public ActionRTS FunGetAction() => m_action;
    }
}