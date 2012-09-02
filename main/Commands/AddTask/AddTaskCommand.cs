using System;
using UIT.iDeal.Common.Commands;

namespace UIT.iDeal.Commands.AddTask
{
    public class AddTaskCommand : ICommand
    {   
        public string Description { get; set; }
        public DateTime ToBeCompletedByDate { get; set; }
    }
}