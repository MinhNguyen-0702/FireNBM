using UnityEngine;

namespace FireNBM
{
    /// <summary>
    ///     Chịu trách nhiệm tạo worker unit cho chủng tộc Zerg.
    /// </summary>
    public class UnitZergWorkerFactory : UnitRaceTypeFactory
    {
        protected override bool InitializeDataTypeUnit(GameObject newUnit)
        {
            return true;
        }
    }
}