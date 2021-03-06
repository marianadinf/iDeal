using System;
using AutoMapper;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.Common.ObjectMapping.Converters
{
    public class EnumTypeConverter : TypeConverter<Enum, string>
    {
        protected override string ConvertCore(Enum source)
        {
            return source.ToDescription();
        }
    }
}