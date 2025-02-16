namespace FireNBM
{
    /// <summary>
    ///     Nhà máy sản xuất các đối tượng liên quan đến chủng tộc TERRAIN.
    /// </summary>
    public class RaceTerrainFactory : IRaceTypeFactory
    {
        private UnitManagerTerrainFactory m_unitManagerFactory;
        private BuildingManagerTerrainFactory m_buildingManagerFactory;


        // ------------------------------------------------------------------------------------------
        //  CONSTRUCTOR
        // ------------
        // ///////////////////////////////////////////////////////////////////////////////////////////

        public RaceTerrainFactory()
        {
            m_buildingManagerFactory = new BuildingManagerTerrainFactory();
            m_unitManagerFactory = new UnitManagerTerrainFactory();
        }


        // --------------------------------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////////////////

        public bool FunInitializeDataRace(RaceDataSO raceData)
        {
            if (m_unitManagerFactory.FunInitializeDataUnitRace(raceData.ListRaceUnitData) == false)
            {
                DebugUtils.FunLog("Lỗi: Không thể khởi tạo dữ liệu 'Unit' cho chủng tộc - " + raceData.NameRaceRTS);
                return false;
            }
            if (m_buildingManagerFactory.FunInitializeDataBuildingRace(raceData.ListRaceBuildingData) == false)
            {
                DebugUtils.FunLog("Lỗi: Không thể khởi tạo dữ liệu 'Building' cho chủng tộc - " + raceData.NameRaceRTS);
                return false;
            }
            return true;
        }

        public UnitManagerRaceFactory FunGetUnitManagerFactory() => m_unitManagerFactory;
        public BuildingManagerRaceFactory FunGetBuildingManagerFactory() => m_buildingManagerFactory;
    }
}