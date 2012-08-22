using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using UIT.iDeal.Commands.AddUser;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.ViewModel.Users
{
  public class AddUserForm:IMapToCommand<AddUserCommand>
    {
      public string Firstname { get; set; }
      public string Lastname { get; set; }
      public string Username { get; set; }
      public string Email { get; set; }
      public List<ApplicationRole> ApplicationRoles { get; set; }
      public List<BusinessUnit> BusinessUnits { get; set; }
    }
}
