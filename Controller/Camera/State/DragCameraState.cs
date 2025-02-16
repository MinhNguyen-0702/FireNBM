using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Xử lý trạng thái kéo chuột để di chuyển của camera.
    /// </summary>
    public class DragCameraState : ICameraState
    {
        private bool m_isClicked;                         // Tránh lỗi nhấn chuột chọn unit mà lại chuyển sang chế độ camera.
        private Vector3 m_startPosDrag;                   // Vị trí bắt đầu người chơi kéo.
        private CameraControllerComp m_controller;        // Dùng để lấy vị trí nhấn chuột từ người chơi.


        // ----------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        /////////////////////////////////////////////////////////////

        public DragCameraState(CameraControllerComp controller)
        {
            m_isClicked = false;
            m_controller = controller;
        }


        // ----------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        /////////////////////////////////////////////////////////////

        public Enum FunGetTypeState() => TypeCameraState.Drag;
        public void FunOnEnter() {}
        public void FunOnExit()
        {
            m_isClicked = false;
        }
        
        public void FunHandle()
        {
            // Khi nhấn chuột trái.
            if (Input.GetMouseButtonDown(0))
            {
                m_isClicked = true;
                m_startPosDrag = m_controller.FunGetPosMouseClick();
            }

            // Cập nhật vị trí kéo khi giữ chuột trái.
            if (Input.GetMouseButton(0) && m_isClicked == true)
            {
                Vector3 difference = m_startPosDrag - m_controller.FunGetPosMouseClick();
                m_controller.transform.position += difference;
            }

            // Khi nhả chuột trái.
            if (Input.GetMouseButtonUp(0))
            {
                m_isClicked = false;
            }
        }
    }
}