using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Xử lý khi click chuột vào đối tượng trong game.
    /// </summary>
    public class MouseClickState : ISelectorState
    {
        private Camera m_mainCamera;                        
        private SelectorControllerComp m_controller;       

        private HashSet<GameObject> m_selectedUnits;        
        private HashSet<GameObject> m_selectedBuildings;   
        private HashSet<GameObject> m_selectedUnderConstructions;   
       
    
        // ----------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        // //////////////////////////////////////////////////////////////////////
        
        public MouseClickState(SelectorControllerComp controller)
        {
            m_controller = controller;
            m_mainCamera = m_controller.FunGetMainCamera();

            m_selectedUnits = m_controller.FunGetUnitsSelected();
            m_selectedBuildings = m_controller.FunGetBuildingsSelected();
            m_selectedUnderConstructions = m_controller.FunGetUnderConstructionsSelected();
        }

        // -----------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // //////////////////////////////////////////////////////////////////////

        public Enum FunGetTypeState() => TypeSelectorState.MouseClick;
        public void FunOnEnter() {}
        public void FunOnExit() {}

        public void FunHandle()
        {
            // Bỏ qua chọn đơn vị nếu chúng ta đang nhấn nút UI.
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (Input.GetMouseButtonDown(0) == true)
            {
                HandleOnClickObject();
            }
        }

        // ------------------------------------------------------------------------
        // FUNCTION HELPER
        // ---------------
        // ///////////////////////////////////////////////////////////////////////

        // Xử lý khí ckick dính unit.
        // --------------------------
        private void HandleClickUnit(GameObject unit)
        {
            ToggleObjectSelector(ref m_selectedUnits, unit, TypeObjectRTS.Unit);
        }

        // Xử lý khí ckick dính building.
        // --------------------------
        private void HandleClickBuilding(GameObject building)
        {
            ToggleObjectSelector(ref m_selectedBuildings, building, TypeObjectRTS.Building);
        }

        // Xử lý khí ckick dính UnderConstruction.
        // ---------------------------------------
        private void HandleClickUnderConstruction(GameObject underConstruction)
        {
            ToggleObjectSelector(ref m_selectedUnderConstructions, underConstruction, TypeObjectRTS.UnderConstruction);
        }

        // Hàm trợ giúp nhằm xử lý đối tượng khi click phải.
        // -------------------------------------------------
        private void ToggleObjectSelector(ref HashSet<GameObject> listObject, GameObject objectRTS, TypeObjectRTS typeObject)
        {
            // Hủy chọn nếu nó có phải là đơn vị đã chọn trước đó.
            if (listObject.Contains(objectRTS) == true)
            {
                m_controller.FunDisableSelectedStateForObjectRTS(objectRTS);
                listObject.Remove(objectRTS);
                return;
            }

            listObject.Add(objectRTS);
            m_controller.FunSetSelectedObjcetRTS(objectRTS);
            m_controller.FunUpdateHUD(typeObject, false);
        }

        private void HandleOnClickObject()
        {
            Ray ray = m_mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 400f) == false)
                return;

            m_controller.FunDeselectAllObjectRTS();
            string tagStr = hit.collider.tag;

            switch (tagStr)
            {
                // Player
                case ConstantFireNBM.UNIT:
                    HandleClickUnit(hit.collider.gameObject);
                    break;

                case ConstantFireNBM.BUILDING:
                    HandleClickBuilding(hit.collider.gameObject);
                    break;

                case ConstantFireNBM.UNDER_CONSTRUCTION:
                    HandleClickUnderConstruction(hit.collider.gameObject);
                    break;

                // Enemy
                case ConstantFireNBM.ENEMY:
                    HandleClickEnemyCheck(hit.collider.gameObject);
                    break;
                
                default:
                    m_controller.FunDeselectAllObjectRTS();
                    break;
            }
        }

        // TEST.
        private void HandleClickEnemyCheck(GameObject enemy)
        {
            m_controller.FunSetCheckObjectRTS(enemy);
            MessagingSystem.Instance.FunTriggerMessage(new MessageDisplayInfoObjectHUD(enemy, TypeObjectRTS.Unit), false);

            var enemyFlyweightComp = enemy.GetComponent<UnitFlyweightComp>();
            MessagingSystem.Instance.FunTriggerMessage(new MessageDisplayPortaitObjectHUD(enemyFlyweightComp.FunGetNameUnit()), false);
        }
    }
}