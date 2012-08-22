using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.Commands.EditTask
{
    public class EditTaskCommandHanlder:ICommandHandler<EditTaskCommand>
    {
        private readonly ITasksRepository _repository;

        public EditTaskCommand Command { get; set; }

        public EditTaskCommandHanlder(ITasksRepository repository)
        {
            _repository = repository;
        }

        
        public void Handle()
        {
            var aTaskToModify = _repository.Get(Command.Id);
            aTaskToModify.Edit(Command.Description , Command.IsDone, Command.ToBeCompletedByDate);
            //aTaskToModify.ToBeCompletedByDate = DateTime.Now;
            //_repository.Save(aTaskToModify);

            _repository.Save(aTaskToModify);
        }
    }
}
