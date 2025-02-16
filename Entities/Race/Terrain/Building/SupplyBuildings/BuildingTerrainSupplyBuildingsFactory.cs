using UnityEngine;

namespace FireNBM
{
    public class BuildingTerrainSupplyBuildingsFactory : BuildingRaceTypeFactory
    {
        protected override bool InitializeDataTypeBuilding(GameObject newUnit)
        {
            return true;
        }
    }
}