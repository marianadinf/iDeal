using System;
using System.Collections.Generic;
using System.Linq;

namespace UIT.iDeal.Domain.Model.Base
{
    public static class UniqueCollectionExtensions
    {
        public static Boolean AddUnique<T>(this ICollection<T> collection, T additionalItem) where T : class
        {
            if (collection == null || additionalItem == null || collection.Contains(additionalItem)) return false;
            collection.Add(additionalItem);
            return true;
        }

        public static Boolean AddRange<T>(this ICollection<T> collection, IEnumerable<T> additionalItems)
            where T : class
        {
            return additionalItems.All(collection.AddUnique);

        }
    }
}