using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class MessageGetNewObject : IMessage
    {
        public GameObject NewObject;
        public GameObject Prefab;

        public MessageGetNewObject(GameObject obj, GameObject prefab)
        {
            NewObject = obj;
            Prefab = prefab;
        }
    }
}