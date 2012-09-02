using System;
using UIT.iDeal.Common.Commands;

namespace UIT.iDeal.Commands.DeleteTask
{
   public class DeleteTaskCommand : ICommand
    {
       public Guid Id { get; set; }
    }
}
