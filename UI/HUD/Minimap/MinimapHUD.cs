using UnityEngine;
using UnityEngine.EventSystems;

namespace FireNBM
{
    // TEST.
    public class MinimapHUD : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform m_rect;
        private bool m_isDragging = false;


        private void Update()
        {
            if (m_isDragging == false)
                HandleCameraIcon();
            else 
                HandleImitateDrag();


            if (Input.GetMouseButtonUp(0) == true)
                m_isDragging = false;
        }       

        public void OnBeginDrag(PointerEventData eventData)
        {
            DebugUtils.FunLog("oke");
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            
            m_isDragging = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            
            m_isDragging = false;
        }


        private void HandleCameraIcon()
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 700) == true)
            {
                Vector2 cameraPos = PosWorldToUI(hit.point, true);
                SetIconPos(cameraPos);
            }
        }

        private void HandleImitateDrag()
        {
            m_rect.transform.position = Input.mousePosition;
            var anchoredPosition = m_rect.anchoredPosition;

            // Kiểm tra xem nó vượt qua minimap ko.
            if (IsPositionIconOfBound(anchoredPosition) == true)
                anchoredPosition = InboundPositionToMap(anchoredPosition);

            SetIconPos(anchoredPosition);
        }

        private Vector2 PosWorldToUI(Vector3 pos, bool scaleToRadian)
        {
            return new Vector2(pos.x, pos.z);
        }

        private Vector3 UIPosToWorld(Vector2 pos)
        {
            return new Vector3(pos.x, 0f, pos.y);
        }

        private void SetIconPos(Vector2 pos, bool updateCameraPos = false)
        {
            m_rect.anchoredPosition = pos;

            if (updateCameraPos == true)
            {
                Vector3 posWorld = UIPosToWorld(pos);
                Camera.main.transform.position = posWorld;
            }
        }

        private bool IsPositionIconOfBound(Vector2 pos)
        {
            return pos.x >= 0f && pos.x <= 210 && pos.y >= 0 && pos.y <= 210;
        }

        private Vector2 InboundPositionToMap(Vector2 pos)
        {
            pos.x = Mathf.Clamp(pos.x, 0f, 210f);
            pos.y = Mathf.Clamp(pos.y, 0f, 210f);

            return pos;
        }
    }
}