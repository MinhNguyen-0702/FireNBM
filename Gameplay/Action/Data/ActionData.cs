using System;
using UnityEngine;
using System.Collections.Generic;

namespace FireNBM
{
    /// <summary>
    ///     
    /// </summary>
    public abstract class ActionData : ScriptableObject
    {
        // Chỉ được dùng trong Unity Editor.
        public bool ShowDropdown = false;
        public bool IsCompositeActionLeaf = false;
        public bool IsCompositeActionGroup = false;

        [SerializeReference] private ActionCompositeGroup m_root = new ActionCompositeGroup(null);
        [SerializeReference] public ActionCompositeGroup ActionGroupChild;
        [SerializeReference] public List<ActionCompositeGroup> ListActionGroup = new List<ActionCompositeGroup>();

        // -----------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // //////////////////////////////////////////////////////////////////////


        public void FunAddAction(ActionComposite composite)    => m_root.FunAddActionChild(composite);
        public void FunRemoveAction(ActionComposite composite) => m_root.FunRemoveActionChild(composite);

        public ActionCompositeGroup FunGetRoot() => m_root;
        public List<ActionComposite> FunGetMemeber() => m_root.FunGetChild();
        public List<ActionCompositeGroup> FunGetListActionGroup() => ListActionGroup;

        public bool FunRemoveActionGroup(ActionCompositeGroup action)
        {
            if (ListActionGroup.Exists(
                actionCompoiste => actionCompoiste.FunGetAction() == action.FunGetAction()))
            {
                ListActionGroup.Remove(action);
                return true;
            }
            return false;
        }


        // -----------------------------------------------------------------------
        // FUNCTION ABSTRACT
        // -----------------
        // //////////////////////////////////////////////////////////////////////

        public abstract ActionRTS FunCreateAction(Enum type);
    } 
}