using System;
using System.Collections.Generic;
using UIT.iDeal.Common.Commands;

namespace UIT.iDeal.Commands.AddUser
{
   public class AddUserCommand : ICommand
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Guid> ApplicationRoleIds { get; set; }
        public List<Guid> BusinessUnitIds { get; set; }
        public List<Guid> ModuleIds { get; set; }
    }
}
