using UnityEngine;

namespace FireNBM
{
    public class ActionBuildingSupplyBuildingsObjectRTS : ActionTypeObjectRTS<TypeRaceBuildingSupplyBuildings>
    {
        public ActionBuildingSupplyBuildingsObjectRTS(TypeRaceBuildingSupplyBuildings typeActionBuilding, KeyCode keyInputAction)
            : base(typeActionBuilding, keyInputAction)
        {
        }
    }
}