using System;
using FireNBM.Pattern;

namespace FireNBM
{
    public interface ICommandFacrory
    {
        ICommand FunCreateCommand(Enum typeAction);
    }
}