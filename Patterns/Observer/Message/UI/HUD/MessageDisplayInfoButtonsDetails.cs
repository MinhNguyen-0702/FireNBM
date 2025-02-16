using System.Collections.Generic;
using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class MessageDisplayInfoButtonsDetails : IMessage
    {
        public List<GameObject> ListObjectSelector;
        public MessageDisplayInfoButtonsDetails(List<GameObject> listObjectSelcetor)
        {
            ListObjectSelector = listObjectSelcetor;
        }
    }
}