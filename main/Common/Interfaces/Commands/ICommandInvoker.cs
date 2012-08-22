using UIT.iDeal.Common.Errors;

namespace UIT.iDeal.Common.Interfaces.Commands
{
    public interface ICommandInvoker
    {
        ExecutionResult Execute<TCommand>(TCommand command) where TCommand : class, ICommand;
    }
}