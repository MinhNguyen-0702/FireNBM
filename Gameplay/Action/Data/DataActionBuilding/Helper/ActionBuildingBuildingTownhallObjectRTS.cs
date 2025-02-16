using UnityEngine;

namespace FireNBM
{
    public class ActionBuildingTownhallObjectRTS : ActionTypeObjectRTS<TypeRaceBuildingTownhall>
    {
        public ActionBuildingTownhallObjectRTS(TypeRaceBuildingTownhall typeActionBuilding, KeyCode keyInputAction)
            : base(typeActionBuilding, keyInputAction)
        {
        }
    }
}