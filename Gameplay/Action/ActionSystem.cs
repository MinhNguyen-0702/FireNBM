using UnityEngine;
using FireNBM.Pattern;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace FireNBM.UI.HUD
{
    [AddComponentMenu("FireNBM/System/Action System")]
    public class ActionSystem : Singleton<ActionSystem>
    {
        private ActionCompositeGroup m_compositeManager;                    // Cây Action composite của đối tượng nơi chứa cặp enum-keycode.
        private MessagingSystem m_messagingSystem;                          // Hệ thống quản lý thông điệp, nơi sẽ được nhận thông điệp nếu đăng ký lắng nghe.

        private Button m_currentButton;
        private List<ActionRTS> m_actionActive;                             // Danh sách các action hiện hoạt động.
        private Dictionary<Enum, GameObject> m_mapActionHUD;                // Chứa các đối tượng action HUD hiện được hỗ trợ.
        private Dictionary<Enum, Action> m_registeredActionsMap;

        private Stack<ActionCompositeGroup> m_preActionGroupsOnclick;       // Chứa các action group, giúp action composite chính quay lại nhánh trước đó.
        private Dictionary<Enum, ActionCompositeGroup> m_mapActionGroup;    // Hỗ trợ kiểm tra xem khi một action hud nhấn, liệu nó có thực thi hành động hay hiển thị các action khác.

        public static ActionSystem Instance { get => InstanceSingletonInScene; }


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        protected override void Awake()
        {
            base.Awake();

            m_compositeManager = null;
            m_registeredActionsMap = new Dictionary<Enum, Action>();
            m_actionActive = new List<ActionRTS>();
            m_mapActionHUD = new Dictionary<Enum, GameObject>();
            m_preActionGroupsOnclick = new Stack<ActionCompositeGroup>();
            m_mapActionGroup = new Dictionary<Enum, ActionCompositeGroup>();
            m_messagingSystem = MessagingSystem.Instance;
        }

        private void OnEnable()
        {
            m_messagingSystem.FunAttachListener(typeof(MessageDisplayActionHUD), OnGetComposite);
            m_messagingSystem.FunAttachListener(typeof(MessageDisableActionHUD), OnDisableAction);
            m_messagingSystem.FunAttachListener(typeof(MessageBreakAction), OnClickActionBreak);
        }

        private void OnDisable()
        {
            m_messagingSystem.FunDetachListener(typeof(MessageDisplayActionHUD), OnGetComposite);
            m_messagingSystem.FunDetachListener(typeof(MessageDisableActionHUD), OnDisableAction);
            m_messagingSystem.FunDetachListener(typeof(MessageBreakAction), OnClickActionBreak);
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        ///     Là một tiện ích giúp AitonSystem thêm các object Action HUD vào danh sách quản lý.
        ///     Bằng các bản thân các action hud chủ động gọi hàm này để thêm vào hệ thống.
        /// </summary>
        /// <param name="type"> Enum mà action hud làm đại diện. </param>
        /// <param name="actionHUD"> Đối tượng chứa action hud. </param>
        public void FunAddActionHUD(Enum type, GameObject actionHUD)
        {
            if (m_mapActionHUD.ContainsKey(type) == true)
            {
                Debug.LogWarning($"Enum for key {type} is already registered.");
                return;
            }
            m_mapActionHUD.Add(type, actionHUD);
            actionHUD.SetActive(false);
        } 

        /// <summary>
        ///     Dành cho action trong phần HUD. khi nhấn nút trong hud thì hàm này sẽ được 
        ///     gọi để xử lý liệu có thực hiện hành động action hay hiện các action khác hay không.
        /// </summary>
        /// <param name="typeAction"> Enum đại diện cho một action. </param>
        /// <param name="actionHudComp"> Thành phần quản lý action cho HUD.</param>
        /// -----------------------------------------------------------------------
        public void FunHandleOnclickAction(Enum typeAction, ActionHudComp actionHudComp)
        {
            if (m_mapActionGroup.TryGetValue(typeAction, out ActionCompositeGroup actionGroup) == true)
            {
                m_preActionGroupsOnclick.Push(m_compositeManager);
                OnClickActionDisplay(actionGroup);
                return;
            }
            actionHudComp.FunOnExecuteAction();
        }


        // ---------------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thực thi một action.</summary>
        /// ----------------------------------
        private void OnClickActionExecute(ActionComposite composite)
        {
            // Lấy thành phần ActionHUD 
            GameObject actionHUD = m_mapActionHUD[composite.FunGetAction().FunGetTypeAction()];
            if (actionHUD == null)
            {
                Debug.LogError($"Action HUD is null for action type: {composite.FunGetAction().FunGetTypeAction()}");
                return;
            }

            // Lấy action object để thực hiện hành động khi nó được nhấn.
            if (actionHUD.TryGetComponent<ActionHudComp>(out var actionHudComp) == false)
                return;
        
            actionHudComp.FunOnExecuteAction();

            // Chỉ thực hiện hiệu ứng đối với ActionLeaf.
            if (composite is ActionCompositeLeaf)
            {
                if (actionHUD.TryGetComponent<Button>(out var buttonComp) == false)
                    return;

                m_currentButton = buttonComp;
                m_currentButton.Select(); 
            }   
        }


        /// <summary>
        ///     Hiện thị các action con mà nó chứa.</summary>
        /// -------------------------------------------------
        private void OnClickActionDisplay(ActionCompositeGroup group)
        {
            // Vấn đề là Khi quay lại thì nút cha group là root. thì làm sao có trong ActionMap.
            if (m_preActionGroupsOnclick.Count > 0)
                OnClickActionExecute(group);

            DisablePreviousActions();
            m_compositeManager = group;
            DisplayNewActions();
        }

        // Ẩn các action trước đó đã sử dụng.
        // ----------------------------------
        private void DisablePreviousActions()
        {
            foreach (var action in m_actionActive)
            {
                m_mapActionHUD[action.FunGetTypeAction()].SetActive(false);
                InputSystem.Instance.FunUnregisterActionKey(action.FunGetKeyCodeAction(), InputSystem.InputMode.Press, m_registeredActionsMap[action.FunGetTypeAction()]);
            }
            m_actionActive.Clear();
            m_registeredActionsMap.Clear();
        }

        // Cập nhật các action mới cho đối tượng.
        // -------------------------------------
        private void DisplayNewActions()
        {
            if (m_compositeManager == null)
            {
                Debug.LogWarning("Composite Manager is not assigned. Cannot display actions.");
                return;
            }

            var inputSystem = InputSystem.Instance;
            foreach (var composite in m_compositeManager.FunGetChild())
            {
                var action = composite.FunGetAction();
                if (m_mapActionHUD.TryGetValue(action.FunGetTypeAction(), out GameObject objAction) == false)
                {
                    Debug.LogWarning($"Trong [ActionSystem], Loại: '{action.FunGetTypeAction()}' không hỗ trợ ActionUI.");
                    continue;
                }

                // Đăng ký hành động tương cho các phím trong composite manager.
                Action registeredAction = () =>
                {
                    if (composite is ActionCompositeLeaf leaf)
                        OnClickActionExecute(leaf);
                    else if (composite is ActionCompositeGroup group)
                    {
                        m_preActionGroupsOnclick.Push(m_compositeManager);
                        OnClickActionDisplay(group);
                    }
                }; 
                inputSystem.FunRegisterActionKey(action.FunGetKeyCodeAction(), InputSystem.InputMode.Press, registeredAction);
                m_registeredActionsMap.Add(action.FunGetTypeAction(), registeredAction);

                objAction.SetActive(true);
                m_actionActive.Add(action);
            }
        }

        // -----------------------------------------------------------------------------
        // HANDLE MESSAGE
        // --------------
        // //////////////////////////////////////////////////////////////////////////////

        // Nhận cây action composite từ đối tượng trong game rts.
        // ------------------------------------------------------
        private bool OnGetComposite(IMessage message)
        {
            var messageResult = message as MessageDisplayActionHUD;
            if (messageResult.ActionRTS == null)
            {
                Debug.Log("ActionRTS is null in MessageDisplayActionHUD. Please check the message.");
                return false;
            }
            m_compositeManager = messageResult.ActionRTS.FunGetRoot();

            // Xóa các nhóm hành động và stack trước đó
            m_mapActionGroup.Clear();
            m_preActionGroupsOnclick.Clear();

            // Lấy danh sách các nhóm Action Group, giúp khi nhấn một action từ UI ta có thể
            // biết được action ui đấy là một action thực thi hay là action hiển thị.
            foreach (var actionGroup in messageResult.ActionRTS.FunGetListActionGroup())
            {
                var typeAction = actionGroup.FunGetAction().FunGetTypeAction();
                m_mapActionGroup[typeAction] = actionGroup;
            }

            DisablePreviousActions();
            DisplayNewActions();
            return true;
        }

        private bool OnDisableAction(IMessage message)
        {
            DisablePreviousActions();
            m_mapActionGroup.Clear();
            m_preActionGroupsOnclick.Clear();
            return true;
        }     
        
        private bool OnClickActionBreak(IMessage message)
        {
            if (m_preActionGroupsOnclick.Count > 0)
                OnClickActionDisplay(m_preActionGroupsOnclick.Pop());        

            return true;
        }   
    }
}

// Nếu chọn unit này rồi lại chọn unit khác có bị lỗi không.