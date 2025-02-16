using System;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     Mẫu Trạng thái.
    /// </summary>
    public interface IState
    {
        /// <summary> 
        ///     Định nghĩa hành vi của tác nhân khi nó ở trạng thái hiện tại.</summary>
        /// -------------------------------------------------------------------------
        public void FunHandle();

        /// <summary> 
        ///     Xử lý chuyển đổi tác nhân, khi một trạng thái đi vào.</summary>
        /// ---------------------------------------------------------------- 
        public void FunOnEnter();

        /// <summary> 
        ///     Xử lý chuyển đổi tác nhân, khi một trạng thái đi ra.</summary>
        /// ----------------------------------------------------------------
        public void FunOnExit();

        /// <summary> 
        ///     Xác định tên của trạng thái.</summary>
        /// ------------------------------------------ 
        public Enum FunGetTypeState();
    }
}