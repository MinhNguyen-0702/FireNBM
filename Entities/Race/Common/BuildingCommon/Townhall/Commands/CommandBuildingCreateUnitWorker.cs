using FireNBM.Pattern;
using UnityEngine;

namespace FireNBM
{
    public class CommandBuildingCreateUnitWorker : ICommand
    {
        public void FunExecute()
        {
            MessagingSystem.Instance.
                FunTriggerMessage(new MessageActionCommand(TypeRaceBuildingTownhall.CreateUnitWorker, TypeObjectRTS.Building), false);
        }   
    }
}