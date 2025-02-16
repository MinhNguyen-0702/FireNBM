using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Trạng thái rảnh của đơn vị.
    ///     <para>Đại diện một giá trị từ enum: <see cref="TypeRaceUnitBase"/></para>
    /// </summary>
    public class StateUnitBaseFree : IUnitState
    {
        private UnitControllerComp m_controller;


        // ------------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ///////////////////////////////////////////////////////////////////////////////////

        public StateUnitBaseFree(GameObject owner)
        {
            m_controller = owner.GetComponent<UnitControllerComp>();
            if (m_controller == null)
            {
                Debug.LogError("In StopState. Error, Could not find conponent UnitControllerComponent.");
                return;
            }
        }


        // --------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy tên kiểu của trạng thái Stop. </summary>
        /// ------------------------------------------------
        public Enum FunGetTypeState() => TypeRaceUnitBase.Free;
        public void FunOnEnter() {}
        public void FunHandle() {}

        public void FunOnExit()
        {
            // Chỉ cần vậy là thiết lập trạng thái dừng lại.
            m_controller.FunResetDefaultController();
        }
    }
}