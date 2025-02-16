namespace FireNBM
{
    /// <summary>
    ///     Nhà máy sản xuất các đối tượng liên quan đến chủng tộc ZERG.
    /// </summary>
    public class RaceZergFactory : IRaceTypeFactory
    {
        private UnitManagerZergFactory m_unitManagerFactory;
        private BuildingManagerZergFactory m_buildingManagerFactory;


        // ------------------------------------------------------------------------------------------
        //  CONSTRUCTOR
        // ------------
        // ///////////////////////////////////////////////////////////////////////////////////////////

        public RaceZergFactory()
        {
            m_unitManagerFactory = new UnitManagerZergFactory();
            m_buildingManagerFactory = new BuildingManagerZergFactory();
        }


        // --------------------------------------------------------------------------------------------
        // FUNCTION PUBLIC
        // ---------------
        // /////////////////////////////////////////////////////////////////////////////////////////////

        public bool FunInitializeDataRace(RaceDataSO raceData)
        {
            if (m_unitManagerFactory.FunInitializeDataUnitRace(raceData.ListRaceUnitData) == false)
            {
                DebugUtils.FunLog($"Error: Failed to initialize unit data for race Zerg.");
                return false;
            }

            if (m_buildingManagerFactory.FunInitializeDataBuildingRace(raceData.ListRaceBuildingData) == false)
            {
                DebugUtils.FunLog($"Error: Failed to initialize building data for race Zerg.");
                return false;
            }

            return true;
        }

        public UnitManagerRaceFactory FunGetUnitManagerFactory() => m_unitManagerFactory;
        public BuildingManagerRaceFactory FunGetBuildingManagerFactory() => m_buildingManagerFactory;
    }
} 