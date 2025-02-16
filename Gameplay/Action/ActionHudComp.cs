using UnityEngine;

namespace FireNBM
{
    /// <summary>
    /// Lớp cơ sở trừu tượng đại diện cho một thành phần xử lý các hành động trong Action HUD (Giao diện hiển thị trên màn hình).
    /// Cung cấp cấu trúc để định nghĩa hành vi khi nhấn và thực thi các hành động.
    /// </summary>
    public abstract class ActionHudComp : MonoBehaviour
    {
        /// <summary>
        /// Được gọi khi hành động liên kết được nhấn trong HUD.
        /// Triển khai phương thức này để định nghĩa điều gì xảy ra khi hành động được chọn. </summary>
        /// ------------------------------------------------------------------------------------------- 
        public abstract void FunOnclickAction();

        /// <summary>
        /// Được gọi khi hành động liên kết được thực thi trong HUD.
        /// Triển khai phương thức này để định nghĩa hành vi của hành động khi được thực hiện. </summary>
        /// ----------------------------------------------------------------------------------------------
        public abstract void FunOnExecuteAction();
    }
}
