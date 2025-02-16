using System;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     Giao diện chung của nút lá.
    ///     Mục tiêu là tìm dữ liệu.
    /// </summary>
    public class CompositeLeaf : Composite
    {
        
        // ----------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        // //////////////////////////////////////////////////////////////////////
    
        public CompositeLeaf(string name, Composite parent, CompositeData data) 
            : base(name, parent, data)
        {
        }

        public CompositeLeaf(CompositeLeaf compositeLeaf) 
            : base(compositeLeaf)
        {
        }
        

        // ---------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////

        public override void FunExecuteAction(Action<Composite> action)
        {
            action?.Invoke(this);
        }   

        public override void FunShutdow()
        {
        }        
    }
}