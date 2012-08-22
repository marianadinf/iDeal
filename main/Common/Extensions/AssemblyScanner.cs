using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UIT.iDeal.Common.Extensions
{
    public static class AssemblyScanner
    {
        public static IEnumerable<Type> GetMatchingTypesFromAssemblyContaining<T>(Func<Type, bool> expression)
        {
            return (typeof(T)).Assembly.GetTypes().Matching(expression);
        }

        
        public static IEnumerable<Type> FromAssemblyContaining<T>()
        {
            return FromAssemblyContaining(typeof(T));
        }

        public static IEnumerable<Type> GetExportedTypesFromAssemblyContaining<T>()
        {
            return typeof(T).Assembly.GetExportedTypes();
        }

        public static Assembly GetAssembly(this string assemblyName)
        {
            return
                AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Single(a => a.FullName.Contains(assemblyName));
        }

        public static IEnumerable<Type> GetExportedTypesFromAssemblyNamed(string assemblyName)
        {
            return
                AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Single(a => a.FullName.Contains(assemblyName))
                    .GetExportedTypes();
        }


        public static IEnumerable<Type> FromAssemblyContaining(Type type)
        {
            return type.Assembly.GetTypes();
        }

        public static IEnumerable<Type> FromThisAssembly()
        {
            return Assembly.GetCallingAssembly().GetTypes();
        }

        public static IEnumerable<Type> Matching(this IEnumerable<Type> types, Func<Type, bool> expression)
        {
            return types.Where(expression);
        }
    }
}
