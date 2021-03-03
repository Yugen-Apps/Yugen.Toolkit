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
        /// <param name="items"></param>
        public static void Refresh<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }

            collection.Clear();

            collection.AddRange(items);
        }

        /// <summary>
        /// Adds the elements of the given items to add to the end of the ObservableCollection.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <param name="items">
        /// The items to add to the collection.
        /// </param>
        /// <typeparam name="T">
        /// The type of object within the collections.
        /// </typeparam>
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }

            foreach (T item in items)
            {
                collection.Add(item);
            }
        }

        /// <summary>
        /// Sorts the elements of a sequence in order according to a key.
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
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> collection, Func<TSource, TKey> keySelector)
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
        /// Sorts the elements of a sequence in order according to the default comparer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable<T> => 
            Sort(collection, Comparer<T>.Default);

        /// <summary>
        /// Sorts the elements of a sequence in order according to a comparer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="comparer"></param>
        public static void Sort<T>(this ObservableCollection<T> collection, IComparer<T> comparer)
        {
            if (collection == null || collection.Count <= 1)
            {
                return;
            }

            var newIndex = 0;
            foreach (var oldIndex in collection.OrderBy(x => x, comparer).Select(collection.IndexOf))
            {
                if (oldIndex != newIndex)
                {
                    collection.Move(oldIndex, newIndex);
                }

                newIndex++;
            }
        }

        /// <summary>
        /// Inserts an element into the ordered collection at the proper index.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        /// <param name="keySelector"></param>
        public static void AddSorted<TSource, TKey>(this ObservableCollection<TSource> collection, TSource item, 
            Func<TSource, TKey> keySelector) where TKey : IComparable<TKey>
        {
            var i = collection.Select((Value, Index) => new { Value, Index })
                                .FirstOrDefault(x => keySelector(x.Value)
                                    .CompareTo(keySelector(item)) > 0);

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
        /// Inserts an element into the ordered collection at the proper index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        public static void AddSorted<T>(this ObservableCollection<T> collection, T item) where T : IComparable<T> =>
            AddSorted(collection, item, Comparer<T>.Default);

        /// <summary>
        /// Inserts an element into the ordered collection at the proper index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="item"></param>
        /// <param name="comparer"></param>
        public static void AddSorted<T>(this ObservableCollection<T> collection, T item, IComparer<T> comparer)
        {
            int i = 0;
            while (i < collection.Count && comparer.Compare(collection[i], item) < 0)
            {
                i++;
            }

            collection.Insert(i, item);
        }
    }
}