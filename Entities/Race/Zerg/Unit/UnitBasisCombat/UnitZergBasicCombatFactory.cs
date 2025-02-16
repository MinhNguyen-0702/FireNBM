using UnityEngine;

namespace FireNBM
{
    public class UnitZergBasicCombatFactory : UnitRaceTypeFactory
    {
        protected override bool InitializeDataTypeUnit(GameObject newUnit)
        {
            return true;
        }
    }
}