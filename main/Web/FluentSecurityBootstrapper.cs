using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks;
using FluentSecurity;
using System.Web.Mvc;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model.ReferenceData;
using UIT.iDeal.Common.Builders.DataSources.ReferenceData;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.Web.Controllers;
using System.Web.UI.WebControls;
using System.Web.Routing;


namespace UIT.iDeal.Web
{
    public class FluentSecurityBootstrapper : IRunTaskAtStartup
    {

        private readonly IUserQuery _query;

        public void Execute()
        {
            SecurityConfigurator.Configure(configuration =>
            {
                /**************************************
                 Simulation assigning role via userbuilder
                 This module will progressively evolve once we have all pieces into place
                 1.Get the user authenticated, get its login name
                 2.Check this user in the entity list
                 3.Get its roles for (now we are using application role but we need to use application permission which is the lowest granularity)
                 4. Set all config for all controller/action 
                 * 
                *****************************************/
                // 1. Use the User Query to get the current user from DB
                //UserEFContainer dbcontext = new UserEFContainer();
                //var CurrUser = _query.GetOne(user => user.Username == HttpContext.Current.User.Identity.Name);
                 
                // Note: To verify this actually works, try changing role other than ADMIN and you should not get access to any UserController action
                //Also to add, list of constant that represent each CODE of role and others
                List<ApplicationRole> adminApplicationRole = new ApplicationRoleReferenceDataSource().Where(x => x.Code == "ADMIN").ToList();
                 User myUser = new UserBuilder(1).WithApplicationRoles(adminApplicationRole);
                                                //.WithUserName("UITPC\\underscoreit7");
                
               
                //2. Check whether the current user is authenticated
                // Let Fluent Security know how to get the authentication status of the current user
                configuration.GetAuthenticationStatusFrom(() => HttpContext.Current.User.Identity.IsAuthenticated);
                
                //3. Get its roles , NOTE: it won t be application role but the list of application permission 
                //var LstRoles = dbcontext.Users.Single(x => x.Id == 1).Roles.ToList().Select(x => x.RoleName);
                //configuration.GetRolesFrom(() => dbcontext.Users.Single(x => x.Loginname == HttpContext.Current.User.Identity.Name).Roles.ToList().Select(x => x.RoleName));
               configuration.GetRolesFrom(() => myUser.ApplicationRoles.Select(x => x.Code)) ;

                // This is where you set up the policies you want Fluent Security to enforce on your controllers and actions
                configuration.ForAllControllers().DenyAnonymousAccess();

                // ADMIN SECTION *************************************************
                configuration.For<UserController>().RequireRole("ADMIN");
                
                // DEAL SECTION *************************************************
                //configuration.For<DealController>(x => x.Index()).RequireRole("DealLeader");

                // ASSET SECTION *************************************************
                //configuration.For<AssetController>(x => x.Index()).RequireRole("DealLeader");

                // XXX SECTION *************************************************
                //configuration.For<XXXController>(x => x.Index()).RequireRole("XXX");


                configuration.ResolveServicesUsing(type =>
                {
                    var objects = new List<object>();

                    if (type == typeof(IPolicyViolationHandler))
                        objects.Add(new RequireRolePolicyViolationHandler());

                    return objects;
                });
            });
            
            GlobalFilters.Filters.Add(new HandleSecurityAttribute(), 0);
        }

        //public FluentSecurityBootstrapper(IUserQuery query )
        //{
        //    _query = query;
        //}
    }

    //Managing the Policy Violation in iDeal
    public class RequireRolePolicyViolationHandler : IPolicyViolationHandler
    {
        public ActionResult Handle(PolicyViolationException exception)
        {
            //Log the violation, send mail etc. etc.
            var rvd = new RouteValueDictionary(new
            {
                area = "",
                controller = "Error",
                action = "HttpForbidden",
            });
            return new RedirectToRouteResult(rvd);
            
        }
    }

}