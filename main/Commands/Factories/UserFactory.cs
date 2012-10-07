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
                                  IEnumerable<BusinessUnit> businessUnits,
            IEnumerable<Module> modules )
        {

            return Create(addUserCommand.Firstname,
                          addUserCommand.Lastname,
                          addUserCommand.Username,
                          addUserCommand.Email,
                          applicationRoles,
                          businessUnits,
                          modules);
        }

        public static User Create(string firstname,
                                  string lastname,
                                  string username,
                                  string email,
                                  IEnumerable<ApplicationRole> applicationRoles,
                                  IEnumerable<BusinessUnit> businessUnits,
            IEnumerable<Module> modules )
        {
            return
                new UserBuilder()
                    .WithFirstNames(firstname)
                    .WithLastNames(lastname)
                    .WithUserNames(username)
                    .WithEmails(email)
                    .WithApplicationRoles(applicationRoles.ToList())
                    .WithBusinessUnits(businessUnits.ToList())
                    .WithModules(modules.ToList());
        }
    }
}
