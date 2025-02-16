using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Chọn nhanh các unit có trong đội.
    /// </summary>
    public class QuickSelectState : ISelectorState
    {
        private SelectorControllerComp m_controller;   // Điều khiển các đối tượng được chọn
        private HashSet<GameObject> m_selectedUnits;        // Danh sách các đơn vị được chọn.
        private MessagingSystem m_messagingSystem;          // Hệ thống gửi tin nhắn.

        private KeyCode m_quickSelectKey;                   // Phím kích hoạt chế độ chọn nhanh.


        // ---------------------------------------------------------------------------------------
        // CONSTRUCTOR
        // ------------
        // ////////////////////////////////////////////////////////////////////////////////////////

        public QuickSelectState(SelectorControllerComp controller)
        {
            m_controller = controller;
            m_selectedUnits = m_controller.FunGetUnitsSelected();
            m_messagingSystem = MessagingSystem.Instance;
            m_quickSelectKey = KeyCode.Tab;

            // Đăng ký thông điệp nhận đội hình unit khi nhấn chọn
            m_messagingSystem.FunAttachListener(typeof(MessageNeedGetFormUnit), OnUpdateFormationUnit);
        }


        // ---------------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        // ////////////////////////////////////////////////////////////////////////////////////////

        public Enum FunGetTypeState() => TypeSelectorState.QuickSelect;
        public void FunOnEnter() {}
        public void FunOnExit() {}

        public void FunHandle()
        {
            if (m_selectedUnits.Count == ConstantFireNBM.ONE_MEMBER)
                HandleQuickSelect();
        }

        // -----------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////

        // Gửi yêu cầu lấy đội hình khi nhấn phím chọn nhanh.
        // -------------------------------------------------
        private void HandleQuickSelect()
        {
            if (Input.GetKeyDown(m_quickSelectKey) == true)
                m_messagingSystem.FunTriggerMessage(new MessageGetFormUnit(m_selectedUnits.First()), false);
        }


        // -----------------------------------------------------------------------------
        // HANDLE MESSAGE
        // --------------
        // //////////////////////////////////////////////////////////////////////////////

        // Thêm tất cả unit có trong đội hình vào danh sách được chọn chỉ trừ unit đại diện.
        // ---------------------------------------------------------------------------------
        private bool OnUpdateFormationUnit(IMessage message)
        {
            var messageResult = message as MessageNeedGetFormUnit;
            if (messageResult.Formation != null)
            {
                var representativeUnit = m_selectedUnits.First();
                foreach (var unit in messageResult.Formation.FunGetUnitsInForm())
                {
                    if (unit == representativeUnit) continue;

                    m_controller.FunSetSelectedObjcetRTS(unit);
                    m_selectedUnits.Add(unit);
                }
            }
            return true;
        }
    }
}