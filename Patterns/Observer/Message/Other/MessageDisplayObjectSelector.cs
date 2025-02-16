using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class MessageDisplayObjectSelector : IMessage
    {
        public GameObject ObjectSelector;
        public MessageDisplayObjectSelector(GameObject objSelector)
        {
            ObjectSelector = objSelector;
        }
    }
}