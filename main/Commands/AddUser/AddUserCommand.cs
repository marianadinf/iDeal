using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Commands.AddUser
{
   public class AddUserCommand : ICommand
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<ApplicationRole> ApplicationRoles { get; set; }
        public List<BusinessUnit> BusinessUnits { get; set; }
    }
}
