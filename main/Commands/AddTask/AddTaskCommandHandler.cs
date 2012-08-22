using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Commands.AddTask
{
    public class AddTaskCommandHandler : ICommandHandler<AddTaskCommand>
    {
        private readonly ITasksRepository _repository;

        public AddTaskCommand Command { get; set; }

        public AddTaskCommandHandler(ITasksRepository repository)
        {
            _repository = repository;
        }


        public void Handle()
        {
            
            _repository.Save(Task.Create(Command.Description, Command.ToBeCompletedByDate));
        }

        
    }
}