namespace FireNBM
{
    /// <summary>
    ///     Nhà máy chịu trách nhiệm quản lý việc tạo ra 'BUILDING' dựa trên công việc mà nó đại diện cho chủng tộc TERRAIN.
    /// </summary>
    public class BuildingManagerTerrainFactory : BuildingManagerRaceFactory
    {
        /// <summary>
        ///     Khởi tạo một thể hiện mới của lớp <see cref="BuildingManagerTerrainFactory"/>.
        ///     Thiết lập một dictionary rỗng để lưu trữ các factory công trình cho từng chủng tộc.
        /// </summary>
        public BuildingManagerTerrainFactory()
        {
            // Thêm logic khởi tạo tại đây nếu cần trong tương lai.
            base.AddBuildingFactoryEntry(TypeRaceBuilding.Townhall, new BuildingTerrainTownhallFactory());
            base.AddBuildingFactoryEntry(TypeRaceBuilding.SupplyBuildings, new BuildingTerrainSupplyBuildingsFactory());
            base.AddBuildingFactoryEntry(TypeRaceBuilding.ProductionBuildings, new BuildingTerrainProductionBuildingsFactory());
        }
    }
}
