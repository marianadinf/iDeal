using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            if (additionalItems == null) return false;
            return additionalItems.All(collection.AddUnique);

        }

        public static ReadOnlyCollection<T> ConcateneToReadOnlyCollection<T>(this ReadOnlyCollection<T> readOnlyCollection, IEnumerable<T> items)
           where T : class
        {
            var arrayOfItems = new T[] { };
            readOnlyCollection.CopyTo(arrayOfItems, 0);
            var list = arrayOfItems.ToList();
            list.AddRange(items);
            return new ReadOnlyCollection<T>(list);
        }

        
    }
}