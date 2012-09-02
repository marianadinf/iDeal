namespace UIT.iDeal.Common.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler HandlerForCommand(ICommand command);
    }
}