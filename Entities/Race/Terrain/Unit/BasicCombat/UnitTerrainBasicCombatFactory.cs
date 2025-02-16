using UnityEngine;

namespace FireNBM
{
    public class UnitTerrainBasicCombatFactory : UnitRaceTypeFactory
    {
        protected override bool InitializeDataTypeUnit(GameObject newUnit)
        {
            return true;
        }
    }
}