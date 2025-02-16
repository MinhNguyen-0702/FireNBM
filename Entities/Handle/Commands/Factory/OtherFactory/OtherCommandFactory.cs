using FireNBM.Pattern;
using System;

namespace FireNBM
{
    public class OtherCommandFactory : ICommandFacrory
    {
        public ICommand FunCreateCommand(Enum typeAction)
        {
            return (typeAction) switch
            {
                TypeOther.None => null,
                TypeOther.Break => new CommandOtherBreak(),
                _=> null
            };
        }
    }
}