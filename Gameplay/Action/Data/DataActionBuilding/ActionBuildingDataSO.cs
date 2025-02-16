using System;
using UnityEngine;

namespace FireNBM
{
    [CreateAssetMenu(menuName = "FireNBM/RaceRTS/BuildingRace/New_ActionBuilding")]
    public class ActionBuildingDataSO : ActionData
    {
        public override ActionRTS FunCreateAction(Enum typeBuilding)
        {
            return typeBuilding switch
            {
                TypeRaceBuilding.Townhall => new ActionBuildingTownhallObjectRTS(TypeRaceBuildingTownhall.None, KeyCode.None),
                TypeRaceBuilding.SupplyBuildings => new ActionBuildingSupplyBuildingsObjectRTS(TypeRaceBuildingSupplyBuildings.None, KeyCode.None),
                TypeRaceBuilding.ProductionBuildings => new ActionBuildingProductionBuildingsObjectRTS(TypeRaceBuildingProductionBuildings.None, KeyCode.None),
                
                // Helper
                TypeRaceBuilding.Other => new ActionOtherObjectRTS(TypeOther.None, KeyCode.None),
                
                _=> null
            };
        }
    }
}