using System;
using UnityEngine;

namespace FireNBM
{
    public class StateBuildingBaseFree : IUnitState
    {
    
        // ------------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ///////////////////////////////////////////////////////////////////////////////////

        public StateBuildingBaseFree(GameObject owner)
        {
        }


        // --------------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        ////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        ///     Lấy tên kiểu của trạng thái Stop. </summary>
        /// ------------------------------------------------
        public Enum FunGetTypeState() => TypeRaceBuildingTownhall.Free;
        public void FunOnEnter() {}
        public void FunHandle() {}
        public void FunOnExit() {}
    }
}