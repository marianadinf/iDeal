using System.Collections.Generic;
using UIT.iDeal.Commands.AddUser;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Commands.Factories
{
    public static class UserFactory
    {
        public static User Create(AddUserCommand  addUserCommand)
        {

            return Create(addUserCommand.Firstname,
                          addUserCommand.Lastname,
                          addUserCommand.Username,
                          addUserCommand.Email,
                          addUserCommand.ApplicationRoles,
                          addUserCommand.BusinessUnits);
        }

        public static User Create(string firstname,
                                  string lastname,
                                  string username,
                                  string email,
                                  List<ApplicationRole> applicationRoles = null, 
                                  List<BusinessUnit> businessUnits = null)
        {
            return
                new UserBuilder()
                    .WithFirstName(firstname)
                    .WithLastName(lastname)
                    .WithUserName(username)
                    .WithEmail(email)
                    .WithApplicationRoles(applicationRoles)
                    .WithBusinessUnits(businessUnits);
        }
    }
}
