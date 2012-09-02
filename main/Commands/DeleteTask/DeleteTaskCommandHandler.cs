using UIT.iDeal.Common.Commands;
using UIT.iDeal.Common.Interfaces.Data;


namespace UIT.iDeal.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : ICommandHandler<DeleteTaskCommand>
    {
        readonly ITasksRepository _repository;

        public DeleteTaskCommand Command { get; set; }

        public DeleteTaskCommandHandler(ITasksRepository repository)
        {
            _repository = repository;
        }

        public void Handle()
        {
            var aTaskToDelete = _repository.Get(Command.Id);
            _repository.Delete(aTaskToDelete);
        }

    }
}
