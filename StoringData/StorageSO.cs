using System.Collections.Generic;
using UnityEngine;

namespace FireNBM
{
    public enum TypePlayer { Player, Player1 };

    [CreateAssetMenu(menuName = "FireNBM/StorageSO")]
    public class StorageSO : ScriptableObject
    {
        public List<RaceDataSO> DataRaces;
        public ActionUnitDataSO UnitActionDefault;
        public ActionBuildingDataSO BuildingActionDefault;
    }
} 