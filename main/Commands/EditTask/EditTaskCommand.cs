using System;
using UIT.iDeal.Common.Commands;

namespace UIT.iDeal.Commands.EditTask
{
    public class EditTaskCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime ToBeCompletedByDate { get; set; }
    }
}
