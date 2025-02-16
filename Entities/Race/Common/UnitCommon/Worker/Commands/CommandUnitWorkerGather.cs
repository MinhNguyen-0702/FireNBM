using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Lệnh thu thập tài nguyên mà người dùng đã nhấn chọn trong bảng Action.
    /// </summary>
    public class CommandUnitWorkerGather : ICommand
    {
        public void FunExecute()
        {    
             MessagingSystem.Instance.
                FunTriggerMessage(new MessageActionCommand(TypeRaceUnitWorker.Gather, TypeObjectRTS.Unit), false);
        }
    }
}