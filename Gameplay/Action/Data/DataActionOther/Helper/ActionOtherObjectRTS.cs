using UnityEngine;

namespace FireNBM
{
    public class ActionOtherObjectRTS : ActionTypeObjectRTS<TypeOther>
    {
        public ActionOtherObjectRTS(TypeOther typeActionOther, KeyCode keyInputAction)
            : base(typeActionOther, keyInputAction)
        {
        }
    }
}