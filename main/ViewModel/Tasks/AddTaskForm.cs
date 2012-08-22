using System;
using UIT.iDeal.Commands.AddTask;
using UIT.iDeal.Common.Interfaces.ObjectMapping;

namespace UIT.iDeal.ViewModel.Tasks
{
    public class AddTaskForm : IMapToCommand<AddTaskCommand>
    {
        public string Description { get; set; }
        public DateTime ToBeCompletedByDate { get; set; }
    }
}