using System;
using AutoMapper;
using UIT.iDeal.Domain.Model.Base;

namespace UIT.iDeal.Common.ObjectMapping.Converters
{
    public class EntityToGuidTypeConverter : TypeConverter<Entity, Guid>
    {

        protected override Guid ConvertCore(Entity source)
        {
            return source == null ? Guid.Empty : source.Id;
        }
    }
}