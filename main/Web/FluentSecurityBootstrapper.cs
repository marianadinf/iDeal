using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks;
using FluentSecurity;
using System.Web.Mvc;
using UIT.iDeal.Common.Interfaces.Data;

namespace UIT.iDeal.Web
{
    public class FluentSecurityBootstrapper : IRunTaskAtStartup
    {

        private readonly IUserQuery _query;

        public void Execute()
        {
            SecurityConfigurator.Configure(configuration =>
            {
                // 1. Use the User Query to get the current user from DB
                //UserEFContainer dbcontext = new UserEFContainer();
                var CurrUser = _query.GetOne(user => user.Username == HttpContext.Current.User.Identity.Name);

                //2. Check whether the current user is authenticated
                // Let Fluent Security know how to get the authentication status of the current user
                configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                
                //3. Get its roles , NOTE: it won t be application role but the list of application permission 
                //var LstRoles = dbcontext.Users.Single(x => x.Id == 1).Roles.ToList().Select(x => x.RoleName);
                 //configuration.GetRolesFrom(() => dbcontext.Users.Single(x => x.Loginname == HttpContext.Current.User.Identity.Name).Roles.ToList().Select(x => x.RoleName));
                configuration.GetRolesFrom(() => CurrUser.ApplicationRoles.ToArray());

                // This is where you set up the policies you want Fluent Security to enforce on your controllers and actions
                configuration.ForAllControllers().DenyAnonymousAccess();
                //configuration.For<HomeController>(x => x.About()).RequireRole("Admin");
                //configuration.For<DealLeaderController>(x => x.Index()).RequireRole("DealLeader");
            });

            GlobalFilters.Filters.Add(new HandleSecurityAttribute(), 0);
        }

        public FluentSecurityBootstrapper(IUserQuery query )
        {
            _query = query;
        }
    }
}