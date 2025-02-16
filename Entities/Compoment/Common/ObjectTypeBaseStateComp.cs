using System;
using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Chứa logic chung 
    /// </summary>
    public class ObjectTypeBaseStateComp : MonoBehaviour
    {
        private ManagerState m_managerState;
        private bool m_isActive;


        // ------------------------------------------------------------------
        // API UNITY 
        // ---------
        /////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_isActive = true;
            m_managerState = new ManagerState();
        }

        private void FixedUpdate()
        {
            if (m_isActive == true)
                m_managerState.FunUpdateStateCurrent();
        }


        // --------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        ///////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thay đổi trạng thái của đơn vị.</summary>
        /// ---------------------------------------------
        public void FunChangeState(Enum typeAction) => m_managerState.FunChangeState(typeAction);

        /// <summary>
        ///     Thêm trạng thái mới cho đơn vị.</summary>
        /// ---------------------------------------------
        public void FunRegisterState(IObjectState unitState) => m_managerState.FunRegisterState(unitState);
        
        /// <summary>
        ///     Xóa một trạng thái ra khỏi unit. </summary>
        /// -----------------------------------------------
        public void FunUnregisterState(IObjectState unitState) => m_managerState.FunUnregisterState(unitState);

        /// <summary>
        ///     Kiểm tra xem trạng thái có tồn tại ko. </summary>
        /// -----------------------------------------------------
        public bool FunCheckState(Enum typeActionState) => m_managerState.FunCheckState(typeActionState);

        public void FunSetActive(bool active) => m_isActive = active;
    }
}