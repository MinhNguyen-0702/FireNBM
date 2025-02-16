using FireNBM.Pattern;

namespace FireNBM
{
    public class CommandListBuildingTownhall : ICommand
    {
        public void FunExecute()
        {
            // Gửi thông điệp đến BuildingConstruction để xây dựng công trình.
            // Khi xây xong thì gửi thông báo đến unit worker đang trong quá trình đợi để xây dựng công trình.
            // Khi nhấn lệnh building thì unit worker đăng ký thông điệp nhận vị trí công trình đang xây.

            MessagingSystem.Instance.FunTriggerMessage(new MessageInitializeBuilding(TypeNameBuilding.CommandCenter), false);
        }
    }
}