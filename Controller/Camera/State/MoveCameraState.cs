using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Xử lý phần di chuyển của camera.
    /// </summary>
    public class MoveCameraState : ICameraState
    {
        private Vector2 m_mousePos;
        private float m_cameraSensivity;
        private int m_screenBorderForMouse;

        private Transform m_cameraMoverTransform;
        private Transform m_cameraDirectionsTransform;


        // ----------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        /////////////////////////////////////////////////////////////

        public MoveCameraState(CameraControllerComp controller)
        {
            m_cameraSensivity            = controller.FunGetCameraSensivity();
            m_cameraMoverTransform       = controller.FunGetTransformCameraMover();
            m_screenBorderForMouse       = controller.FunGetScreenBorderForMouse();
            m_cameraDirectionsTransform  = controller.FunGetTransformCameraDirections();
        }


        // --------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ///////////////////////////////////////////////////////////////////////

        public Enum FunGetTypeState() => TypeCameraState.Move;
        public void FunOnExit() {}
        public void FunOnEnter() {}

        public void FunHandle() 
        {
            m_mousePos = Input.mousePosition;

            // Xử lý di chuyển trái và phải của camera.
            if ((-1 < m_mousePos.x && m_mousePos.x <= m_screenBorderForMouse) ||
                Input.GetKey(KeyCode.LeftArrow) == true)
            {
                var targetPos = m_cameraMoverTransform.position - m_cameraDirectionsTransform.right * m_cameraSensivity;
                m_cameraMoverTransform.position = Vector3.Lerp(m_cameraMoverTransform.position, targetPos, 4.5f * Time.deltaTime);
            }
            else if ((Screen.width - m_screenBorderForMouse <= m_mousePos.x && m_mousePos.x < Screen.width + 1) ||
                     Input.GetKey(KeyCode.RightArrow) == true)
            {
                var targetPos = m_cameraMoverTransform.position + m_cameraDirectionsTransform.right * m_cameraSensivity;
                m_cameraMoverTransform.position = Vector3.Lerp(m_cameraMoverTransform.position, targetPos, 4.5f * Time.deltaTime);
            }

            // Xử lý di chuyển trên và dưới của camera.
            if ((Screen.height - m_screenBorderForMouse <= m_mousePos.y && m_mousePos.y < Screen.height + 1) ||
                Input.GetKey(KeyCode.UpArrow) == true)
            {
                var targetPos = m_cameraMoverTransform.position + m_cameraDirectionsTransform.forward * m_cameraSensivity;
                m_cameraMoverTransform.position = Vector3.Lerp(m_cameraMoverTransform.position, targetPos, 6.5f * Time.deltaTime);
            }
            else if ((-1 < m_mousePos.y && m_mousePos.y <= m_screenBorderForMouse) ||
                Input.GetKey(KeyCode.DownArrow) == true)
            {
                var targetPos = m_cameraMoverTransform.position - m_cameraDirectionsTransform.forward * m_cameraSensivity;
                m_cameraMoverTransform.position = Vector3.Lerp(m_cameraMoverTransform.position, targetPos, 6.5f * Time.deltaTime);
            }
        }
    }
}


// m_cameraMoverTransform.position -= m_cameraDirectionsTransform.right * (m_cameraSensivity * 10f * Time.deltaTime);
// m_cameraMoverTransform.position += m_cameraDirectionsTransform.right * (m_cameraSensivity * 10f * Time.deltaTime);
// m_cameraMoverTransform.position += m_cameraDirectionsTransform.forward * (m_cameraSensivity * 10f * Time.deltaTime);
// m_cameraMoverTransform.position -= m_cameraDirectionsTransform.forward * (m_cameraSensivity * 10f * Time.deltaTime);