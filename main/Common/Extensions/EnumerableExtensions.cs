using System;
using System.Collections.Generic;
using System.Linq;

namespace UIT.iDeal.Common.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable != null)
            {
                foreach (var item in enumerable)
                {
                    action(item);
                }
            }

            return enumerable;
        }

        public static IEnumerable<T> LazyInitialiseFor<T>(ref IEnumerable<T> internalEnumeration, Func<IEnumerable<T>> listInitialiser)
            where T:class 
        {
            if (internalEnumeration == null || !internalEnumeration.Any())
            {
                internalEnumeration = listInitialiser();
            }

            return internalEnumeration;
        }
    }

    
}