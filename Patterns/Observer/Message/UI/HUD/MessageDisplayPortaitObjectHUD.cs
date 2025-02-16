using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class MessageDisplayPortaitObjectHUD : IMessage
    {
        public TypeNameUnit NameUnit;
        public MessageDisplayPortaitObjectHUD(TypeNameUnit name)
            => NameUnit = name;
    }
}