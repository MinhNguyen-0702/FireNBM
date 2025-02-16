using UnityEngine;

namespace FireNBM
{
    public class BuildingTerrainTownhallFactory : BuildingRaceTypeFactory
    {
        protected override bool InitializeDataTypeBuilding(GameObject newUnit)
        {
            return true;
        }
    }
}