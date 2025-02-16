// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Sắp xếp vị trí các đơn vị khi có vị trí di chuyển mới.
//     /// </summary>
//     public class SortUnits
//     {
//         private bool m_checkSort;
//         private const float DISTANCE_BETWEEN_UNITS = 3f;

//         private HashSet<GameObject> m_selectedUnits;
//         private Dictionary<GameObject, Vector3> m_posTarget;

//         private static SortUnits m_instance;
//         public static SortUnits Instance
//         { 
//             get{ return m_instance ?? (m_instance = new SortUnits()); } 
//         }

//         private SortUnits()
//         {
//             m_checkSort = false;
//             m_selectedUnits = SelectorUnitsComponent.Instance.FunGetUnitsSelected();
//             m_posTarget = new Dictionary<GameObject, Vector3>();
//         }


//         // --------------------------------------------------------------------------------
//         // FUNSTOR
//         // --------
//         // /////////////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Lấy vị trí đã được sắp xếp đội hình.</summary>
//         /// --------------------------------------------------
//         public Vector3 FunGetPosition(GameObject outUnit, Vector3 currentPos)
//         {   
//             // Sắp xếp lại nếu chưa được xử lý.
//             if (m_checkSort == false) 
//                 SortAllPosUnit();
            
//             return m_posTarget[outUnit] + currentPos;
//         }

//         /// <summary>
//         ///     Reset lại vị trí nếu có danh sách unit mới.</summary>
//         /// ---------------------------------------------------------
//         public void FunResetSortUnits() => m_checkSort = false;

        
//         // ---------------------------------------------------------------------------------
//         // FUNSTION HELPER
//         // ---------------
//         // /////////////////////////////////////////////////////////////////////////////////
        
//         // Sắp xếp lại vị trí.
//         private void SortAllPosUnit()
//         {
//             // Lưu trữ số lượng unit theo chiều ngang và chiều dọc.
//             int i = 0;
//             int column = 0;
//             int rows = Mathf.RoundToInt(Mathf.Sqrt(m_selectedUnits.Count)); 

//             foreach (GameObject unit in m_selectedUnits)
//             {
//                 // Tính số đơn vị theo hàng ngang.
//                 if (i > 0 && i % rows == 0) 
//                     column++;

//                 // Sắp xếp đơn vị cách nhau 1 khoảng nhất định.
//                 float offsetX = (i % rows) * DISTANCE_BETWEEN_UNITS;
//                 float offsetZ = column * DISTANCE_BETWEEN_UNITS;
                
//                 Vector3 target = new Vector3(offsetX, 0f, offsetZ);

//                 // Đặt lại vị trí cho unit.
//                 if (m_posTarget.ContainsKey(unit) == false)
//                     m_posTarget.Add(unit, target);
//                 else
//                     m_posTarget[unit] = target;

//                 ++i;
//             }

//             // Thông báo đã được sắp xếp.
//             m_checkSort = true;
//         }
//     }
// }