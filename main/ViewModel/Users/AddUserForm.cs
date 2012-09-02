using System;
using System.Collections.Generic;
using UIT.iDeal.Commands.AddUser;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.ViewModel.Users
{
    public class AddUserForm : IMapToCommand<AddUserCommand>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public List<Guid> ApplicationRoleIds { get; set; }
        public List<Guid> BusinessUnitIds { get; set; }
    }
}
