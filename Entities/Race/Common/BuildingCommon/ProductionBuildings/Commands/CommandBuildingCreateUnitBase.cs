using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class CommandBuildingCreateUnitBase : ICommand
    {
        public void FunExecute()
        {
            DebugUtils.FunLog("Okr");
            
            MessagingSystem.Instance.
                FunTriggerMessage(new MessageActionCommand(TypeRaceBuildingProductionBuildings.CreateUnitBase, TypeObjectRTS.Building), false);
        }
    }
}