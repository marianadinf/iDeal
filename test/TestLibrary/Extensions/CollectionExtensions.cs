using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;

namespace UIT.iDeal.TestLibrary.Extensions
{
    public static class CollectionExtensions
    {
        public static void ShouldBeEquivalentTo<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            CollectionAssert.AreEquivalent(expected, actual);
        }


        public static void ShouldBeEquivalentTo<TActual, TExpected>(this IEnumerable<TActual> actual, IEnumerable<TExpected> expected, Func<dynamic, Object> transformWith)
            where TExpected:class
            where TActual:class 
        {
            expected.Select(transformWith).ShouldBeEquivalentTo(actual.Select(transformWith));
        }

        /// <summary>
        /// Verify that all item in the collection satisfies the specified predicate
        /// </summary>
        public static void ShouldSatisfy<T>(this IEnumerable<T> collection,Func<T,Boolean> predicate)
        {
            Assert.IsTrue(collection.All(predicate));
        }

        public static void ShouldNotSatisfy<T>(this IEnumerable<T> collection,Func<T,Boolean> predicate)
        {
            Assert.IsFalse(collection.All(predicate));
        }

        public static void ShouldBeOrderedBy<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> key)
        {
            var array = collection.ToArray();
            CollectionAssert.AreEqual(array, array.OrderBy(key));
        }

        public static void ShouldBeOrderedByDescending<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> key)
        {
            var array = collection.ToArray();
            CollectionAssert.AreEqual(array, array.OrderByDescending(key));
        }

        public static void ShouldBeEquivalentTo(this SelectList selectList, Dictionary<string, string> itemsToBeCompared)
        {
            selectList.ToDictionary(x => x.Value, x => x.Text).ShouldBeEquivalentTo(itemsToBeCompared);
        }

    }
}
