using UnityEngine;

namespace FireNBM.UI.HUD
{
    public class SelectorHUDComp : MonoBehaviour
    {
        private RectTransform m_boxVisual;
        private Vector2 m_boxStart;
        private Vector2 m_boxEnd;


        // ---------------------------------------------------------------------------------
        // API UNITY 
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_boxStart = Vector2.zero;
            m_boxEnd = Vector2.zero;
            m_boxVisual = GetComponent<RectTransform>();
            DrawVisual();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_boxStart = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                m_boxEnd = Input.mousePosition;
                DrawVisual();
            }

            if (Input.GetMouseButtonUp(0))
            {
                m_boxStart = Vector2.zero;
                m_boxEnd = Vector2.zero;
                DrawVisual();
            }
        }


        // ------------------------------------------------------------------------
        // FUNCTION HELPER
        // ---------------
        // ///////////////////////////////////////////////////////////////////////

        private void DrawVisual()
        {
            Vector2 boxCenter = (m_boxStart + m_boxEnd) / 2;
            m_boxVisual.position = boxCenter;

            Vector2 boxSize = new Vector2(Mathf.Abs(m_boxStart.x - m_boxEnd.x), Mathf.Abs(m_boxStart.y - m_boxEnd.y));
            m_boxVisual.sizeDelta = boxSize;
        }
    }
}
    