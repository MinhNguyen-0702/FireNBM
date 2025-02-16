using UnityEngine;

namespace FireNBM
{
    public class ActionUnitBaseObjectRTS : ActionTypeObjectRTS<TypeRaceUnitBase>
    {
        public ActionUnitBaseObjectRTS(TypeRaceUnitBase typeActionUnit, KeyCode keyInputAction)
            : base(typeActionUnit, keyInputAction)
        {
        }
    }
}