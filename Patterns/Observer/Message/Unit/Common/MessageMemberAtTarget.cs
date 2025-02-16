using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Thông báo trong đội rằng nó đã đến vị trí đã chỉ định.
    /// </summary>
    public class MessageMemberAtTarget : IMessage
    {
        public int IdForm;
        public GameObject Member;

        public MessageMemberAtTarget(GameObject owner, int id)
        {
            IdForm = id;
            Member = owner;
        }
    }
}