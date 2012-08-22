using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.TestLibrary.Extensions
{
    public static class ExecutionResultAssertionExtensions
    {
        
        public static void ShouldHaveError<TProperty>(this ExecutionResult result,
                                                      Expression<Func<TProperty, object>> property, ErrorType type)
        {
            string propertyName = property.GetMemberInfo().Name;
            var errorsForProperty = result.Errors.Where(x => x.Value.PropertyName == propertyName);
            if (errorsForProperty.Count() == 0)
            {
                Assert.Fail(string.Format("No message for {0}.", propertyName));
            }

        }

        public static void ShouldNotBeSuccessful(this ExecutionResult result)
        {
            Assert.That(result.IsSuccessFull, Is.False);
        }

        public static void ShouldBeSuccessful(this ExecutionResult result)
        {
            //Assert.That(result.IsSuccessful, Is.True);
            if (!result.IsSuccessFull)
            {
                Assert.Fail(string.Format("Not successful. Messages include:\n{0}", result.AllErrorMessages));
            }
        }
    }
}
