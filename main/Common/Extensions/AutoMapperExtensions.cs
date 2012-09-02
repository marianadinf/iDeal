using System;
using System.Linq;
using AutoMapper;

namespace UIT.iDeal.Common.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression IgnoreAllNonExisting(this IMappingExpression expression,
                                                              Type sourceType, Type destinationType)
        {
            var existingMaps =
                Mapper
                    .GetAllTypeMaps()
                    .First(x => x.SourceType == sourceType && x.DestinationType == destinationType);

            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }
            return expression;
        }
    }
}