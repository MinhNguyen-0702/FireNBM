using UnityEngine;

namespace FireNBM
{
    public class BuildingZergSupplyBuildingsFactory : BuildingRaceTypeFactory
    {
        protected override bool InitializeDataTypeBuilding(GameObject newUnit)
        {
            return true;
        }
    }
}