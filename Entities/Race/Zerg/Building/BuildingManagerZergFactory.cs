namespace FireNBM
{
    /// <summary>
    ///     Nhà máy chịu trách nhiệm quản lý việc tạo ra 'BUILDING' dựa trên công việc mà nó đại diện cho chủng tộc ZERG.
    /// </summary>
    public class BuildingManagerZergFactory : BuildingManagerRaceFactory
    {
        public BuildingManagerZergFactory()
        {
            base.AddBuildingFactoryEntry(TypeRaceBuilding.Townhall,             new BuildingZergTownhallFactory());
            base.AddBuildingFactoryEntry(TypeRaceBuilding.SupplyBuildings,      new BuildingZergSupplyBuildingsFactory());
            base.AddBuildingFactoryEntry(TypeRaceBuilding.ProductionBuildings,  new BuildingZergProductionBuildingsFactory());
        }
    }
}