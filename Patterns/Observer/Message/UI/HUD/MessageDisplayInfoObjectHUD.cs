using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class MessageDisplayInfoObjectHUD : IMessage
    {
        public TypeObjectRTS TypeObject;
        public GameObject ObjectDisplay;

        public MessageDisplayInfoObjectHUD(GameObject obj, TypeObjectRTS typeObject)
        {
            ObjectDisplay = obj;
            TypeObject = typeObject;
        }
    }
}