namespace UIT.iDeal.Common.Interfaces.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler HandlerForCommand(ICommand command);
    }
}