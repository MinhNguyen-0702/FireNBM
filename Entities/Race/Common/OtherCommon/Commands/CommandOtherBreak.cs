using FireNBM.Pattern;

namespace FireNBM
{
    public class CommandOtherBreak : ICommand
    {
        public void FunExecute()
        {
            MessagingSystem.Instance.FunTriggerMessage(new MessageBreakAction(), false);
        }        
    }
}