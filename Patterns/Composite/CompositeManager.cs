using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     Quản lý cấu trúc Composite, cho phép thực hiên thao tác cơ bản trên cây.
    /// </summary>
    public class CompositeManager
    {
        private CompositeGroup m_root;                              // Nút gốc 
        private Dictionary<string, Composite> m_compositeLookup;    // Giúp tìm leaf nhanh hơn.
        private Dictionary<string, Action<Composite>> m_actions;    // Danh sách các hàm thực hiện trong nút.


        // ------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        // ////////////////////////////////////////////////////////////////////////

        public CompositeManager()
        {
            m_root = new CompositeGroup(Composite.NAME_ROOT, null, null);
            m_actions = new Dictionary<string, Action<Composite>>();
            m_compositeLookup = new Dictionary<string, Composite>();
            m_compositeLookup.Add(m_root.Name, m_root);
        }


        // ----------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // //////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thêm nột nút lá mới vào cây. </summary>
        /// -------------------------------------------
        public void FunAddComposite(Composite newChild, string nameParent = null) // -> 
        {
            // Thoát nếu lá này đã được thêm.
            if (FunCheckComposite(newChild.Name) == true)
            {
                Debug.LogWarning($"In CompositeManager, leaf with name: {newChild.Name} has existed");
                return;
            }

            // Thêm vào noot gốc.
            if (nameParent == null)
            {
                m_root.FunAddChild(newChild);
                m_compositeLookup.Add(newChild.Name, newChild);
                return;
            } 

            // Thêm vào nhánh.
            AddComponentAt(nameParent, newChild);
        }

        /// <summary>
        ///     Xóa một nút khỏi cây. </summary>
        /// -----------------------------------
        public void FunRemoveComposite(string name)
        {
            // Thoát nếu lá này không tồn tại.
            if (FunCheckComposite(name) == false)
            {
                Debug.LogWarning($"In CompositeManager, leaf with name: {name} not found");
                return;
            }

            Composite composite = m_compositeLookup[name];
            RemoveLeafInMap(composite);
            RemoveLeafInTree(composite);
        }

        /// <summary>
        ///     Thêm một action vào danh sách. </summary>
        /// --------------------------------------------
        public void FunAddAction(string nameAction, Action<Composite> action) 
        {
            if (m_actions.ContainsKey(nameAction) == true)
            {
                Debug.LogWarning($"Action for name: {nameAction} has existed.");
                return;
            }
            m_actions.Add(nameAction, action);
        }

        /// <summary>
        ///     Xóa một hàm call back action ra khỏi danh sách. </summary>
        /// -------------------------------------------------------------
        public void FunRemoveAction(string nameAction) 
        {
            if (m_actions.ContainsKey(nameAction) == false)
            {
                Debug.LogWarning($"Action for name: {nameAction}, could not find.");
                return;
            }
            m_actions.Remove(nameAction);
        }

        /// <summary>
        ///     Thực thi hàm cho một nhánh hoặc toàn bộ cây khi được truyền từ bên ngoài vào. </summary>
        /// --------------------------------------------------------------------------------------------
        public void FunExecuteActionAll(string typeNameAction)
        {
            if (m_root == null)
            {
                Debug.LogWarning("Root node is null. Cannot execute actions.");
                return;
            }

            if (m_actions.TryGetValue(typeNameAction, out Action<Composite> action)) 
                m_root.FunExecuteAction(action);
            else 
                Debug.LogWarning("In CompositeManager, Could not find Action for type: " + typeNameAction);
        }

        /// <summary>
        /// Thực hiện hành động trên một nhánh cụ thể.
        /// Nếu hành động thành công trên bất kỳ node nào, trả về true. </summary>
        /// ----------------------------------------------------------------------
        public bool FunExecuteActionAt(string name, Func<Composite, bool> action)
        {
            // Tìm node trong bảng lookup.
            if (m_compositeLookup.TryGetValue(name, out Composite composite) == false)
            {
                Debug.Log("Composite not found: " + name);
                return false;
            }

            // Gọi hàm action với tham số composite.
            bool isActionSuccessful = action?.Invoke(composite) == true;

            // Nếu hành động thành công hoặc đây là node gốc, dừng lại.
            if (isActionSuccessful || composite.Parent == null)
            {
                return isActionSuccessful;
            }

            // Đệ quy lên node cha.
            return FunExecuteActionAt(composite.Parent.Name, action);
        }

        /// <summary>
        ///     Kiểm tra xem node leaf với tên được chỉ định có tồn tại ko. </summary>
        /// --------------------------------------------------------------------------
        public bool FunCheckComposite(string parent) => m_compositeLookup.ContainsKey(parent);

        /// <summary>
        ///     Lấy dữ liệu có trong conposite. </summary>
        /// ---------------------------------------------- 
        public CompositeData FunGetCompositeData(string name) => FunGetComposite(name).Data;

        /// <summary>
        ///     Lấy Node Composite dựa theo tên của chúng. </summary>
        /// ---------------------------------------------------------
        public Composite FunGetComposite(string name)
        {
            if (m_compositeLookup.ContainsKey(name) == false)
            {
                Debug.Log("In CompositeManager, Could not find composite with name: " + name);
                return null;
            }
            return m_compositeLookup[name];
        }

        public CompositeGroup FunGetRoot() => m_root;
        public List<Composite> FunGetChilds() => m_root.FunGetChildren();


        // ----------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // //////////////////////////////////////////////////////////////////////

        // Xóa các node con ra bản đồ tìm kiếm.
        private void RemoveLeafInMap(Composite composite)
        {
            if (composite is CompositeGroup group)
                RemoveChildren(group);
            
            m_compositeLookup.Remove(composite.Name);
        }

        // Xóa node con ra khỏi cây.
        private void RemoveLeafInTree(Composite composite)
        {
            // Lấy node cha để xóa
            var parentLeaf = composite.Parent as CompositeGroup;
            parentLeaf?.FunRemoveChild(composite.Name);

            // Nếu là leaf root.
            if (parentLeaf == null && m_root.Name == composite.Name)
                m_root.FunShutdow();
        }

        // Hàm trợ giúp, xóa các node con ra khỏi map tìm kiếm.
        private void RemoveChildren(CompositeGroup leafGroup)
        {
            foreach (var leaf in leafGroup.FunGetChildren())
            {
                if (leaf is CompositeGroup group)
                    RemoveChildren(group);
                 
                m_compositeLookup.Remove(leaf.Name);
            }
        }

        // Thêm node composite mới vào một node có tên được chỉ định.
        private void AddComponentAt(string nameParent, Composite newComposite)
        {
            if (m_compositeLookup.ContainsKey(nameParent) == false)
            {
                Debug.LogWarning("could not find data for: " + nameParent);
                return;
            }

            // Lấy composite mà node mới muốn thêm vào.
            Composite composite = m_compositeLookup[nameParent];

            // TH1: composite là CompositeGroup
            // --------------------------------
            if (composite is CompositeGroup group)
            {
                newComposite.Parent = composite;
                group.FunAddChild(newComposite);
                m_compositeLookup.Add(newComposite.Name, newComposite);
                return;
            }

            // TH1: composite là CompositeLeaf
            // -------------------------------

            // Lấy node cha để chuyển đổi kiểu compositeLeaf thành CompositeGroup với node
            // được chỉ định để thêm composite mới vào.
            var compositeParent = composite.Parent as CompositeGroup;

            CompositeGroup compositeGroup = new CompositeGroup(composite);
            newComposite.Parent = compositeGroup; // mage-root.
            compositeGroup.FunAddChild(newComposite);

            // Thay đổi chức vụ
            compositeParent.FunRemoveChild(composite.Name);
            compositeParent.FunAddChild(compositeGroup);

            // Xoá node cha cũ và thêm lại node ý với kiểu mới.
            m_compositeLookup.Remove(composite.Name);
            m_compositeLookup.Add(compositeGroup.Name, compositeGroup);
            m_compositeLookup.Add(newComposite.Name, newComposite);
        }
    }
}