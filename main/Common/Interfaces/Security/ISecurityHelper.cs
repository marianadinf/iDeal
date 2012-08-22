using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UIT.iDeal.Common.Interfaces.Security
{
    public interface ISecurityHelper
    {
        Func<Type, LambdaExpression, bool> ActionIsAllowedForUser { get; }

        Func<bool> UserIsAuthenticated { get; }

        Func<IEnumerable<object>> UserRoles { get; }

        Func<string> UserName { get; }
    }
}