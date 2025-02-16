using UnityEngine;
using FireNBM.Pattern;


namespace FireNBM
{
    /// <summary>
    ///     Quản lý các trạng thái lựa chọn đối tượng có trong game.
    /// </summary>
    [AddComponentMenu("FireNBM/Selector/Selector State Comp")]
    public class SelectorStateComp : MonoBehaviour
    {
        // Quản lý các trạng thái lựa chọn của đối tượng.
        private ManagerState m_managerState;

        /// <summary> Cho biết có nên cập nhật trạng thái không. </summary>
        public bool Active;

        // ---------------------------------------------------------------------------------
        // API UNITY 
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_managerState = new ManagerState();
            Active = true;
        }

        private void Update()
        {
            if (Active == true)
                m_managerState.FunUpdateStateCurrent();
        }


        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        ///     Thay đổi trạng thái lựa chọn của đối tượng. </summary>
        /// ----------------------------------------------------------
        public void FunChangeStateSelecter(TypeSelectorState typeSelector) => m_managerState.FunChangeState(typeSelector);

        /// <summary>
        ///     Thêm trạng thái lựa chọn đối tượng mới. </summary>
        /// ------------------------------------------------------
        public bool FunRegisterStateSelecter(ISelectorState newStateSelector) => m_managerState.FunRegisterState(newStateSelector);

        /// <summary>
        ///     Kiểm tra xem có trạng thái lựa chọn này ko. </summary>
        /// -----------------------------------------------------------
        public bool FunCheckStateSelecter(TypeSelectorState typeSelector) => m_managerState.FunCheckState(typeSelector);
    }
}