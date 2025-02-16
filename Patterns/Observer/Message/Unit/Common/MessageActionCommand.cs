using System;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Mẫu thông điệp chứa thông tin lệnh hành động mà người chơi nhấn.
    /// </summary>
    public class MessageActionCommand : IMessage
    {
        public Enum Action;
        public TypeObjectRTS TypeObject;

        public MessageActionCommand(Enum typeAction, TypeObjectRTS typeObject)
        {
            Action = typeAction;
            TypeObject = typeObject;
        } 
    }
}