using CommunityToolkit.Mvvm.Collections;
using System;
using System.Linq;

namespace Yugen.Toolkit.Uwp.Extensions
{
    public static class ObservableGroupExtensions
    {
        /// <summary>
        /// Add item in the correct position in sorted group
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="group"></param>
        /// <param name="item"></param>
        /// <param name="keySelector"></param>
        public static void ReplaceSorted<TKey, TSource>(this ObservableGroup<TKey, TSource> group,
            TSource item, Func<TSource, TKey> keySelector) where TKey : IComparable<TKey>
        {
            var i = group.Select((Value, Index) => new { Value, Index }).FirstOrDefault(x => keySelector(x.Value).CompareTo(keySelector(item)) > 0);

            if (i == null)
            {
                group.Add(item);
            }
            else
            {
                group.Insert(i.Index, item);
            }
        }
    }
}