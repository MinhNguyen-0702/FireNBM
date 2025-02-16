using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Xử lý đơn vị khi di chuột.</summary>
    public class MouseHoverState : ISelectorState
    {
        private Camera m_mainCamera;
        private SelectorControllerComp m_controller;

        // Đánh dấu đối tượng mà chuột đã từng chạm qua.
        private GameObject m_preHighlightUnit;
        private GameObject m_preHighlightBuilding;
        private GameObject m_preHighlightUnderConstruction;

        private float m_idleTime = 0.0f;
        private float m_idleThreshold = 0.5f;


        // ----------------------------------------------------------
        // CONSTRUCTOR 
        // -----------
        // /////////////////////////////////////////////////////////

        public MouseHoverState(SelectorControllerComp controller)
        {
            m_controller = controller;
            m_mainCamera = m_controller.FunGetMainCamera();

            m_preHighlightUnit = null;
            m_preHighlightBuilding = null;
            m_preHighlightUnderConstruction = null;
        }


        // ----------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////

        public Enum FunGetTypeState() => TypeSelectorState.MouseHover;
        public void FunOnEnter() {}
        public void FunOnExit() {}

        public void FunHandle()
        {
            // Dừng cập nhật nếu ko di chuyển chuột sau 1 khoảng thời gian.
            if (IsIdleTimeExceeded() == true)
                return;
            
            Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
            ResetTryHighlightObjectRTS(ray);
            TryHighlightObjectRTS(ray);
        }


        // --------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // //////////////////////////////////////////////////////////////

        // Làm mới trạng thái highlight cho đối tượng trước đó.
        // ---------------------------------------------------- 
        private void ResetTryHighlightObjectRTS(Ray ray)
        {
            ResetHighlightForPreviousObject(ray, ref m_preHighlightUnit, TypeObjectRTS.Unit, ConstantFireNBM.UNIT);
            ResetHighlightForPreviousObject(ray, ref m_preHighlightBuilding, TypeObjectRTS.Building, ConstantFireNBM.BUILDING);
            ResetHighlightForPreviousObject(ray, ref m_preHighlightUnderConstruction, TypeObjectRTS.UnderConstruction, ConstantFireNBM.UNDER_CONSTRUCTION);
        }

        // Highlight đối tượng dựa trên tag cụ thể khi chuột di qua.
        // ----------------------------------------------------------
        private void TryHighlightObjectRTS(Ray ray)
        {
            if (Physics.Raycast(ray, out RaycastHit hit, 600) == false)
                return;
            
            string tagObject = hit.collider.tag;
            switch (tagObject)
            {
                case ConstantFireNBM.UNIT:
                    HandleHoverObjectRTS(ref m_preHighlightUnit, hit.collider.gameObject, TypeObjectRTS.Unit);
                    break;

                case ConstantFireNBM.BUILDING:
                    HandleHoverObjectRTS(ref m_preHighlightBuilding, hit.collider.gameObject, TypeObjectRTS.Building);
                    break;

                case ConstantFireNBM.UNDER_CONSTRUCTION:
                    HandleHoverObjectRTS(ref m_preHighlightUnderConstruction, hit.collider.gameObject, TypeObjectRTS.UnderConstruction);
                    break;

                default:
                    break;
            }
        }

        private void ResetHighlightForPreviousObject(Ray ray, ref GameObject preObject, TypeObjectRTS typeObject, string tag)
        {
            // Nếu đối tượng trước đó không nằm trong danh sách được chọn.
            if (preObject == null || m_controller.FunIsObjectSelector(typeObject, preObject) == true)
                return;
            
            // Bỏ qua nếu người dùng vẫn di chuột ở đối tượng cũ.
            if (Physics.Raycast(ray, out RaycastHit hit, 600) == true &&
                hit.collider.CompareTag(tag) == true &&
                hit.collider.gameObject == preObject)
                return;

            m_controller.FunDisableSelectedStateForObjectRTS(preObject);
            preObject = null;
        }

        private void HandleHoverObjectRTS(ref GameObject preObject, GameObject currObject, TypeObjectRTS typeObject)
        {
            if (preObject != currObject && 
                m_controller.FunIsObjectSelector(typeObject, currObject) == false)
            {
                m_controller.FunSetHighlightObjcetRTS(currObject);
                preObject = currObject;
            }
        }

        private bool IsIdleTimeExceeded()
        {
            if (m_controller.FunIsMouseMoving() == false)
            {
                m_idleTime += Time.deltaTime;
                return m_idleTime >= m_idleThreshold;
            }
            m_idleTime = 0.0f;
            return false;
        }
    }
}