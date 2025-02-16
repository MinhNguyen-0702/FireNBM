using System;

namespace FireNBM.Pattern
{
    /// <summary>
    ///     
    /// </summary>
    public abstract class Composite
    {
        public string Name;            // Tên của node.
        public Composite Parent;       // Node cha.
        public CompositeData Data;     // Dữ liệu của node.

        public static string NAME_ROOT = "Root";

        
        // --------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        /////////////////////////////////////////////////////////////////////////////

        public Composite(Composite composite)
        {
            Name = composite.Name;
            Parent = composite.Parent;
            Data = composite.Data;
        } 

        public Composite(string name, Composite parent, CompositeData data)
        {
            Name = name;
            Parent = parent;
            Data = data;
        } 


        // ---------------------------------------------------------------------------------
        // METHOD PUBLIC
        // -------------
        // /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Thực hiện một hành động với tại nhánh composite này.</summary>
        /// ------------------------------------------------------------------ 
        public abstract void FunExecuteAction(Action<Composite> action);

        /// <summary>
        ///     Xóa dữ liệu trước khi đối tượng bị hủy.</summary>
        /// -----------------------------------------------------
        public abstract void FunShutdow();


        /// <summary>
        ///     Tìm và trả về tham chiếu T nếu tìm thấy hoặc null nếu thất bại. </summary>
        /// -----------------------------------------------------------------------------
        /// <typeparam name="T"> Kiểu giữ liệu cần tìm.</typeparam>
        public T FindInHierarchy<T>(System.Func<CompositeData, T> predicate) where T : class
        {
            // Kiểm tra dữ liệu trong node hiện tại.
            T result = predicate(Data);
            if (result != null)
                return result;
            
            // Nếu không tìm thấy, tiếp tực tìm node cha.
            return Parent?.FindInHierarchy(predicate);
        }

        /// <summary>
        ///     Tìm và trả về giá trị nếu tìm thấy hoặc null nếu thất bại. </summary>
        /// -------------------------------------------------------------------------
        /// <typeparam name="T"> Kiểu giữ liệu cần tìm.</typeparam>
        public T? FindInHierarchy<T>(System.Func<CompositeData, T?> predicate) where T : struct
        {
            T? result = predicate(Data);
            if (result.HasValue)
                return result;

            return Parent?.FindInHierarchy(predicate);
        }
    }
}


