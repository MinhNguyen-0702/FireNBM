using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    [System.Serializable]
    public class ActionCompositeGroup : ActionComposite
    {
        [SerializeReference]
        private List<ActionComposite> m_childrens;

        
        // ----------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        // //////////////////////////////////////////////////////////////////////

        public ActionCompositeGroup(ActionRTS action)
            : base(action)
        {
            m_childrens = new List<ActionComposite>();
        }


        // ---------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thêm một node con vào nhóm. </summary>
        /// -------------------------------------------
        public void FunAddActionChild(ActionComposite child)
        {
            if (child == null)
            {
                Debug.LogError("A ActionComposite is Null when add to ActionCompositeGroup!");
                return;
            } 

            // Thoát nếu nó đã tồn tại.
            if (FunCheckActionComposite(child) == true)
            {
                Debug.LogWarning("Value created has exists!");
                return;
            }
            m_childrens.Add(child);
        }
        
        /// <summary>
        ///     Thêm một node con vào nhóm. </summary>
        /// -------------------------------------------
        public void FunRemoveActionChild(ActionComposite child)
        {
            if (child == null)
            {
                Debug.LogError("A ActionComposite is Null when Remove to ActionCompositeGroup!");
                return;
            } 

            if (FunCheckActionComposite(child) == false)
            {
                Debug.LogWarning("Could not find value composite to remove");
                return;
            }
            m_childrens.Remove(child);
        }

        /// <summary>
        ///     Kiểm tra xem một ActionComposite đã tồn tại trong danh sách chưa.</summary>
        /// ------------------------------------------------------------------------------- 
        public bool FunCheckActionComposite(ActionComposite value)
        {
            return m_childrens.Exists(composite => composite.FunGetAction() == value.FunGetAction());
        }

        public List<ActionComposite> FunGetChild() => m_childrens;
    } 
}