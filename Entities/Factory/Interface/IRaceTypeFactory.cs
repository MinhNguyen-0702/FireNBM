namespace FireNBM
{
    public interface IRaceTypeFactory
    {
        public bool FunInitializeDataRace(RaceDataSO raceData);
        public UnitManagerRaceFactory FunGetUnitManagerFactory();
        public BuildingManagerRaceFactory FunGetBuildingManagerFactory();
    }
}