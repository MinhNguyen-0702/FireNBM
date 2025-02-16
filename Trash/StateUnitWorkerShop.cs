// using System;
// using UnityEngine;

// namespace FireNBM
// {
//     /// <summary>
//     ///     Trạng thái thu thập tài nguyên của đơn vị.
//     /// </summary>
//     public class StateUnitWorkerShop : IUnitState
//     {
//         private UnitDataComp m_data;
//         private UnitControllerComp m_controller;
 

//         // ---------------------------------------------------------------------
//         // CONSTRUCTOR
//         // -----------
//         ////////////////////////////////////////////////////////////////////////

//         public StateUnitWorkerShop(GameObject owner)
//         {
//             m_data = owner.GetComponent<UnitDataComp>();
//             m_controller = owner.GetComponent<UnitControllerComp>();

//             if (m_data == null || m_controller == null)
//             {
//                 Debug.LogError("In GatherState, Error: m_data or m_controller is Null.");
//                 return;
//             }
//         }


//         // -----------------------------------------------------------------------
//         // PUBLIC METHODS
//         // --------------
//         //////////////////////////////////////////////////////////////////////////

//         /// <summary>
//         ///     Lấy tên kiểu trạng thái Thêm vật tư mới.</summary>
//         /// ------------------------------------------------------
//         public Enum FunGetTypeState() => TypeRaceUnitWorker.Shop;

//         public void FunOnEnter()
//         {
//             // TODO: Thiết lập trạng thái Buy cho unit.
//         }

//         public void FunOnExit()
//         {
//             // TODO: Trở lại trạng thái mặc định.
//         }
 
//         public void FunHandle()
//         {
//         }
//     }
// }