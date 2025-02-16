using System;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Trạng thái hiện tại của unit RTS.
    /// </summary>
    [AddComponentMenu("FireNBM/RaceRTS/Unit/Unit State Comp")]
    public class UnitStateComp : ObjectTypeBaseStateComp
    {  

        // ------------------------------------------------------------------------------
        // FUNSTION STATIC
        // ---------------
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Hàm tiện ích giúp thay đổi trạng thái cho unit. </summary>
        /// --------------------------------------------------------------
        public static void FunChangeStateUnit(GameObject unit, Enum typeAction)
        {
            if (unit == null )
            {
                Debug.Log("In UnitStateComp: Object Unit is NULL!");
                return;
            }

            // Lấy thành phần quản lý trạng thái của unit. 
            var stateUnit = unit.GetComponent<UnitStateComp>();

            // Thay đổi trạng thái nếu unit có.
            if (stateUnit.FunCheckState(typeAction) == true)
                stateUnit.FunChangeState(typeAction);
            else 
                Debug.Log("Unit not have state: " + typeAction);
        }
    }
}