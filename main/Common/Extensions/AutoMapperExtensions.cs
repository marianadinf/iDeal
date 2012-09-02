using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;

namespace UIT.iDeal.Common.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression IgnoreAllNonExisting(this IMappingExpression expression,
                                                              Type sourceType, Type destinationType)
        {
            ApplyIgnoreOnNonExisting(property => expression.ForMember(property, opt => opt.Ignore()), sourceType,
                                     destinationType);

            return expression;
        }

        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {

            ApplyIgnoreOnNonExisting(property => expression.ForMember(property, opt => opt.Ignore()),
                                     typeof (TSource),
                                     typeof (TDestination));
            return expression;
        }


        public static IMappingExpression<TSource, TDestination> MapDestinationPropertyFromContainer
            <TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression,
                                    Expression<Func<TDestination, Object>> destinationPropertySelector)
            where TDestination : class
        {

            var propertyType = destinationPropertySelector.GetPropertyFromLambda().PropertyType;
            expression.ForMember(destinationPropertySelector, o => o.UseValue(ConstructServiceUsing(propertyType)));
            return expression;
        }

        public static Func<Type, Object> ConstructServiceUsing { get; set; }



        static void ApplyIgnoreOnNonExisting(Action<string> ignorePropertyAction, Type sourceType, Type destinationType)
        {
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType)
                && x.DestinationType.Equals(destinationType));
            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                ignorePropertyAction(property);

            }

        }
    }
}