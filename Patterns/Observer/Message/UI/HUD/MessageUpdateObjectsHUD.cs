using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class MessageUpdateObjectsHUD : IMessage
    {
        public bool IsDifferentObject;
        public TypeObjectRTS TypeObject;
        public HashSet<GameObject> ListObject;

        public MessageUpdateObjectsHUD(TypeObjectRTS typeObject, HashSet<GameObject> gameObjects, bool isDifferentObject)
        {
            TypeObject = typeObject;
            ListObject = gameObjects;
            IsDifferentObject = isDifferentObject;
        }
    }
}