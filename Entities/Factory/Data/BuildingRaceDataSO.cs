using UnityEngine;

namespace FireNBM
{
    [CreateAssetMenu(menuName = "FireNBM/RaceRTS/BuildingRace/New_BuildingRaceData")]
    public class BuildingRaceDataSO : ScriptableObject
    {
        public int SizePoolBuilding;
        public GameObject BuildingPrefab;

        public int SizePoolUnderConstruction;
        public GameObject UnderConstructionPrefab;

        public BuildingDataSO Data;
        public ActionBuildingDataSO ActionData;
        public BuildingFlyweightDataSO FlyweightData;
    }
} 