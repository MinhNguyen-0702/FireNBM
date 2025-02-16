using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Lớp này chịu trách nhiệm điều chỉnh camera chính 
    /// </summary>
    [AddComponentMenu("FireNBM/Controller/Camera/Camera State Comp")]
    public class CameraStateComp : MonoBehaviour
    {
        public bool ActiveCamera { get; set; }
        private ManagerState m_managerState;


        // -------------------------------------------------------------------------------
        // API UNITY 
        // ---------
        //////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            ActiveCamera = true; 
            m_managerState = new ManagerState(); 
        } 

        private void LateUpdate()
        {
            if (ActiveCamera == true)
                m_managerState.FunUpdateStateCurrent();
        }


        // ------------------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thay đổi trạng thái camera.</summary>
        ///------------------------------------------
        public void FunChangeStateCamera(TypeCameraState typeCamera) => m_managerState.FunChangeState(typeCamera);

        /// <summary>
        ///     Thêm trạng thái mới cho camera. </summary>
        /// ---------------------------------------------- 
        public bool FunRegisterStateCamera(ICameraState newStateCamera) => m_managerState.FunRegisterState(newStateCamera);

        /// <summary>
        ///     Kiểm tra xem camera có trạng thái này ko. </summary>
        /// -------------------------------------------------------
        public bool FunCheckStateCamera(TypeCameraState typeCamera) => m_managerState.FunCheckState(typeCamera);
    }
} 