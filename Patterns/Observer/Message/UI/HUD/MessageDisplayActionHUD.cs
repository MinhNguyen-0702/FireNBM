using FireNBM.Pattern;

namespace FireNBM
{
    public class MessageDisplayActionHUD : IMessage
    {
        // public Type
        public ActionData ActionRTS;

        public MessageDisplayActionHUD(ActionData action)
        {
            ActionRTS = action;
        }
    }
}