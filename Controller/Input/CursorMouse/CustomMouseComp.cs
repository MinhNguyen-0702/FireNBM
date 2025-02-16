using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Thay đổi hình dạng con trở chuột.
    /// </summary>
    [AddComponentMenu("FireNBM/Controller/Mouse/Custom Mouse Comp")]
    public class CustomMouseComp : MonoBehaviour
    {
        [SerializeField] private Texture2D m_spriteMouse;
        [SerializeField] private GameObject m_mouseClickPrefab;

        private GameObject m_mouseClickInstance;
        private Camera m_mainCamera;
        private Vector3 m_posClick;


        // -----------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////

        private void Awake()
        {
            // Kiểm tra xem mẫu mouse click đã được người dùng cho vào hay chưa.
            if (m_mouseClickPrefab == null || m_spriteMouse == null)
            {
                Debug.Log("In CustomMouse: Error, mouseClickPrefab or spriteMouse is Null!");
                return;
            }

            // Tạo bản sao của hiệu ứng mouse click.
            m_mouseClickInstance = Instantiate(m_mouseClickPrefab, new Vector3(0f, 0.1f, 0.0f), m_mouseClickPrefab.transform.rotation);
            m_mouseClickInstance.SetActive(false);
        }

        private void Start()
        {
            m_mainCamera = Camera.main;
            m_posClick = Vector3.zero;
            Cursor.SetCursor(m_spriteMouse, Vector2.zero, CursorMode.ForceSoftware);
        }


        // -----------------------------------------------------------------
        // METHOD PUBLIC
        // -------------
        // /////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy vị trí nhấn chuột từ người chơi.</summary>
        /// --------------------------------------------------
        public Vector3 FunGetPosMouseClick()
        {
            Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
            int planeMask = LayerMask.GetMask("Plane");

            if (Physics.Raycast(ray, out RaycastHit hit, 500f, planeMask))
            {
                m_posClick = hit.point;

                // Thiết lập hiệu ứng click chuột trong khoảng 0.25s.
                if (m_mouseClickInstance != null)
                {
                    m_mouseClickInstance.transform.position = m_posClick + new Vector3(0f, 0.1f, 0f);
                    m_mouseClickInstance.SetActive(true);
                    Invoke(nameof(HideMouseClickEffect), 0.25f);
                }
            }
            return m_posClick;
        }

        /// <summary>
        ///     Kiểm tra xem chuột có di chuyển ko. </summary>
        /// --------------------------------------------------
        public bool FunIsMouseMoving() => Input.GetAxis(ConstantFireNBM.MOUSE_X_AXIS) != 0;


        // -----------------------------------------------------------------
        // METHOD HELPER
        // -------------
        // /////////////////////////////////////////////////////////////////

        private void HideMouseClickEffect() => m_mouseClickInstance.SetActive(false);
    }
}