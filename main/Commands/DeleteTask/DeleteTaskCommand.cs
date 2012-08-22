using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.Common.Interfaces.Commands;

namespace UIT.iDeal.Commands.DeleteTask
{
   public class DeleteTaskCommand : ICommand
    {
       public Guid Id { get; set; }
    }
}
