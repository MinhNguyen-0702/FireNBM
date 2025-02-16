using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Gửi tín hiệu cần lấy đội hình dựa trên unit cụ thể.
    /// </summary>
    public class MessageGetFormUnit : IMessage
    {
        public GameObject Unit;
        public MessageGetFormUnit(GameObject unit) => Unit = unit;
    }   
}