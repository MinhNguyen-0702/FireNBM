using System;
using UnityEngine;

namespace FireNBM
{
    public class StateBuildingProductionBuildingFree : IUnitState
    {
    
        // ------------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ///////////////////////////////////////////////////////////////////////////////////

        public StateBuildingProductionBuildingFree(GameObject owner)
        {
        }


        // --------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy tên kiểu của trạng thái Stop. </summary>
        /// ------------------------------------------------
        public Enum FunGetTypeState() => TypeRaceBuildingProductionBuildings.Free;
        public void FunOnEnter() {}
        public void FunHandle() {}
        public void FunOnExit() {}
    }
}