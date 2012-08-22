using System;
using System.Collections.Generic;
using UIT.iDeal.Commands.DeleteTask;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Domain.Model;


namespace UIT.iDeal.ViewModel.Tasks
{
    
    public class TaskItemViewModel : IMapFromDomain<Task>, IMapToCommand<DeleteTaskCommand>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime ToBeCompletedByDate { get; set; }
    }
}