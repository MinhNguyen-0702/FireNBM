using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Xử lý lệnh enum liên quan đến việc tạo trạng thái cho unit.
    /// </summary>
    public static class BuildingStateHandler 
    {
        static private Dictionary<Type, IBuildingStateFactory> m_buildingStateHandlers;

        // -------------------------------------------------------------------------
        // CONSTRUCTOR
        // -----------
        ///////////////////////////////////////////////////////////////////////////

        static BuildingStateHandler()
        {
            m_buildingStateHandlers = new Dictionary<Type, IBuildingStateFactory>
            {
                { typeof(TypeRaceBuildingTownhall),             new BuildingTownhallStateFactory() },
                { typeof(TypeRaceBuildingSupplyBuildings),      new BuildingSupplyBuildingsStateFactory() },
                { typeof(TypeRaceBuildingProductionBuildings),  new BuildingProductionBuildingsStateFactory() },
            };
        }

        // --------------------------------------------------------------------------
        // PUBLIC METHODS
        // --------------
        /////////////////////////////////////////////////////////////////////////////

        static public IBuildingState FunCreate(Enum typeAction, GameObject building)
        {
            var type = typeAction.GetType();
            return m_buildingStateHandlers.ContainsKey(type) 
                   ? m_buildingStateHandlers[type].FunCreateState(typeAction, building)
                   : null;
        }
    }
}