using FireNBM.Pattern;

namespace FireNBM
{
    /// <summary>
    ///     Lệnh dừng lại mà người dùng đã nhấn chọn trong bảng Action. 
    ///     <para>Đại diện một giá trị từ enum: <see cref="TypeRaceUnitBase"/></para>
    /// </summary>
    public class CommandUnitBaseStop : ICommand
    {
        public void FunExecute()
        {
            // Gửi thông điệp chứa lệnh hành động đến các unit được chọn.
            // Nơi nhận: 'SelectorUnitsController'
            MessagingSystem.Instance.
                FunTriggerMessage(new MessageActionCommand(TypeRaceUnitBase.Stop, TypeObjectRTS.Unit), false);
        }
    }
}
