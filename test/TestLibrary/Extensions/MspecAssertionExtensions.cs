using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.ComponentModel;
using Machine.Specifications;
using Machine.Specifications.Annotations;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.TestLibrary.Extensions
{
    /// <summary>
    /// Adds overloads for standard mspec extension methods to allow passing 
    /// in an additional string to be output as part of the error message
    /// </summary>
    public static class MspecAssertionExtensions
    {
        [AssertionMethod]
        public static void ShouldBeTrue([AssertionCondition(AssertionConditionType.IS_TRUE)] this bool condition, string message)
        {
            if (!condition)
            {
                string returnMessage = FormatMessage("Should be [true] but is [false]", message);
                throw new SpecificationException(returnMessage);
            }
        }

        [AssertionMethod]
        public static void ShouldBeFalse([AssertionCondition(AssertionConditionType.IS_FALSE)] this bool condition, string message)
        {
            if (condition)
            {
                string returnMessage = FormatMessage("Should be [false] but is [true]", message);
                throw new SpecificationException(returnMessage);
            }
        }

        [AssertionMethod]
        public static void ShouldBeNull([AssertionCondition(AssertionConditionType.IS_NULL)] this object anObject, string message)
        {
            if (anObject != null)
            {
                string normalMessage = string.Format("Should be [null] but is {0}", anObject.ToString());
                string returnMessage = FormatMessage(normalMessage, message);
                throw new SpecificationException(returnMessage);
            }
        }

        public static void ShouldBeEmpty(this IEnumerable<string> collection, string message)
        {
            if (collection.Any())
            {
                string returnMessage = message + "\r\n" + string.Join("\r\n", collection.ToArray());
                throw new SpecificationException(returnMessage);
            }
        }

        [AssertionMethod]
        public static void ShouldNotBeNull([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] this object anObject, string message)
        {
            if (anObject == null)
            {
                string normalMessage = "Should be [not null] but is [null]";
                string returnMessage = FormatMessage(normalMessage, message);
                throw new SpecificationException(returnMessage);
            }
        }

        public static T TypeShouldBe<T>(this object actual)
        {
            actual.ShouldBeOfType(typeof(T));
            return (T)actual;
        }

        [AssertionMethod]
        public static T ShouldHaveProperty<T, TProperty>(this T obj, Expression<Func<T, TProperty>> propertySelector)
            where T : class
        {
            obj.GetPropertyFromLambda(propertySelector);

            return obj;
        }
       

        [AssertionMethod]
        public static void DescriptionShouldEqual(this string enumDescription, Enum enumValue)
        {
            if (enumDescription == null)
            {
                enumValue.ShouldBeNull();
            }
            else
            {
                enumDescription.ShouldEqual(enumValue.ToDescription());   
            }
            
        }

        [AssertionMethod]
        public static void ShouldBeEquivalentToModel<TActual,TExpected>(this TActual actual, TExpected expected)
            where TActual:class
            where TExpected:class
        {
            var actualModelType = typeof (TActual);
            var expectedModelType = typeof (TExpected);

            if (actual == null) throw new SpecificationException(String.Format("{0} has no reference set",actualModelType.Name));

            if (expectedModelType == null) throw new SpecificationException(String.Format("{0} has no reference set",expectedModelType.Name));

            expectedModelType
                .GetProperties()
                .Where(p => p.CanWrite && p.Name != "Id" && actualModelType.GetProperty(p.Name) != null)
                .Select(p => new
                                 {
                                     ExpectedValue = p.GetValue(expected, null),
                                     ActualValue = actualModelType.GetProperty(p.Name).GetValue(actual, null)
                                 })
                .Where(x => x.ExpectedValue != null && x.ActualValue != null)
                .Each(x => x.ActualValue.ShouldEqual(x.ExpectedValue));

        }

        [AssertionMethod]
        public static T ShouldHavePropertyDisplayedAs<T,TProperty>(this T obj, Expression<Func<T, TProperty>> propertySelector, String displayName)
            where T : class
        {
            var property = obj.GetPropertyFromLambda(propertySelector);
            
            if (property != null)
            {
                var displayAttribute =
                    property
                        .GetCustomAttributes(typeof (DisplayNameAttribute), false)
                        .OfType<DisplayNameAttribute>()
                        .FirstOrDefault();

                if (displayAttribute == null)
                    throw new SpecificationException(string.Format("Display attribute is not decorating property {0}",
                                                                   property.Name));

                displayAttribute.DisplayName.ShouldEqual(displayName);
            }

            return obj;
        }

        private static string FormatMessage(string normalMessage, string customMessage)
        {
            var sb = new StringBuilder();
            sb.AppendLine(normalMessage);
            sb.AppendLine(customMessage);
            return sb.ToString();
        }

    }
}