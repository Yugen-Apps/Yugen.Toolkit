using System;
using System.Linq;
using Microsoft.Toolkit.Collections;

namespace Yugen.Toolkit.Uwp.Extensions
{
    public static class ObservableGroupedCollectionExtensions
    {
        /// <summary>
        /// Add item and eventually group in the correct position in sorted list
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="collection"></param>
        /// <param name="groupKey"></param>
        /// <param name="item"></param>
        /// <param name="groupKeySelector"></param>
        /// <param name="itemKeySelector"></param>
        public static void AddOrReplaceSorted<TKey, TSource>(this ObservableGroupedCollection<TKey, TSource> collection,
            TKey groupKey, TSource item, Func<ObservableGroup<TKey, TSource>, bool> groupKeySelector,
            Func<TSource, TKey> itemKeySelector) where TKey : IComparable<TKey>
        {
            var targetGroup = collection.FirstOrDefault(groupKeySelector);
            if (targetGroup is null)
            {
                collection.AddSorted(new ObservableGroup<TKey, TSource>(groupKey, new[] { item }));
            }
            else
            {
                targetGroup.ReplaceSorted(item, itemKeySelector);
            }
        }

        /// <summary>
        /// Add group with a new item in the correct position in sorted list
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        public static void AddSorted<TKey, TSource>(this ObservableGroupedCollection<TKey, TSource> collection,
            ObservableGroup<TKey, TSource> item) where TKey : IComparable<TKey>
        {
            var i = collection.Select((Value, Index) => new { Value, Index }).FirstOrDefault(x => x.Value.Key.CompareTo(item.Key) > 0);

            if (i == null)
            {
                collection.Add(item);
            }
            else
            {
                collection.Insert(i.Index, item);
            }
        }
    }
}