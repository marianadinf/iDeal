using System;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Commands.EditTask;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.ViewModel.Tasks
{
    public class EditTaskForm : IMapToCommand<EditTaskCommand>, IMapFromDomain<Task>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime ToBeCompletedByDate { get; set; }
        public bool IsDone { get; set; } 
    }
    
}
