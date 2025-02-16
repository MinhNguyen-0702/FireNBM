using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Quản lý điều khiển trạng thái camera chính của game.
    /// </summary>
    [AddComponentMenu("FireNBM/Controller/Camera/Camera Controller Comp")]
    public class CameraControllerComp : MonoBehaviour
    {
        private CameraStateComp m_cameraState;                               // Quản lý các trạng thái của camera.
        private Dictionary<KeyCode, TypeCameraState> m_keyToStateMap;        // Quản lý bàn phím tương ứng với trạng thái camera.

        [Header("<u>Transform</u>")]
        [Space()]
        [SerializeField] private Transform m_cameraMoverTransform;            // Tham chiếu đến camera chính của cảnh.
        [SerializeField] private Transform m_cameraMainTransforms;            // Tham chiếu đến camera chính của cảnh.
        [SerializeField] private Transform m_cameraDirectionsTransform;

        [Header("<u>Zoom Camera</u>")]
        [Space()]
        [SerializeField] private float m_zoomSpeed;
        [SerializeField, Range(0f, 50f)] private float m_maxZoom;

        [Header("<u>Move Camera</u>")]
        [Space()]
        [Tooltip("Khi con trỏ chuột di chuyển đến cạnh viền dựa trên phạm vị đã đặt thì lúc đấy camera sẽ di chuyển.")]
        [SerializeField] private int m_screenBorder = 10;
        [SerializeField] private float m_cameraSensivity = 7f;


        // ------------------------------------------------------------------
        // API UNITY 
        // ---------
        /////////////////////////////////////////////////////////////////////

        private void Start()
        {
            m_cameraState = GetComponent<CameraStateComp>();
            DebugUtils.HandleErrorIfNullGetComponent<CameraStateComp, CameraControllerComp>(m_cameraState, this, gameObject);

            // Thêm các trạng thái của camera.
            // m_cameraState.FunRegisterStateCamera(new DragCameraState(this));
            // m_cameraState.FunRegisterStateCamera(new RotationCameraState(this));
            m_cameraState.FunRegisterStateCamera(new MoveCameraState(this));
            m_cameraState.FunRegisterStateCamera(new ZoomCameraState(this));

            // Thiết lập trình điều khiển camera.
            m_keyToStateMap = new Dictionary<KeyCode, TypeCameraState>
            {
                // { KeyCode.LeftControl, TypeCameraState.Rotation },
                // { KeyCode.LeftShift,   TypeCameraState.Drag },
                { KeyCode.LeftAlt,     TypeCameraState.Zoom },
            };
            m_cameraState.FunChangeStateCamera(TypeCameraState.Move);
        }

        private void LateUpdate()
        {
            foreach (var state in m_keyToStateMap)
                ProcessCameraStateInput(state.Key, state.Value);
        }   

        // --------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        /////////////////////////////////////////////////////////////////////// 

        public Vector3 FunGetPosMouseClick() => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        public Transform FunGetTransformCameraMain() => m_cameraMainTransforms;
        public Transform FunGetTransformCameraMover() => m_cameraMoverTransform;
        public Transform FunGetTransformCameraDirections() => m_cameraDirectionsTransform;

        public float FunGetZoomSpeed() => m_zoomSpeed;
        public float FunGetMaxZoom() => m_maxZoom;

        public int FunGetScreenBorderForMouse() => m_screenBorder;
        public float FunGetCameraSensivity() => m_cameraSensivity;


        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        // Hàm trợ giúp, Xử lý trạng thái của camera dựa trên đầu vào.
        // -----------------------------------------------------------
        private void ProcessCameraStateInput(KeyCode keyCode, TypeCameraState typeCameraState)
        {
            if (Input.GetKeyDown(keyCode))
                m_cameraState.FunChangeStateCamera(typeCameraState);

            if (Input.GetKeyUp(keyCode))
                m_cameraState.FunChangeStateCamera(TypeCameraState.Move);
        }
    }
}