using UnityEngine;

namespace FireNBM
{
    public class UnitTerrainWorkerFactory : UnitRaceTypeFactory
    {
        protected override bool InitializeDataTypeUnit(GameObject newUnit)
        {
            return true;
        }
    }
}