using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Reflection;

namespace UIT.iDeal.Common.Extensions
{
    public static class EnumExtensions
    {
        public static string ToDescription(this Enum enumeration)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = enumeration.GetType().GetField(enumeration.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return enumeration.ToString();
        }

        public static String ToIntAsString(this Enum enumeration)
        {
            if (enumeration == null)
            {
                return String.Empty;
            }

            return Convert.ToInt32(enumeration).ToString();
        }

        public static T ToEnum<T>(this string enumName)
        {
            Type enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            // Remove spaces from Enum
            var enumNameWithoutSpace = enumName.Replace(" ", string.Empty);

            // return null if enumName is not in enum values.
            return (T)Enum.Parse(enumType, enumNameWithoutSpace);
        }

        public static string ToEnumDescriptionFromEnumName<T>(this string enumName)
        {
            Type enumType = typeof(T);

            // Can't use type constraints on value types, so have to do check like this
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            // return null if enumName is not in enum values.
            try
            {
                var value = (Enum)Enum.Parse(enumType, enumName);
                return value.ToDescription();
            }
            catch (ArgumentException exception)
            {
                // Enum name is not in Enum value.
                return null;
            }
        }

        public static IEnumerable<T> GetEnumItems<T>()
        {
            return Enum.GetValues(typeof(T)) as T[];
        }

        public static IEnumerable<KeyValuePair<int,string>> GetAllIdAndDescriptionValuePairs<T>()
        {
            return
                GetEnumItems<T>()
                    .OfType<Enum>()
                    .Select(x => new {Id = Convert.ToInt32(x), Description = x.ToDescription()})
                    .ToDictionary(x => x.Id, x => x.Description);
        }
    }
}