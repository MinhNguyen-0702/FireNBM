using UnityEngine;

namespace FireNBM
{
    public class BuildingTerrainProductionBuildingsFactory : BuildingRaceTypeFactory
    {
        protected override bool InitializeDataTypeBuilding(GameObject newUnit)
        {
            return true;
        }
    }
}