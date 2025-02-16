using System;
using FireNBM.Pattern;
using FireNBM.UI.HUD;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    /// Một lớp tổng quát để quản lý các hành động trong Action HUD (Giao diện hiển thị trên màn hình) dựa trên một loại hành động cụ thể.
    /// Hỗ trợ các loại hành động khác nhau được định nghĩa bởi một enum.
    /// </summary>
    /// <typeparam name="TEnum">Một enum đại diện cho loại hành động được hỗ trợ bởi thành phần HUD này.</typeparam>
    public class ActionTypeHUDComp<TEnum> : ActionHudComp where TEnum : Enum
    {
        /// <summary>
        /// Loại hành động liên kết với thành phần HUD này.
        /// </summary>
        [SerializeField] private TEnum m_typeActionHUD;

        /// Lệnh liên kết với loại hành động, được sử dụng để thực thi hành vi cụ thể.
        private ICommand m_command;


        // ---------------------------------------------------------------------------------
        // API UNITY 
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Khởi tạo lệnh và đăng ký HUD hành động với Hệ thống Hành động. </summary>
        /// -------------------------------------------------------------------------- 
        private void Awake()
        {
            m_command = CommandHandler.FunCreate(m_typeActionHUD);
            ActionSystem.Instance.FunAddActionHUD(m_typeActionHUD, gameObject);
        }

        
        // --------------------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// /// Xử lý sự kiện nhấn chuột cho hành động liên kết trong HUD. </summary>
        /// -------------------------------------------------------------------- 
        public override void FunOnclickAction()
        {
            ActionSystem.Instance.FunHandleOnclickAction(m_typeActionHUD, this);
        }

        /// <summary>
        /// Thực thi lệnh liên kết với loại hành động. </summary>
        /// ---------------------------------------------------- 
        public override void FunOnExecuteAction()
        {
            m_command?.FunExecute();
        }
    }
}
