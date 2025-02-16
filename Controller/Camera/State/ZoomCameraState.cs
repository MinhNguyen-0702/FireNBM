using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Xử lý trạng thái zoom của camera.
    /// </summary>
    public class ZoomCameraState : ICameraState
    {
        private float m_zoomValue;                  // Giá trị zoom hiện tại (được cập nhật khi lăn chuột)
        private float m_zoomSpeed;                  // Tốc độ zoom (điều chỉnh độ nhạy của con lăn chuột)
        private float m_maxZoom;                    // Giới hạn zoom tối đa (để camera không vượt quá mức cho phép)

        private Transform m_cameraMainTransform;    // Tham chiếu đến camera chính của game.
        private Vector3 m_startCameraLocalPos;      // Vị trí ban đầu của camera (trong không gian local)


        // --------------------------------------------------------------------
        // CONSTRUCTOR
        // ------------
        ///////////////////////////////////////////////////////////////////////

        public ZoomCameraState(CameraControllerComp controller)
        {
            m_cameraMainTransform = controller.FunGetTransformCameraMain();
            m_zoomSpeed = controller.FunGetZoomSpeed();
            m_maxZoom = controller.FunGetMaxZoom();
            m_startCameraLocalPos = m_cameraMainTransform.transform.localPosition;
            m_zoomValue = 0f;
        }

        // --------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ///////////////////////////////////////////////////////////////////////
        
        public Enum FunGetTypeState() => TypeCameraState.Zoom;
        public void FunOnEnter() {}
        public void FunOnExit() {}
        
        public void FunHandle()
        {
            // 1. Cập nhật giá trị zoom.
            //      - Lấy giá trị từ trục lăn chuột ("Mouse ScrollWheel")
            //      - Nhân giá trị này với tốc độ zoom ('m_zoomSpeed') để tăng/giảm zoom nhanh hơn.
            //      - Dùng 'Mathf.Clamp' để giới hạn giá trị zoom trong khoảng [0, m_maxZooom]
            m_zoomValue = Mathf.Clamp(m_zoomValue + Input.GetAxis(ConstantFireNBM.MOUSE_SCROLL_AXIS) * m_zoomSpeed, 0, m_maxZoom);

            // 2. Tính toán hướng di chuyển của camera.
            //      - Lấy hướng 'forward' của camera trong không gian local (dựa vào trục z)
            //      - Trừ đi 'Vector3.up' để loại bỏ thành phần Y, giúp camera không bị nghiêm khi zoom.
            var localForward = m_cameraMainTransform.InverseTransformDirection(m_cameraMainTransform.forward) - Vector3.up;

            // 3. Tính toán giá trị mục tiêu của camera.
            //      - Vị trí mục tiêu là 'm_startCameraLocalPos' (vị trí ban đầu của cameara)
            //        cộng thêm hướng 'localForward' nhân với giá trị zoom ('m_zoomValue')
            var targetPos = m_startCameraLocalPos + localForward * m_zoomValue;

            // 4. Di chuyển camera tới vị trí mục tiêu bằng nội suy tuyến tính (Vector3.Lerp).
            //      - Tăng dần vị trí camera theo thời gian ('Time.deltaTime') để đảm bảo chuyển động mượt mà.
            m_cameraMainTransform.localPosition = Vector3.Lerp(m_cameraMainTransform.localPosition, targetPos, Time.deltaTime * 5f);
        }     
    }
}  




// private Vector2 m_scrollLimit;    // Giới hạn không gian cho việc zoom gần và zoom xa.
// private float m_scrollSpeed;      // Tốc độ di chuyển của con lăn chuột.
// m_scrollSpeed = 300.0f;
// m_scrollLimit = new Vector2(3.5f, 20f);

// Cho Camera orthographic
// // Lấy trục lăn của chuột.
// float scroll = Input.GetAxis("Mouse ScrollWheel");

// // Cập nhật giá trị qua các khung hình.
// scroll = scroll * m_scrollSpeed * Time.deltaTime;
// // Cập nhật tốc độ zoom của camera.
// m_camera.orthographicSize += scroll;
// // Cập nhật giới hạn mà camera có thể zoom.
// m_camera.orthographicSize = Mathf.Clamp(m_camera.orthographicSize, 
//     m_scrollLimit.x, m_scrollLimit.y);