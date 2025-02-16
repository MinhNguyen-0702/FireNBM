using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Muốn tạo một công trình mới.
    /// </summary>
    public class MessageInitializeBuilding : IMessage
    {
        public TypeNameBuilding TypeNameBuiding;
        public MessageInitializeBuilding(TypeNameBuilding typeNameBuiding) => TypeNameBuiding = typeNameBuiding;
    }
}