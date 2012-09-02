using System;
using System.Linq.Expressions;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.Common.Errors
{
    public class BusinessRuleExceptionFor<TEntity> : BusinessRuleException
        where TEntity : class
    {
        public BusinessRuleExceptionFor(Expression<Func<TEntity, object>> propertySelector, string error)
            : base(error)
        {
            PropertyName = propertySelector.GetPropertyFromLambda().Name;
        }

        public BusinessRuleExceptionFor(Expression<Func<TEntity, object>> propertySelector, string errorFormat,params object[] arguments)

            : this(propertySelector,string.Format(errorFormat,arguments))
        {
            
        }
    }
}