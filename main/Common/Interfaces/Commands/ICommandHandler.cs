namespace UIT.iDeal.Common.Interfaces.Commands
{
    public interface ICommandHandler
    {
        void Handle();
    }

    public interface ICommandHandler<TCommand> : ICommandHandler where TCommand : class,ICommand
    {
        TCommand Command { get; set; }
    }
}