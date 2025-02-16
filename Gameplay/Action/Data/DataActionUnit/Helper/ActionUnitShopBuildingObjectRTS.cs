using UnityEngine;

namespace FireNBM
{
    public class ActionUnitListBuildingObjectRTS :  ActionTypeObjectRTS<TypeRaceBuilding>
    {
        public ActionUnitListBuildingObjectRTS(TypeRaceBuilding typeActionUnit, KeyCode keyInputAction)
            : base(typeActionUnit, keyInputAction)
        {
        }
    }
}