using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Lệnh phòng thủ mà người dùng đã nhấn chọn trong bảng Action. 
    ///     <para>Đại diện một giá trị từ enum: <see cref="TypeRaceUnitBase"/></para>
    /// </summary>
    public class CommandUnitBaseHoldPosition : ICommand
    {
        public void FunExecute()
        {
            // Gửi thông điệp chứa lệnh hành động đến các unit được chọn.
            MessagingSystem.Instance.
                FunTriggerMessage(new MessageActionCommand(TypeRaceUnitBase.HoldPosition, TypeObjectRTS.Unit), false);
        }
    }
}
