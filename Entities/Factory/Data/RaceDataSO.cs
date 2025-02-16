using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    [System.Serializable]
    public class UnitRaceData
    {
        public TypeRaceUnit ObjectRace;
        public List<UnitRaceDataSO> ListDataSO = new List<UnitRaceDataSO>();
    }

    [System.Serializable]
    public class BuildingRaceData
    {
        public TypeRaceBuilding ObjectRace;
        public List<BuildingRaceDataSO> ListDataSO = new List<BuildingRaceDataSO>();
    }

    /// <summary>
    ///     Dữ liệu cần có của một chủng tộc.
    /// </summary>
    [CreateAssetMenu(menuName = "FireNBM/RaceRTS/Data/New_RaceData")]
    public class RaceDataSO : ScriptableObject
    {
        public TypeRaceRTS NameRaceRTS;
        public List<UnitRaceData> ListRaceUnitData = new List<UnitRaceData>();
        public List<BuildingRaceData> ListRaceBuildingData = new List<BuildingRaceData>();
    } 
} 