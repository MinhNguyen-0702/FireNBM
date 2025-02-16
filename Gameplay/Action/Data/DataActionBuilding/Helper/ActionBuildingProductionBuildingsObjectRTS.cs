using UnityEngine;

namespace FireNBM
{
    public class ActionBuildingProductionBuildingsObjectRTS : ActionTypeObjectRTS<TypeRaceBuildingProductionBuildings>
    {
        public ActionBuildingProductionBuildingsObjectRTS(TypeRaceBuildingProductionBuildings typeActionBuilding, KeyCode keyInputAction)
            : base(typeActionBuilding, keyInputAction)
        {
        }
    }
}