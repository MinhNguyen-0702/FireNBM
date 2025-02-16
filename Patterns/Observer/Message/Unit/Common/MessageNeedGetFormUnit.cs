using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Nhận lấy đội hình từ một unit cụ thể.
    /// </summary>
    public class MessageNeedGetFormUnit : IMessage
    {
        public FormationManager Formation;
        public MessageNeedGetFormUnit(FormationManager formation) => Formation = formation;
    }
}