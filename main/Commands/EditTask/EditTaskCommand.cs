using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.Common.Interfaces.Commands;

namespace UIT.iDeal.Commands.EditTask
{
    public class EditTaskCommand:ICommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public DateTime ToBeCompletedByDate { get; set; } 
    }
}
