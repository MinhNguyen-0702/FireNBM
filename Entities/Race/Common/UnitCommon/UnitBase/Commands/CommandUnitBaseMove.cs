using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Lệnh di chuyển mà người dùng đã nhấn chọn trong bảng Action. 
    ///     <para>Đại diện một giá trị từ enum: <see cref="TypeRaceUnitBase"/></para>
    /// </summary>
    public class CommandUnitBaseMove : ICommand
    {
        public void FunExecute()
        {
            // Gửi thông điệp chứa lệnh hành động đến các unit được chọn.
            MessagingSystem.Instance.
                FunTriggerMessage(new MessageActionCommand(TypeRaceUnitBase.Move, TypeObjectRTS.Unit), false);
        }
    }
}
