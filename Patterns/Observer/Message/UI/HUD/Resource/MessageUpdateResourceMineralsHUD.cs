using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class MessageUpdateResourceMineralsHUD : IMessage
    {
        public int CountResourceMinerals;

        public MessageUpdateResourceMineralsHUD(int count)
        {
            CountResourceMinerals = count;
        }
    }
}