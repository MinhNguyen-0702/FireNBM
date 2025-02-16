using System;
using UnityEngine;

namespace FireNBM
{
    [CreateAssetMenu(menuName = "FireNBM/RaceRTS/UnitRace/New_ActionUnit")]
    public class ActionUnitDataSO : ActionData
    {
        public override ActionRTS FunCreateAction(Enum typeUnit)
        {
            return typeUnit switch
            {
                TypeRaceUnit.Base => new ActionUnitBaseObjectRTS(TypeRaceUnitBase.None, KeyCode.None),
                TypeRaceUnit.Worker => new ActionUnitWorkerObjectRTS(TypeRaceUnitWorker.None, KeyCode.None),
                TypeRaceUnit.ListBuilding => new ActionUnitListBuildingObjectRTS(TypeRaceBuilding.None, KeyCode.None),

                // Helper
                TypeRaceUnit.Other => new ActionOtherObjectRTS(TypeOther.None, KeyCode.None),
                
                _=> null
            };
        }
    }
}