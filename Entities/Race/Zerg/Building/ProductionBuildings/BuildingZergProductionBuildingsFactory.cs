using UnityEngine;

namespace FireNBM
{
    public class BuildingZergProductionBuildingsFactory : BuildingRaceTypeFactory
    {
        protected override bool InitializeDataTypeBuilding(GameObject newUnit)
        {
            return true;
        }
    }
}