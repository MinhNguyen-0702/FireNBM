using UnityEngine;
using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Nhận thông báo đối tượng cần xây dựng.
    /// </summary>
    public class MessageBuildStructure : IMessage
    {
        public UnderConstructionComp UnderConstruction;
        public MessageBuildStructure(UnderConstructionComp manager) => UnderConstruction = manager;
    }
}