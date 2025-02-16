using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FireNBM.Pattern;
using System.Linq;
using System;

namespace FireNBM
{
    public enum TypeObjectRTS 
    { 
        Unit, 
        Building, 
        UnderConstruction 
    };
    
    /// <summary>
    ///     Điều khiển các đơn vị được chọn.
    /// </summary>
    [AddComponentMenu("FireNBM/Selector/Selector Controller Comp")]
    public class SelectorControllerComp : MonoBehaviour
    {
        private Camera m_mainCamera;                        // Camera chính của cảnh.       
        private List<GameObject> m_units;                   // Tất cả các unit hoạt động có trong scene.

        private HashSet<GameObject> m_selectedUnits;        
        private HashSet<GameObject> m_selectedBuildings;        
        private HashSet<GameObject> m_selectedUnderConstructions;        

        private MessagingSystem m_messagingSystem;          // Hệ thống gửi tin nhắn.
        private InputSystem m_inputSystem;

        private TypeSelectorState m_currentTypeState;       // Tránh việc thay đổi cùng một trạng thái.
        private SelectorStateComp m_selectorState;          // Quản lý các trạng thái lựa chọn đối tượng.

        private bool m_isButtonUp;
        private bool m_isCommandReceived;                   // Nếu chưa có lệnh đc gửi, thì xử lý mặc định khi người chơi nhấn.
        private Vector3 m_startPosClick;                    // Lưu trữ vị trí bất đầu nhấn chuột.

        private CustomMouseComp m_customMouse;              // Dùng để lấy vị trí nhấn chuột.
        private HighlightManager m_highlightManager;
        private HealthManager m_healthManager;

        private GameObject m_enemyCheck;

        // -----------------------------------------------------------------------------
        // API UNITY
        // ---------
        // //////////////////////////////////////////////////////////////////////////////

        private void Awake()
        {
            m_mainCamera = Camera.main;
            m_isCommandReceived = false;
            m_isButtonUp = false;
            m_highlightManager = new HighlightManager();
            m_healthManager = new HealthManager();
            m_currentTypeState = TypeSelectorState.None;
            m_selectedUnits = new HashSet<GameObject>();
            m_selectedBuildings = new HashSet<GameObject>();
            m_selectedUnderConstructions = new HashSet<GameObject>();
        }

        private void Start()
        {
            m_customMouse = GetComponent<CustomMouseComp>();
            DebugUtils.HandleErrorIfNullGetComponent<CustomMouseComp, SelectorControllerComp>(m_customMouse, this, gameObject);

            m_selectorState = GetComponent<SelectorStateComp>();
            DebugUtils.HandleErrorIfNullGetComponent<SelectorStateComp, SelectorControllerComp>(m_selectorState, this, gameObject);

            m_units = UnitManager.Instance.FunGetAllUnitActive(GameSystem.Instance.FunGetSettingGame().FunGetRacePlayer());
    
            m_selectorState.FunRegisterStateSelecter(new MouseClickState(this));
            m_selectorState.FunRegisterStateSelecter(new MouseDragState(this));
            m_selectorState.FunRegisterStateSelecter(new MouseHoverState(this));
            m_selectorState.FunRegisterStateSelecter(new QuickSelectState(this));
        }

        private void OnEnable()
        {
            m_inputSystem = InputSystem.Instance;
            m_messagingSystem = MessagingSystem.Instance;

            m_messagingSystem.FunAttachListener(typeof(MessageActionCommand), OnHandleActionFromObjectSelector);
            m_messagingSystem.FunAttachListener(typeof(MessageActionOnClick), OnHandleActionOnClick);
            m_messagingSystem.FunAttachListener(typeof(MessageDisplayObjectSelector), OnSelectorObjectButtonInfoClick);

            m_inputSystem.FunRegisterActionMouseButton(ConstantFireNBM.MOUSE_LEFT, InputSystem.InputMode.Press, OnActionMouseClick);
            m_inputSystem.FunRegisterActionMouseButton(ConstantFireNBM.MOUSE_LEFT, InputSystem.InputMode.Hold, OnActionMouseDrag);
            m_inputSystem.FunRegisterActionMouseButton(ConstantFireNBM.MOUSE_LEFT, InputSystem.InputMode.Release, OnActionMouseHover);
            m_inputSystem.FunRegisterActionKey(KeyCode.Tab, InputSystem.InputMode.Press, OnActionQuickSelect);
        }

        private void OnDisable()
        {
            m_messagingSystem.FunDetachListener(typeof(MessageActionCommand), OnHandleActionFromObjectSelector);
            m_messagingSystem.FunDetachListener(typeof(MessageActionOnClick), OnHandleActionOnClick);
            m_messagingSystem.FunDetachListener(typeof(MessageDisplayObjectSelector), OnSelectorObjectButtonInfoClick);

            m_inputSystem.FunRegisterActionMouseButton(ConstantFireNBM.MOUSE_LEFT, InputSystem.InputMode.Press, OnActionMouseClick);
            m_inputSystem.FunRegisterActionMouseButton(ConstantFireNBM.MOUSE_LEFT, InputSystem.InputMode.Hold, OnActionMouseDrag);
            m_inputSystem.FunRegisterActionMouseButton(ConstantFireNBM.MOUSE_LEFT, InputSystem.InputMode.Release, OnActionMouseHover);
            m_inputSystem.FunUnregisterActionKey(KeyCode.Tab, InputSystem.InputMode.Press, OnActionQuickSelect);
        }

        private void Update()
        {
            // Bỏ qua chọn đơn vị nếu chúng ta đang nhấn nút UI.
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            UpdateMoveFormCurrent();   
        }

        private void LateUpdate()
        {
            if (m_isButtonUp == true)
            {
                m_isButtonUp = false;
                TryChangeState(TypeSelectorState.MouseHover);
            }
        }


        // -----------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        // /////////////////////////////////////////////////////////////////////////////

        /// <summary> 
        ///     Lấy tùy chỉnh chuột.</summary>
        /// ---------------------------------
        public CustomMouseComp FunGetCursorMouse() => m_customMouse;
        ///
        /// <summary>
        ///     Kiểm tra xem chuột có di chuyển ko. </summary>
        /// --------------------------------------------------
        public bool FunIsMouseMoving() => m_customMouse.FunIsMouseMoving();
        ///
        /// <summary> 
        ///     Lấy camera chính có trong scene.</summary>
        /// ----------------------------------------------
        public Camera FunGetMainCamera() => m_mainCamera;
        ///
        /// <summary> 
        ///     Lấy vị trí bắt đầu nhấn của chuột trái. </summary>
        /// ------------------------------------------------------
        public Vector3 FunGetStartMousePos() => m_startPosClick;
        ///
        /// <summary> 
        ///     Lấy các đối tượng unit được chọn. </summary>
        /// -------------------------------------------
        public HashSet<GameObject> FunGetUnitsSelected() => m_selectedUnits;
        ///
        /// <summary> 
        ///     Lấy các đối tượng building được chọn. </summary>
        /// -------------------------------------------
        public HashSet<GameObject> FunGetBuildingsSelected() => m_selectedBuildings;
        ///
        /// <summary> 
        ///     Lấy các đối tượng under construction được chọn. </summary>
        /// -------------------------------------------
        public HashSet<GameObject> FunGetUnderConstructionsSelected() => m_selectedUnderConstructions;
        ///
        /// <summary> 
        ///     Lấy các đối tượng unit có trong scene. </summary>
        /// -----------------------------------------------------
        public List<GameObject> FunGetUnits() => m_units;
        
        /// <summary>
        ///     Đánh dấu đối tượng được highlight.</summary>
        /// -----------------------------------------------
        public void FunSetHighlightObjcetRTS(GameObject objectRTS)
        {
            m_highlightManager.FunSetHighlight(objectRTS);
            m_healthManager.FunSetEnable(objectRTS);
        }

        /// <summary>
        ///     Đánh dấu đối tượng được chọn.</summary>
        /// -------------------------------------------
        public void FunSetSelectedObjcetRTS(GameObject objectRTS)
        {
            m_highlightManager.FunSetSelector(objectRTS);
            m_healthManager.FunSetEnable(objectRTS);
        }

        /// <summary>
        ///     Đánh dấu đối tượng được kiểm tra. </summary>
        /// ------------------------------------------------
        public void FunSetCheckObjectRTS(GameObject enemy)
        {
            if (m_enemyCheck == enemy)
                return;
            
            m_enemyCheck = enemy;
            m_highlightManager.FunSetCheck(enemy);
            m_healthManager.FunSetEnable(enemy);
        }

        /// <summary>
        ///     Tắt trạng thái không được chọn cho unit.</summary>
        /// ------------------------------------------------------
        public void FunDisableSelectedStateForObjectRTS(GameObject objectRTS)
        {
            m_highlightManager.FunSetDisable(objectRTS);
            m_healthManager.FunSetDisable(objectRTS);
        }
        
        /// <summary>
        ///     Bỏ chọn tất cả đơn vị. </summary>
        /// -------------------------------------
        public void FunDeselectAllObjectRTS()
        {
            foreach (GameObject unit in m_selectedUnits)
                FunDisableSelectedStateForObjectRTS(unit);

            foreach (GameObject building in m_selectedBuildings)
                FunDisableSelectedStateForObjectRTS(building);

            foreach (GameObject underConstruction in m_selectedUnderConstructions)
                FunDisableSelectedStateForObjectRTS(underConstruction);

            m_selectedUnits.Clear();
            m_selectedBuildings.Clear();
            m_selectedUnderConstructions.Clear();

            if (m_enemyCheck != null)
            {
                FunDisableSelectedStateForObjectRTS(m_enemyCheck);
                m_enemyCheck = null;
            }

            // Cập nhật lại HUD.
            m_messagingSystem.FunTriggerMessage(new MessageDisableObjectsHUD(), false);
        }

        /// <summary>
        ///     Cập nhật bảng HUD khi có một hoặc nhiều đối tượng được chọn. </summary>
        /// --------------------------------------------------------------------------- 
        public void FunUpdateHUD(TypeObjectRTS typeObject, bool isDifferentObject)
        {
            switch (typeObject)
            {
                case TypeObjectRTS.Unit:
                    m_messagingSystem.FunTriggerMessage(new MessageUpdateObjectsHUD(typeObject, m_selectedUnits, isDifferentObject), false);
                    break;
                
                case TypeObjectRTS.Building:
                    m_messagingSystem.FunTriggerMessage(new MessageUpdateObjectsHUD(typeObject, m_selectedBuildings, isDifferentObject), false);
                    break;
                
                default:
                    break;
            }
        }

        /// <summary>
        ///     Kiểm tra xem đối tượng có nằm trong danh sách được chọn chưa.</summary>
        /// --------------------------------------------------------------------------- 
        public bool FunIsObjectSelector(TypeObjectRTS type, GameObject objectRTS)
        {
            return (type) switch
            {
                TypeObjectRTS.Unit => m_selectedUnits.Contains(objectRTS),
                TypeObjectRTS.Building => m_selectedBuildings.Contains(objectRTS),
                TypeObjectRTS.UnderConstruction => m_selectedUnderConstructions.Contains(objectRTS),
                _=> false
            };
        }

        /// <summary>
        ///     Cập nhật các action của đối tượng khi nó được người chơi lựa chọn. </summary>
        /// ---------------------------------------------------------------------------------
        public void FunUpdateForActionHUD(ActionData actionData)
        {
            m_messagingSystem.FunTriggerMessage(new MessageDisplayActionHUD(actionData), false);
        }


        // -----------------------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        // //////////////////////////////////////////////////////////////////////////////

        // Di chuyển đội hình đến vị trí mới khi nhấn chuột
        // ------------------------------------------------
        private void UpdateMoveFormCurrent()
        {
            if (Input.GetMouseButtonDown(ConstantFireNBM.MOUSE_RIGHT) && m_isCommandReceived == true) 
            {
                m_isCommandReceived = false;
                m_selectorState.Active = true;

                m_messagingSystem.FunTriggerMessage(
                    new MessageUpdateFormCurrMoveTarget(m_customMouse.FunGetPosMouseClick(), m_selectedUnits), false);
            }
        }

        // Tránh thay đổi lại trạng thái lại cũ.
        // ------------------------------------
        private void TryChangeState(TypeSelectorState typeSelector)
        {
            if (m_currentTypeState == typeSelector)
                return;
            
            m_currentTypeState = typeSelector;
            m_selectorState.FunChangeStateSelecter(typeSelector);
        }        

        // Kiểm tra xem chuột có ở trạng thái click ko.
        // --------------------------------------------
        private bool IsMouseClick() 
        {
            return Vector3.Distance(m_startPosClick, Input.mousePosition) < ConstantFireNBM.DRAG_THRESHOLD;
        }

        // Cập nhật trạng thái building khi action cho building được người chơi nhấn.
        // -------------------------------------------------------------------------
        private void HandleActionBuilding(Enum action)
        {
            if (m_selectedBuildings.Count < 1)
                return;
            
            var building = m_selectedBuildings.First();
            var buildingStateComp = building.GetComponent<BuildingStateComp>();
            buildingStateComp.FunChangeState(TypeRaceBuildingTownhall.Free);    // Test.
            buildingStateComp.FunChangeState(action);
        }

        // Cập nhật trạng thái unit khi action cho unit được người chơi nhấn.
        // -----------------------------------------------------------------
        private void HandleActionUnit(Enum action)
        {
            m_messagingSystem.FunTriggerMessage(new MessageUpdateFormCurrState(m_isCommandReceived, action, m_selectedUnits), false);
            m_isCommandReceived = true;
        }


        // -----------------------------------------------------------------------------
        // HANDLE MESSAGE
        // --------------
        // //////////////////////////////////////////////////////////////////////////////

        // Thay đổi trạng thái cho các đối tượng RTS được chọn khi lệnh Action được nhấn.
        // ------------------------------------------------------------------------------
        private bool OnHandleActionFromObjectSelector(IMessage message)
        {
            var messageResult = message as MessageActionCommand;
            switch (messageResult.TypeObject)
            {
                case TypeObjectRTS.Unit:
                    HandleActionUnit(messageResult.Action);
                    break;
                
                case TypeObjectRTS.Building:
                    HandleActionBuilding(messageResult.Action);
                    break;
                
                default:
                    break;
            }
            return true;
        }

        // Dừng cập nhật state selector nếu nhấn lệnh action.
        // -------------------------------------------------
        private bool OnHandleActionOnClick(IMessage message)
        {
            m_selectorState.Active = false;
            return true;
        }

        private bool OnSelectorObjectButtonInfoClick(IMessage message)
        {
            var messageResult = message as MessageDisplayObjectSelector;

            foreach (var unit in m_selectedUnits)
                FunDisableSelectedStateForObjectRTS(unit);
            
            m_selectedUnits.Clear();
            m_selectedUnits.Add(messageResult.ObjectSelector);
            FunSetSelectedObjcetRTS(messageResult.ObjectSelector);
            return true;
        }


        // -----------------------------------------------------------------------------
        // HANDLE INPUT ACTION
        // -------------------
        // //////////////////////////////////////////////////////////////////////////////

        private void OnActionMouseClick()
        {
            m_isCommandReceived = false;  
            m_startPosClick = Input.mousePosition;
            TryChangeState(TypeSelectorState.MouseClick);
        }

        private void OnActionMouseDrag()
        {
            if (IsMouseClick() == false)
                TryChangeState(TypeSelectorState.MouseDrag);
        }

        private void OnActionMouseHover()
        {
            m_isButtonUp = true;
        }

        private void OnActionQuickSelect()
        {
            TryChangeState(TypeSelectorState.QuickSelect);
        }
    }
}