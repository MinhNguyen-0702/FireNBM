using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Tin nhắn cho biết rằng đối tượng đã bị tiêu diệt.
    /// </summary>
    public class MessageKillTarget : IMessage
    {
        public GameObject Target;  
        public MessageKillTarget(GameObject target) => Target = target;
    }
}