using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class MessageUpdateEndGame : IMessage
    {
        public bool IsVictory;
        public string TimeLeft;
        public string EnemyCount;
    }
}