using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentSecurity;
using UIT.iDeal.Common.Interfaces.Security;
using UIT.iDeal.Infrastructure.Security.Extensions;


namespace UIT.iDeal.Infrastructure.Security.Helpers
{
    public class HttpContextSecurityHelper : ISecurityHelper
    {
        
        public Func<Type, LambdaExpression, bool> ActionIsAllowedForUser
        {
            get
            {
                return (controllerType, actionExpression) =>
                    {
                        var configuration = SecurityConfiguration.Current;
                        var controllerName = controllerType.GetFullControllerName();
                        var actionName = MvcExtensions.GetActionName(actionExpression);

                        var policyContainer = configuration.PolicyContainers.GetContainerFor(controllerName, actionName);
                        if (policyContainer != null)
                        {
                            var context = SecurityContext.Current;
                            var results = policyContainer.EnforcePolicies(context);
                            return results.All(x => x.ViolationOccured == false);
                        }
                        return true;
                    };
            }
        }

        public Func<bool> UserIsAuthenticated
        {
            get
            {
                return () =>
                    {
                        var currentUser = Current.User;
                        return currentUser != null;
                    };
            }
        }

        public virtual Func<IEnumerable<object>> UserRoles
        {
            get
            {
                return () =>
                    {
                        var currentUser = Current.User;
                        return currentUser != null ? currentUser.Roles.Cast<object>().ToArray() : null;
                    };
            }
        }

        public virtual Func<string> UserName
        {
            get 
            { 
                return
                    () => HttpContext.Current.User.Identity.Name;
            }
          }

     
    }
}