using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     Là thành phần trong Composite được sử dụng để quản lý các node con.
    /// </summary>
    public class CompositeGroup : Composite
    {
        private Dictionary<string, Composite> m_childrens;


        // ----------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        // //////////////////////////////////////////////////////////////////////

        public CompositeGroup(string name, Composite parent, CompositeData data) 
            : base(name, parent, data)
        {
            m_childrens = new Dictionary<string, Composite>();
        }

        public CompositeGroup(Composite compositeGroup) 
            : base(compositeGroup)
        {
            m_childrens = new Dictionary<string, Composite>();
        }


        // ---------------------------------------------------------------------
        // FUNSTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thêm một node mới vào một nhóm. </summary>
        /// ----------------------------------------------
        public void FunAddChild(Composite child)
        {
            // Thoát nếu đối tượng thêm vào rỗng.
            if (child == null) 
                return;

            // Thoát nếu đã thêm Composite này.
            if (m_childrens.ContainsKey(child.Name) == true)
            {
                Debug.LogWarning($"In CompositeGroup, Object with name '{child.Name}' has existed");
                return;
            }
            m_childrens.Add(child.Name, child);
        }

        /// <summary>
        ///     Xóa một node ra khỏi nhóm. </summary>
        /// -----------------------------------------
        public void FunRemoveChild(string nameChild)
        {
            // Thoát nếu không tồn tại node Composite cần xóa.
            if (m_childrens.ContainsKey(nameChild) == false)
            {
                Debug.LogWarning($"In CompositeGroup, Could not find Object with name '{nameChild}' to remove");
                return;
            }
            m_childrens[nameChild].FunShutdow();
            m_childrens.Remove(nameChild);
        }


        /// <summary>
        ///     Thực hiện một action trên nút này và tất cả các node con mà nó quản lý.</summary>
        /// -------------------------------------------------------------------------------------
        public override void FunExecuteAction(Action<Composite> action)
        {
            // Thực hiện trên node lá này.
            action?.Invoke(this);

            // Thực hiện trên các node lá con.
            foreach (var child in m_childrens.Values)
                child.FunExecuteAction(action);
        }

        /// <summary>
        ///     Xóa dữ liệu trước khi bị hủy. </summary>
        /// -------------------------------------------- 
        public override void FunShutdow()
        {
            // Xóa dư liệu các node Composite con.
            foreach (var child in m_childrens.Values)
                child.FunShutdow();
               
            // Xóa dũ liệu node hiện tại.
            m_childrens.Clear();
            CleanData();
        }

        /// <summary>
        ///     Lấy danh sách các node Composite con. </summary>
        /// ----------------------------------------------- 
        public List<Composite> FunGetChildren() => m_childrens.Values.ToList();


        // ----------------------------------------------------------------------
        // FUNSTION HELPER
        // ---------------
        // //////////////////////////////////////////////////////////////////////

        // Xóa dữ liệu mà nó chứa.
        private void CleanData()
        {
        }
    }
} 