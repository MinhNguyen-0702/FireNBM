using UnityEngine;
using UnityEditor;
using FireNBM.Custom;
using System;

namespace FireNBM
{
    [CustomEditor(typeof(ActionData), true), CanEditMultipleObjects]
    public class ActionDataEditor : Editor
    {   
        private bool m_isInfoActionExecure = false;
        private bool m_isInfoActionDisplay = false;

        // Hiển thị thông tin nếu vị trí chuột nằm trong button.
        private Rect m_btnRectActionExecute;
        private Rect m_btnRectActionDisplay;

        // Được sử dụng để thêm thông tin khi di chuột và button.
        private Event m_event; 

        private GUIStyle m_boxStyle1;
        private GUIStyle m_boxStyle2;
        private GUIStyle m_boxStyle3;

        // Được dùng để tránh truy cập node sau khi xóa.
        private bool m_cleanActionComposite = false;


        // ---------------------------------------------------------------------------------
        // FUNCTION PROTECTED
        // ------------------
        // //////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     </summary>
        protected virtual void FunInitialize()
        {
            m_boxStyle1 = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(10, 10, 10, 10),    // Padding bên trong Box
                margin = new RectOffset(5, 5, 0, 0)          // Khoảng cách ngoài Box
            };

            m_boxStyle2 = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(30, 30, 10, 10),    
                margin = new RectOffset(5, 5, 5, 5)         
            };

            m_boxStyle3 = new GUIStyle(GUI.skin.box)
            {
                padding = new RectOffset(10, 10, 10, 10),    // Padding bên trong Box
                margin = new RectOffset(5, 5, 5, 5)          // Khoảng cách ngoài Box
            };
        }

        /// <summary>
        ///     Hiển thị cây action composite đã được tạo.</summary>
        // ----------------------------------------------------------
        protected void FunDisplayActionData(ActionData actionData)
        {
            GUILayout.BeginVertical("", "GroupBox");
            GUIStyleCustom.Label.FunSetTitleGroupBox("Tree Composite Action:", -3, TextAnchor.LowerLeft);
            DisplayAcions(actionData, actionData.FunGetRoot());
            GUILayout.EndVertical();
            GUILayout.Space(15);
        }   

        /// <summary>
        ///     Xử lý logic việc thêm một composite action vào cây.</summary>
        // ------------------------------------------------------------------
        protected void FunUpdateActionData(ActionData actionData, Enum type)
        {
            m_event = Event.current;

            GUILayout.BeginVertical(m_boxStyle3, GUILayout.Height(22));
            if (actionData.ActionGroupChild == null)
                GUIStyleCustom.Label.FunSetTitleGroupBox("Add to Root", 3, TextAnchor.MiddleCenter, 3);
            else 
            {
                GUILayout.BeginHorizontal();
                Color oldBackgroundColor = GUI.backgroundColor;
                GUI.backgroundColor =  new Color(1f, 0.6f, 0.6f);

                if (GUILayout.Button("X", GUILayout.Height(18), GUILayout.Width(30))) 
                    actionData.ActionGroupChild = null;

                GUI.backgroundColor = oldBackgroundColor;
                GUIStyleCustom.Label.FunSetTitleGroupBox("Add to Group", 3, TextAnchor.MiddleCenter, 3);
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical(m_boxStyle3, GUILayout.Height(120));
            UpdateButtonCreateAction(actionData);
            GUILayout.EndVertical();

            if (actionData.ShowDropdown == true)
                UpdateButtonsInShowDropdown(actionData, type);
        }

        /// <summary>
        ///     Cập nhật các thay đổi. </summary>
        // --------------------------------------
        protected void FunApplyChangeActionData(ActionData data)
        {
            if (GUI.changed)
            {
                EditorUtility.SetDirty(data);   // Thông báo nó đã thay đổi và chưa được lưu.
                AssetDatabase.SaveAssets();     // Sẽ lưu vào ổ đĩa tất cả asset chưa được lưu.
                AssetDatabase.Refresh();        // Báo cho Unity Editor biết và lưu chúng.
            }
        }


        // ---------------------------------------------------------------------------------
        // FUNCTION PROTECTED
        // ------------------
        // //////////////////////////////////////////////////////////////////////////////////

        // Hiển thị danh sách các node action.
        // ----------------------------------
        private void DisplayAcions(ActionData actionData, ActionCompositeGroup group, int indent = 0)
        {
            var listActionData = group.FunGetChild();
            for (int i = 0; i < listActionData.Count; ++i)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(indent);
                UpdateCompositeAction(actionData, group, listActionData[i]);
                GUILayout.EndHorizontal();

                GUILayout.Space(7);
                
                if (m_cleanActionComposite == false && listActionData[i] is ActionCompositeGroup groupAction)
                    DisplayAcions(actionData, groupAction, indent + 15);
            }
        }   

        // Cập nhật giữ liệu của từng node action.
        // ---------------------------------------
        private void UpdateCompositeAction(ActionData actionData, ActionCompositeGroup compositeGroup, ActionComposite childComposite)
        {
            if (childComposite == null)
            {
                Debug.LogError("compositeAction is null!");
                return;
            }

            var action = childComposite.FunGetAction();
            if (action == null)
            {
                Debug.LogError("Action is null for compositeAction");
                return;
            }
            GUILayout.BeginVertical(m_boxStyle1);

            // Info Action main.
            // ----------------
            if (childComposite is ActionCompositeLeaf)
                GUILayout.Label("ACTION EXECUTE:     " + action.FunGetTypeAction() + " / " + action.FunGetKeyCodeAction());
            else 
                GUILayout.Label("ACTION DISPLAY:     " + action.FunGetTypeAction() + " / " + action.FunGetKeyCodeAction());
            
            GUILayout.Space(10);

            // Info Action detailt
            // -------------------
            GUILayout.BeginVertical(m_boxStyle1);
            GUILayout.BeginHorizontal();
            EditorGUI.indentLevel++;

            // Khối thứ nhất.
            GUILayout.BeginVertical();
            {
                // Cập nhật action type nếu có thay đổi.
                Enum currentEnum = action.FunGetTypeAction();
                GUILayout.BeginHorizontal();
                GUIStyleCustom.Label.FunSetLabel("Type", 48);
                Enum updatedEnum = EditorGUILayout.EnumPopup(currentEnum);
                GUILayout.EndHorizontal();
                if (updatedEnum.ToString() != currentEnum.ToString())
                {
                    action.FunSetTypeAction(updatedEnum);
                }
                
                // Cập nhật keyCode nếu có thay đổi.
                KeyCode currentKeyCode = action.FunGetKeyCodeAction();
                GUILayout.BeginHorizontal();
                GUIStyleCustom.Label.FunSetLabel("Input", 48);
                KeyCode updatedKeyCode = (KeyCode)EditorGUILayout.EnumPopup(currentKeyCode);
                GUILayout.EndHorizontal();
                if (updatedKeyCode.ToString() != currentKeyCode.ToString())
                {
                    action.FunSetKeyCodeAction(updatedKeyCode);
                }
            }
            GUILayout.EndVertical();

            // Khối thứ hai.
            EditorGUILayout.BeginVertical();
            {
                if (childComposite is ActionCompositeLeaf)
                {
                    if (GUILayout.Button("Remove", GUILayout.Height(38)))
                    {
                        m_cleanActionComposite = true;
                        compositeGroup.FunRemoveActionChild(childComposite);
                    }
                }
                else if (childComposite is ActionCompositeGroup group)
                {
                    if (GUILayout.Button("Remove", GUILayout.Height(19)))
                    {
                        m_cleanActionComposite = true;
                        compositeGroup.FunRemoveActionChild(childComposite);
                        actionData.FunRemoveActionGroup(group);
                    } 
                    if (GUILayout.Button("Add", GUILayout.Height(19)))
                    {
                        actionData.ActionGroupChild = group;
                    }
                }
            }
            EditorGUILayout.EndVertical();

            EditorGUI.indentLevel--;
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.Space(3);
            GUILayout.EndVertical();
        }

        // Xử lý việc hiển thị và logic của các button tạo action composite. 
        // ----------------------------------------------------------------
        private void UpdateButtonCreateAction(ActionData actionData)
        {
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("New Action Execute", GUILayout.Height(30)))
            {
                actionData.ShowDropdown = true;
                actionData.IsCompositeActionLeaf = true;
                actionData.IsCompositeActionGroup = false;
            }
            m_btnRectActionExecute = GUILayoutUtility.GetLastRect();

            GUILayout.Space(5);

            if (GUILayout.Button("New Action Display", GUILayout.Height(30)))
            {
                actionData.ShowDropdown = true;
                actionData.IsCompositeActionLeaf = false;
                actionData.IsCompositeActionGroup = true;

            }
            m_btnRectActionDisplay = GUILayoutUtility.GetLastRect();

            GUILayout.EndHorizontal();
            HandleInfoButtonsAction();
        }

        // TODO: Làm clean code hơn.
        private void HandleInfoButtonsAction()
        {
            // Kích hoạt tính năng xuống dòng
            var wrappedLabelStyle = new GUIStyle(EditorStyles.label)
            {
                fontStyle = FontStyle.Italic,
                wordWrap = true 
            };

            // Hiển thị thông tin nếu chuột hover vào button
            GUILayout.BeginVertical("box");
            wrappedLabelStyle.normal.textColor = new Color(0.5490196078431373f, 0.5490196078431373f, 0.5490196078431373f);
            if (m_isInfoActionExecure == true)
            {
                GUILayout.Label("INFO: A leaf composite node will be created, and this action will " + 
                                "contain the execution command when the user clicks the button in the game's HUD.", wrappedLabelStyle);
            } else if (m_isInfoActionDisplay == true)
            {
                GUILayout.Label("INFO: A group composite node will be created, and this action will " + 
                                "serve as a container for other actions. When it is clicked, its child actions will be displayed to the user in the game's HUD.", wrappedLabelStyle);
            }
            GUI.contentColor = Color.white;
            GUILayout.EndVertical();

            // Hiện thị thông tin nếu di chuột vào các button.
            // -----------------------------------------------
            if (m_event.type == EventType.Repaint) 
            {
                if (m_btnRectActionExecute.Contains(m_event.mousePosition))
                {
                    if (m_isInfoActionExecure == false)
                    {
                        m_isInfoActionExecure = true;
                        Repaint(); // Yêu cầu Editor vẽ lại
                    }
                }
                else if (m_btnRectActionDisplay.Contains(m_event.mousePosition))
                {
                    if (m_isInfoActionDisplay == false) // Kiểm tra nếu chưa bật thông tin
                    {
                        m_isInfoActionDisplay = true;
                        Repaint();
                    } 
                } 
                else
                {
                    if (m_isInfoActionExecure || m_isInfoActionDisplay)
                    {
                        m_isInfoActionExecure = false;
                        m_isInfoActionDisplay = false;
                    }
                }
            }
        }

        private void UpdateButtonsInShowDropdown(ActionData actionData, Enum typeRTS)
        {
            GUILayout.BeginVertical(m_boxStyle1);
            if (actionData.IsCompositeActionLeaf == true) 
                GUILayout.Label("Select a type 'Action Execute' for unit to create: " , EditorStyles.label);
            else if (actionData.IsCompositeActionGroup == true) 
                GUILayout.Label("Select a type 'Action Display' for unit to create: " , EditorStyles.label); 

            GUILayout.Space(5);
            CreateListTypeFromEnum(actionData, typeRTS);
            GUILayout.EndVertical();
        }

        private void CreateListTypeFromEnum(ActionData actionData, Enum typeRTS)
        {
            Color oldBackgroundColor = GUI.backgroundColor;
            Color oldContentColor = GUI.contentColor;

            GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f);  // Màu nền nhạt (R, G, B)
            GUI.contentColor = new Color(0.7f, 0.7f, 0.7f);     // Màu chữ nhạt (R, G, B)

            GUILayout.BeginVertical(m_boxStyle2);
            var opitionsUnit = Enum.GetNames(typeRTS.GetType());
            foreach (var type in opitionsUnit)
            {
                if (type == "None")
                {
                    GUI.backgroundColor =  new Color(1f, 0.6f, 0.6f);
                    if (GUILayout.Button("Exit", GUILayout.Height(26))) 
                    {
                        actionData.ShowDropdown = false;
                        actionData.IsCompositeActionLeaf = false;
                        actionData.IsCompositeActionGroup = false;
                        break;
                    }
                    GUI.backgroundColor = new Color(0.8f, 0.8f, 0.8f); 
                    continue;
                }
                if (GUILayout.Button(type, GUILayout.Height(26)))
                {
                    Enum parsedEnum = (Enum)Enum.Parse(typeRTS.GetType(), type);
                    CreateCompositeAction(actionData, parsedEnum);
                    actionData.ShowDropdown = false;
                }
            }
            GUILayout.EndVertical();

            GUI.backgroundColor = oldBackgroundColor;
            GUI.contentColor = oldContentColor;
        }

        // Tạo node cho cây composite action.
        // ----------------------------------
        private void CreateCompositeAction(ActionData actionData, Enum type)
        {
            var action = actionData.FunCreateAction(type);
            ActionComposite actionComposite = null;

            if (actionData.IsCompositeActionLeaf == true)
                actionComposite = new ActionCompositeLeaf(action);
            else if (actionData.IsCompositeActionGroup == true)
            {
                actionComposite = new ActionCompositeGroup(action);
                actionData.ListActionGroup.Add(actionComposite as ActionCompositeGroup);
            }

            if (actionData.ActionGroupChild == null)
                actionData.FunAddAction(actionComposite);
            else 
            {
                actionData.ActionGroupChild.FunAddActionChild(actionComposite);
                actionData.ActionGroupChild = null;
            }

            actionData.IsCompositeActionLeaf = false;
            actionData.IsCompositeActionGroup = false;
        } 
    }
} 