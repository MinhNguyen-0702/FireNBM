using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Lệnh thực hiện xây công trình.
    /// </summary>
    public class CommandUnitWorkerBuildStructure : ICommand
    {
        public void FunExecute()
        {
            // Thông báo unit worker chuẩn bị xây công trình.
            // Gửi thông điệp chứa lệnh hành động đến các unit được chọn.
            MessagingSystem.Instance.
                FunTriggerMessage(new MessageActionCommand(TypeRaceUnitWorker.BuildStructure, TypeObjectRTS.Unit), false);
        }
    }
}