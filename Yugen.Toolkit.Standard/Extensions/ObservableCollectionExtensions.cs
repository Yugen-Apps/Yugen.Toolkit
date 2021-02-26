using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Yugen.Toolkit.Standard.Extensions
{
    /// <summary>
    /// A collection of <see cref="ObservableCollection{T}" /> extensions.
    /// </summary>
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Refresh COllection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="itemsToAdd"></param>
        public static void Refresh<T>(this ObservableCollection<T> collection, IEnumerable<T> itemsToAdd)
        {
            if (itemsToAdd == null)
            {
                return;
            }

            collection.Clear();

            collection.AddRange(itemsToAdd);
        }

        /// <summary>
        /// Adds the elements of the given items to add to the end of the ObservableCollection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="itemsToAdd">
        /// The items to add to the collection.
        /// </param>
        /// <typeparam name="T">
        /// The type of object within the collections.
        /// </typeparam>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> itemsToAdd)
        {
            if (itemsToAdd == null)
            {
                return;
            }

            foreach (T item in itemsToAdd)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Sorts the items within the collection by the given key selector.
        /// </summary>
        /// <param name="collection">
        /// The collection to sort.
        /// </param>
        /// <param name="keySelector">
        /// The key selector.
        /// </param>
        /// <typeparam name="TSource">
        /// The type of item within the collection.
        /// </typeparam>
        /// <typeparam name="TKey">
        /// The type to order by.
        /// </typeparam>
        public static void SortBy<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector)
        {
            if (collection == null || collection.Count <= 1)
            {
                return;
            }

            var newIndex = 0;
            foreach (var oldIndex in collection.OrderBy(keySelector).Select(collection.IndexOf))
            {
                if (oldIndex != newIndex)
                {
                    collection.Move(oldIndex, newIndex);
                }

                newIndex++;
            }
        }

        /// <summary>
        /// TODO: to test 
        /// Sorts the items within the collection by the given key selector.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection"></param>
        /// <param name="keySelector"></param>
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector)
        {
            List<TSource> sorted = collection.OrderBy(keySelector).ToList();
            Dictionary<TSource, int> indexOf = new Dictionary<TSource, int>();

            for (int i = 0; i < sorted.Count; i++)
            {
                indexOf[sorted[i]] = i;
            }

            int idx = 0;
            while (idx < sorted.Count)
            {
                if (!collection[idx].Equals(sorted[idx]))
                {
                    int newIdx = indexOf[collection[idx]]; // where should current item go?
                    collection.Move(newIdx, idx); // move whatever's there to current location
                    collection.Move(idx + 1, newIdx); // move current item to proper location
                }
                else
                {
                    idx++;
                }
            }
        }

        /// <summary>
        /// Add item in the correct position in sorted list
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        /// <param name="keySelector"></param>
        public static void AddSorted<TSource, TKey>(this ObservableCollection<TSource> collection, TSource item, Func<TSource, TKey> keySelector) where TKey : IComparable<TKey>
        {
            var i = collection.Select((Value, Index) => new { Value, Index }).FirstOrDefault(x => keySelector(x.Value).CompareTo(keySelector(item)) > 0);

            if (i == null)
            {
                collection.Add(item);
            }
            else
            {
                collection.Insert(i.Index, item);
            }
        }

        /// <summary>
        /// Add item in the correct position in sorted list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <param name="comparer"></param>
        public static void AddSorted<T>(this IList<T> list, T item, IComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            int i = 0;
            while (i < list.Count && comparer.Compare(list[i], item) < 0)
            {
                i++;
            }

            list.Insert(i, item);
        }

        /// <summary>
        /// Add item in the correct position in sorted list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void AddSorted<T>(this List<T> list, T item) where T : IComparable<T>
        {
            if (list.Count == 0)
            {
                list.Add(item);
                return;
            }

            if (list[list.Count - 1].CompareTo(item) <= 0)
            {
                list.Add(item);
                return;
            }

            if (list[0].CompareTo(item) >= 0)
            {
                list.Insert(0, item);
                return;
            }

            int index = list.BinarySearch(item);
            if (index < 0)
            {
                index = ~index;
            }

            list.Insert(index, item);
        }
    }
}