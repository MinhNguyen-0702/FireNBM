using UnityEditor;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Một tiện ích dùng để đối tên các đối tượng có trong Unity Editor.
    /// </summary>
    public class BatchRename : ScriptableWizard
    {
        // Base name
        public string BaseName = "MyObject_";
        // Start Count
        public int StartNumer = 0;
        // Increment
        public int Increment = 1;


        // --------------------------------------------------------------
        // API UNITY
        // ---------
        //////////////////////////////////////////////////////////////////

        [MenuItem("Edit/Batch Rename... %l")]
        private static void CreateWizard()
        {
            ScriptableWizard.DisplayWizard("Batch Rename", typeof(BatchRename), "Rename");
        }

        // Được gọi khi cửa sổ xuất hiện lần đầu.
        private void OnEnable()
        {
            UpdateSelectionHelper();
        }


        // --------------------------------------------------------------
        // FUNCTOR HELPER
        // --------------
        //////////////////////////////////////////////////////////////////

        // Được gọi khi thay đổi lựa chọn.
        private void OnSelectionChange()
        {
            UpdateSelectionHelper();
        }

        // Cập nhật số lượng đã chọn.
        private void UpdateSelectionHelper()
        {
            helpString = "";

            if (Selection.objects != null)
                helpString = "Number of objects selected: " + Selection.objects.Length;
        }

        // Đổi tên hàng loạt.
        private void OnWizardCreate()
        {
            // Thoát nếu ko chọn đối tượng.
            if (Selection.objects == null)
                return;

            // Mức tăng hiện tại
            int postFix = StartNumer;
            
            // Thực hiện đổi tên hàng loạt đối tượng.
            foreach (Object obj in Selection.objects)
            {
                obj.name = BaseName + postFix;
                postFix += Increment;
            }
        }
    }
}