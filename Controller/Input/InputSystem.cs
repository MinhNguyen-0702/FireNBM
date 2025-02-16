using System;
using UnityEngine;
using System.Collections.Generic;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Quản lý phím được nhấn trong hệ thống trò chời RTS.
    /// </summary>
    [AddComponentMenu("FireNBM/System/Input System")]
    public class InputSystem : Singleton<InputSystem>
    {                    
        // Hold the key, press the key, release the key
        public enum InputMode { Hold, Press, Release };

        // For Update
        private Dictionary<KeyCode, Dictionary<InputMode, HashSet<Action>>> m_mapActionKey;        
        private Dictionary<int,     Dictionary<InputMode, HashSet<Action>>> m_mapActionMouseButton;     

        private bool m_isUpdateKeyActions;
        private bool m_isUpdateMouseButtonActions;
        private List<Tuple<Action, HashSet<Action>>> m_pendingActionClears;

        public static InputSystem Instance { get => InstanceSingleton; }


        // ---------------------------------------------------------------------------------
        // API UNITY
        // ---------
        // /////////////////////////////////////////////////////////////////////////////////

        protected override void Awake()
        {
            base.Awake();

            m_mapActionKey         = new Dictionary<KeyCode, Dictionary<InputMode, HashSet<Action>>>();
            m_mapActionMouseButton = new Dictionary<int,     Dictionary<InputMode, HashSet<Action>>>();

            m_isUpdateKeyActions = false;
            m_isUpdateMouseButtonActions = false;
            m_pendingActionClears = new List<Tuple<Action, HashSet<Action>>>();
        }

        private void Update()
        {
            UpdateKeyAction();
            UpdateMouseButtonAction();
            ClearTempAction();
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////

        public bool FunRegisterActionKey(KeyCode key, InputMode mode, Action action)
        {
            if (m_mapActionKey.ContainsKey(key) == false)
                m_mapActionKey.Add(key, new Dictionary<InputMode, HashSet<Action>>());
            
            Dictionary<InputMode, HashSet<Action>> mapModeAction = m_mapActionKey[key];
            if (mapModeAction.ContainsKey(mode) == false)
                mapModeAction.Add(mode, new HashSet<Action>());

            HashSet<Action> listActions = mapModeAction[mode];
            if (listActions.Contains(action) == true)
                return false;
            
            listActions.Add(action);
            return true;
        }

        public bool FunRegisterActionMouseButton(int key, InputMode mode, Action action)
        {
            if (m_mapActionMouseButton.ContainsKey(key) == false)
                m_mapActionMouseButton.Add(key, new Dictionary<InputMode, HashSet<Action>>());
            
            Dictionary<InputMode, HashSet<Action>> mapModeAction = m_mapActionMouseButton[key];
            if (mapModeAction.ContainsKey(mode) == false)
                mapModeAction.Add(mode, new HashSet<Action>());

            HashSet<Action> listActions = mapModeAction[mode];
            if (listActions.Contains(action) == true)
                return false;
            
            listActions.Add(action);
            return true;
        }


        public bool FunUnregisterActionKey(KeyCode key, InputMode mode, Action action)
        {
            if (m_mapActionKey.ContainsKey(key) == false)
            {
                Debug.LogWarning($"No action registered for key {key}.");;
                return false;
            }
            Dictionary<InputMode, HashSet<Action>> mapModeAction = m_mapActionKey[key];
            HashSet<Action> listActions = mapModeAction[mode];
            if (listActions.Contains(action) == false)
                return false;

            if (m_isUpdateKeyActions == false)
                listActions.Remove(action);
            else
                m_pendingActionClears.Add(Tuple.Create(action, listActions));
        
            return true;
        }

        public bool FunUnregisterActionMouseButton(int key, InputMode mode, Action action)
        {
            if (m_mapActionMouseButton.ContainsKey(key) == false)
            {
                Debug.LogWarning($"No action registered for key {key}.");;
                return false;
            }
            Dictionary<InputMode, HashSet<Action>> mapModeAction = m_mapActionMouseButton[key];
            HashSet<Action> listActions = mapModeAction[mode];
            if (listActions.Contains(action) == false)
                return false;

            if (m_isUpdateMouseButtonActions == false)
                listActions.Remove(action);
            else
                m_pendingActionClears.Add(Tuple.Create(action, listActions));

            return true;
        }


        // -----------------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////

        private void UpdateKeyAction()
        {
            m_isUpdateKeyActions = true;
            foreach (var key in m_mapActionKey.Keys)
            {
                if (HandeInputKey(key) == true)
                    break;
            }
            m_isUpdateKeyActions = false;
        }

        private void UpdateMouseButtonAction()
        {
            m_isUpdateMouseButtonActions = true;
            foreach (var key in m_mapActionMouseButton.Keys)
            {
                if (HandeInputMouseButton(key) == true)
                    break;
            }
            m_isUpdateMouseButtonActions = false;
        }

        private void ClearTempAction()
        {
            foreach (var (action, listAction) in m_pendingActionClears)
            {
                listAction.Remove(action);
            }
            m_pendingActionClears.Clear();
        }

        private bool HandeInputKey(KeyCode key)
        {
            if (Input.GetKeyDown(key))
                return TriggerActionKey(key, InputMode.Press);
            
            if (Input.GetKey(key)) 
                return TriggerActionKey(key, InputMode.Hold);
            
            if (Input.GetKeyUp(key)) 
                return TriggerActionKey(key, InputMode.Release);
            
            return false;
        }

        private bool HandeInputMouseButton(int key)
        {
            if (Input.GetMouseButtonDown(key))
                return TriggerActionMouseButton(key, InputMode.Press);
            
            if (Input.GetMouseButton(key)) 
                return TriggerActionMouseButton(key, InputMode.Hold);
            
            if (Input.GetMouseButtonUp(key)) 
                return TriggerActionMouseButton(key, InputMode.Release);
            
            return false;
        }

        private bool TriggerActionMouseButton(int key, InputMode mode)
        {
            if (m_mapActionMouseButton.TryGetValue(key, out var mapModeAction) == false)
            {
                Debug.LogWarning($"No actions registered for mouse button {key}.");
                return false;
            }

            if (mapModeAction.TryGetValue(mode, out var listActions) == false || listActions.Count == 0)
                return false;

            foreach (var action in listActions)
                action?.Invoke();
            
            return true;
        }

        private bool TriggerActionKey(KeyCode key, InputMode mode)
        {
            if (m_mapActionKey.TryGetValue(key, out var mapModeAction) == false)
            {
                Debug.LogWarning($"No actions registered for mouse button {key}.");
                return false;
            }

            if (mapModeAction.TryGetValue(mode, out var listActions) == false || listActions.Count == 0)
                return false;

            foreach (var action in listActions)
                action?.Invoke();
            
            return true;
        }
    }
} 