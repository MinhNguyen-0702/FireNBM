using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Xử lý đơn vị khi kéo chuột.
    /// </summary>
    public class MouseDragState : ISelectorState
    {
        private Camera m_mainCamera;
        private SelectorControllerComp m_controller;

        private List<GameObject> m_units;
        private HashSet<GameObject> m_selectedUnits;
        private List<GameObject> m_highlightedUnits;

        // Tọa độ 4 góc của hình chữ nhật trong 3D (TL-TR-BL-BR)                 
        private Vector3[] m_cornersRect = new Vector3[4];  

        // Tạo mặt phẳng ảo bằng vector pháp tuyến và 1 điểm (0, 0, 0) để tối ưu hóa
        // trong quá trình tạo hình chữ nhật. 
        private Plane m_groundPlane;


        // ---------------------------------------------------------------------------------
        // CONSTRUCTOR 
        // -----------
        // /////////////////////////////////////////////////////////////////////////////////

        public MouseDragState(SelectorControllerComp controller)
        {
            m_controller = controller;
            m_units = m_controller.FunGetUnits();
            m_selectedUnits = controller.FunGetUnitsSelected();
            m_mainCamera = m_controller.FunGetMainCamera();

            m_highlightedUnits = new List<GameObject>(50);
            m_groundPlane = new Plane(Vector3.up, Vector3.zero);
        }


        // ---------------------------------------------------------------------------------
        // FUNCTION PUBLIC 
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////
        
        public Enum FunGetTypeState() => TypeSelectorState.MouseDrag;
        public void FunOnEnter() {}
        public void FunOnExit() {}

        public void FunHandle()
        {
            if (Input.GetMouseButton(0) == true)
                HandleDragging();

            if (Input.GetMouseButtonUp(0) == true)
                HandleRelease();
        }

        /// <summary>
        ///     Các đối tượng được người chơi chọn.</summary>
        /// -------------------------------------------------
        public HashSet<GameObject> FunGetUnitsSelected() => m_selectedUnits;

        /// <summary>
        ///     Xóa các đối tượng được highlight khi chuyển chế độ camera.</summary>
        /// ------------------------------------------------------------------------
        public void FunResetDrag()
        {
            foreach (GameObject unit in m_highlightedUnits)
                m_controller.FunDisableSelectedStateForObjectRTS(unit);

            m_highlightedUnits.Clear();
        }


        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        // Highlight những đơn vị nào nằm trong hình khi chúng ta kéo chuột ko.
        // --------------------------------------------------------------------
        private void HandleDragging()
        {
            CreateRectangle();
            m_highlightedUnits.Clear();

            // Kiểm tra tất cả unit có trong scene.
            foreach (GameObject unit in m_units)
            {
                if (m_selectedUnits.Contains(unit))
                    continue;

                if (IsWithinRectangle(unit.transform.position) == true)
                {
                    m_controller.FunSetHighlightObjcetRTS(unit);
                    m_highlightedUnits.Add(unit);
                }
                else 
                    m_controller.FunDisableSelectedStateForObjectRTS(unit);
            }
        }

        // Nếu thả chuột, đánh dấu các đơn vị trong HCM mà chúng ta vừa tạo.
        // -----------------------------------------------------------------
        private void HandleRelease()
        {
            if (m_highlightedUnits.Count == 0)
                return;

            // Đại diện cho các lệnh hành động chung cho unit.
            GameObject unitAction = m_highlightedUnits.First();
            var firstActionUnit = unitAction.GetComponent<UnitFlyweightComp>().FunGetNameUnit();
            bool isChange = false;

            foreach (GameObject unit in m_highlightedUnits)
            {
                // Đánh dấu đã chọn và thêm vào danh sách.
                m_controller.FunSetSelectedObjcetRTS(unit);
                m_selectedUnits.Add(unit);

                if (isChange == true)
                    continue;

                var unitFlyweightComp = unit.GetComponent<UnitFlyweightComp>();
                if (firstActionUnit != unitFlyweightComp.FunGetNameUnit())
                {
                    unitAction = unit;
                    isChange = true;
                }
            } 
            m_highlightedUnits.Clear();

            if (isChange == false)
                m_controller.FunUpdateHUD(TypeObjectRTS.Unit, false);
            else
                m_controller.FunUpdateHUD(TypeObjectRTS.Unit, true);
        }

        // Đơn vị có nằm trong một đa giác được xác định bởi 4 góc hay không.
        // -----------------------------------------------------------------
        private bool IsWithinRectangle(Vector3 unitPos)
        {
            // Tam giác bên trái.
            if (IsWithinTriangle(unitPos, m_cornersRect[0], m_cornersRect[2], m_cornersRect[3]) == true)
                return true;

            // Tam giác bên phải.
            if (IsWithinTriangle(unitPos, m_cornersRect[0], m_cornersRect[3], m_cornersRect[1]) == true)
                return true;
 
            return false;
        }

        // Một điểm có nằm trong tam giác không. (x, y) to (x, z)
        private bool IsWithinTriangle(Vector3 p, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            // Tính mẫu số.
            float denominator = (p2.z - p3.z)*(p1.x - p3.x) + (p3.x - p2.x)*(p1.z - p3.z);

            // Tính hệ số a, b, c
            float a = ((p2.z - p3.z)*(p.x - p3.x) + (p3.x - p2.x)*(p.z - p3.z)) / denominator;
            float b = ((p3.z - p1.z)*(p.x - p3.x) + (p1.x - p3.x)*(p.z - p3.z)) / denominator;
            float c = 1 - a - b;

            // Điểm có nằm trong tam giác ko
            return ((0f <= a && a <= 1f) && (0f <= b && b <= 1f) && (0f <= c && c <= 1f));
        }

        // Tạo HCN biển thị vùng được chọn.
        // -------------------------------
        private void CreateRectangle()
        {
            Vector3 rectStartPos = m_controller.FunGetStartMousePos();
            Vector3 rectEndPos = Input.mousePosition;

            Vector3 middle = (rectStartPos + rectEndPos) / 2.0f;

            float halfWidth = Mathf.Abs(rectStartPos.x - rectEndPos.x) * 0.5f;
            float halfHeight = Mathf.Abs(rectStartPos.y - rectEndPos.y) * 0.5f;

            // Tính tọa độ 4 góc của hình chữ nhật trên màn hình.
            Vector3[] screenCorners = 
            {
                new Vector3(middle.x - halfWidth, middle.y + halfHeight), // Top - Left
                new Vector3(middle.x + halfWidth, middle.y + halfHeight), // Top - Right
                new Vector3(middle.x - halfWidth, middle.y - halfHeight), // Bottom - Left
                new Vector3(middle.x + halfWidth, middle.y - halfHeight)  // Bottom - Right
            };
            
            // Tạo các tia từ camera.
            int count = 0;
            foreach (Vector3 corner in screenCorners)
            {
                Ray ray = m_mainCamera.ScreenPointToRay(corner);
                if (m_groundPlane.Raycast(ray, out float distance))
                {
                    m_cornersRect[count++] = ray.GetPoint(distance);
                }
            }
        }
    }
}

// Nếu ko nối thêm đơn vị.
// if (Input.GetKey(KeyCode.LeftControl) == false)
//     m_controller.FunDeselectAllUnit();