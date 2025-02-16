using UnityEngine;

namespace FireNBM
{
    public abstract class ObjectTypeBaseHighlightComp : MonoBehaviour
    {
        /// <summary>
        ///     Thiết lập màu sắc highlight cho chủ nhân khi người dùng di chuột đến.</summary>
        /// ----------------------------------------------------------------------------------
        public abstract void FunHighlightColor();
        
        /// <summary>
        ///     Thiết lập màu sắc selector cho chủ nhân khi người dùng nhấn chuột.</summary>
        /// --------------------------------------------------------------------------------
        public abstract void FunSelectedColor();

        /// <summary>
        ///     Thiết lập màu sắc check cho chủ nhân khi người dùng nhấn chuột để kiểm tra thông tin đối phương.</summary>
        /// --------------------------------------------------------------------------------------------------------------
        public abstract void FunCheckColor();


        /// <summary>
        ///     Tắt trạng thái được chọn cho đối tượng cha. </summary>
        /// ----------------------------------------------------------
        public abstract void FunDisableSelectedState();
    }
}