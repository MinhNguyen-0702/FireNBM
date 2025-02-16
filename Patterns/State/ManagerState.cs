using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     Quản lý các trạng thái của đối tượng.
    /// </summary>
    public class ManagerState
    {
        private IState m_currentState;                  
        private Dictionary<Enum, IState> m_stateMap;


        // ------------------------------------------------------------------------------
        //  CONSTRUCTOR
        // ------------
        // //////////////////////////////////////////////////////////////////////////////

        public ManagerState()
        {
            m_currentState = null;
            m_stateMap = new Dictionary<Enum, IState>();
        }


        // -----------------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Cập nhật trạng thái hiện tại cho đối tượng. </summary>
        /// ----------------------------------------------------------
        public void FunUpdateStateCurrent()
        {
            if (m_currentState != null)
                m_currentState.FunHandle();
        }

        /// <summary>
        ///     Thay đổi trạng thái của đối tượng.</summary>
        /// ------------------------------------------------
        public void FunChangeState(Enum typeState)
        {
            // Thoát nếu vẫn là trạng thái cũ.
            if (m_currentState != null && m_currentState.FunGetTypeState().ToString() == typeState.ToString())
                return;
            
            // Thoát trạng thái hiện tại.
            if (m_currentState != null)
                m_currentState.FunOnExit();
            
            // Tìm kiếm trong map có trạng thái mới không.
            if (m_stateMap.TryGetValue(typeState, out IState state) == true)
            {
                m_currentState = state;
                m_currentState.FunOnEnter();
            }
            else 
            {
                Debug.Log("In ManagerState, Could not find state for: " + typeState);
                m_currentState = null;
            }
        }

        /// <summary>
        ///     Thêm trạng thái mới cho đối tượng.</summary>
        /// ------------------------------------------------
        public bool FunRegisterState(IState state)
        {
            // Thoát nếu trạng thái rỗng hoặc đã có trạng thái này.
            if (state == null || FunCheckState(state.FunGetTypeState()) == true)
            {
                Debug.Log($"In ManagerState, This state '{state.FunGetTypeState()}' is null / has existed.");
                return false;
            }
            
            m_stateMap.Add(state.FunGetTypeState(), state);
            return true;
        }

        /// <summary>
        ///     Xóa một trạng thái ra khỏi đối tượng. </summary>
        /// ----------------------------------------------------
        public bool FunUnregisterState(IState state)
        {
            // Thoát nếu trạng thái rỗng hoặc chưa có trạng thái này.
            if (state == null || FunCheckState(state.FunGetTypeState()) == false)
            {
                Debug.Log($"In ManagerState, This state '{state.FunGetTypeState()}' is null / does not exist.");
                return false;
            }
            
            m_stateMap.Remove(state.FunGetTypeState());
            return true;
        }

        /// <summary>
        ///     Kiểm tra xem trạng thái có tồn tại ko. </summary>
        /// -----------------------------------------------------
        public bool FunCheckState(Enum typeState)
        {
            return m_stateMap.ContainsKey(typeState);
        }
    }
}