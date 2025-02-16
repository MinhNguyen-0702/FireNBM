namespace FireNBM
{
    /// <summary>
    ///     Nhà máy chịu trách nhiệm quản lý việc tạo ra 'UNIT' dựa trên công việc mà nó đại diện cho chủng tộc ZERG.
    /// </summary>
    public class UnitManagerZergFactory : UnitManagerRaceFactory
    {
        public UnitManagerZergFactory()
        {
            base.AddUnitFactoryEntry(TypeRaceUnit.Worker,       new UnitZergWorkerFactory());
            base.AddUnitFactoryEntry(TypeRaceUnit.BasicCombat,  new UnitZergBasicCombatFactory());
        }
    }
}