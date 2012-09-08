using System.Collections.Generic;
using System.Linq;
using UIT.iDeal.Commands.AddUser;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Domain.Model.ReferenceData;

namespace UIT.iDeal.Commands.Factories
{
    public static class UserFactory
    {
        public static User Create(AddUserCommand addUserCommand,
                                  IEnumerable<ApplicationRole> applicationRoles,
                                  IEnumerable<BusinessUnit> businessUnits)
        {

            return Create(addUserCommand.Firstname,
                          addUserCommand.Lastname,
                          addUserCommand.Username,
                          addUserCommand.Email,
                          applicationRoles,
                          businessUnits);
        }

        public static User Create(string firstname,
                                  string lastname,
                                  string username,
                                  string email,
                                  IEnumerable<ApplicationRole> applicationRoles,
                                  IEnumerable<BusinessUnit> businessUnits)
        {
            return
                new UserBuilder()
                    .WithFirstNames(firstname)
                    .WithLastNames(lastname)
                    .WithUserNames(username)
                    .WithEmails(email)
                    .WithApplicationRoles(applicationRoles.ToList())
                    .WithBusinessUnits(businessUnits.ToList());
        }
    }
}
