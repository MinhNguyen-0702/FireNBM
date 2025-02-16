using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class CommandListBuildingProductionBuildings : ICommand
    {
        public void FunExecute()
        {
            MessagingSystem.Instance.FunTriggerMessage(new MessageInitializeBuilding(TypeNameBuilding.BuildingWarror), false);
        }
    }
}