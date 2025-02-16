using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class MessageUpdateResourceSupplysHUD : IMessage
    {
        public int CountResourceSupplys;

        public MessageUpdateResourceSupplysHUD(int count)
        {
            CountResourceSupplys = count;
        }
    }
}