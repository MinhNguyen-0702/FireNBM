using UnityEngine;

namespace FireNBM
{
    public class BuildingZergTownhallFactory : BuildingRaceTypeFactory
    {
        protected override bool InitializeDataTypeBuilding(GameObject newUnit)
        {
            return true;
        }
    }
}