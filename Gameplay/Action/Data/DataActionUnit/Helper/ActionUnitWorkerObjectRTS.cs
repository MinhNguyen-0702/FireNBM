using UnityEngine;

namespace FireNBM
{
    public class ActionUnitWorkerObjectRTS : ActionTypeObjectRTS<TypeRaceUnitWorker>
    {
        public ActionUnitWorkerObjectRTS(TypeRaceUnitWorker typeActionUnit, KeyCode keyInputAction)
            : base(typeActionUnit, keyInputAction)
        {
        }
    }
}