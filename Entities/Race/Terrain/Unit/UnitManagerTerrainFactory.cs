namespace FireNBM
{
    /// <summary>
    ///     Nhà máy chịu trách nhiệm quản lý việc tạo ra 'UNIT' dựa trên công việc mà nó đại diện cho chủng tộc TERRAIN.
    /// </summary>
    public class UnitManagerTerrainFactory : UnitManagerRaceFactory
    {
        public UnitManagerTerrainFactory()
        {
            base.AddUnitFactoryEntry(TypeRaceUnit.Worker,       new UnitTerrainWorkerFactory());
            base.AddUnitFactoryEntry(TypeRaceUnit.BasicCombat,  new UnitTerrainBasicCombatFactory());
        }
    }
}