using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Yugen.Toolkit.Standard.Helpers;

namespace Yugen.Toolkit.Uwp.Collections
{
    /// <summary>
    /// An interface for a grouped collection of items.
    /// It allows us to use x:Bind with <see cref="ObservableGroup{TKey, TValue}"/> and <see cref="ReadOnlyObservableGroup{TKey, TValue}"/> by providing
    /// a non-generic type that we can declare using x:DataType.
    /// </summary>
    public interface IReadOnlyObservableGroup : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the key for the current collection, as an <see cref="object"/>.
        /// It is immutable.
        /// </summary>
        object Key { get; }

        /// <summary>
        /// Gets the number of items currently in the grouped collection.
        /// </summary>
        int Count { get; }
    }

    /// <summary>
    /// An observable group.
    /// It associates a <see cref="Key"/> to an <see cref="ObservableCollection{T}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the group key.</typeparam>
    /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
    public sealed class ObservableGroup<TKey, TValue> : ObservableCollection<TValue>, IGrouping<TKey, TValue>, IReadOnlyObservableGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableGroup{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="key">The key for the group.</param>
        public ObservableGroup(TKey key)
        {
            Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableGroup{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="grouping">The grouping to fill the group.</param>
        public ObservableGroup(IGrouping<TKey, TValue> grouping)
            : base(grouping)
        {
            Key = grouping.Key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableGroup{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="key">The key for the group.</param>
        /// <param name="collection">The initial collection of data to add to the group.</param>
        public ObservableGroup(TKey key, IEnumerable<TValue> collection)
            : base(collection)
        {
            Key = key;
        }

        /// <summary>
        /// Gets the key of the group.
        /// </summary>
        public TKey Key { get; }

        /// <inheritdoc/>
        object IReadOnlyObservableGroup.Key => Key;
    }

    /// <summary>
    /// An observable list of observable groups.
    /// </summary>
    /// <typeparam name="TKey">The type of the group key.</typeparam>
    /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
    public sealed class ObservableGroupedCollection<TKey, TValue> : ObservableCollection<ObservableGroup<TKey, TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableGroupedCollection{TKey, TValue}"/> class.
        /// </summary>
        public ObservableGroupedCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableGroupedCollection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="collection">The initial data to add in the grouped collection.</param>
        public ObservableGroupedCollection(IEnumerable<IGrouping<TKey, TValue>> collection)
            : base(collection.Select(c => new ObservableGroup<TKey, TValue>(c)))
        {
        }
    }

    /// <summary>
    /// The extensions methods to simplify the usage of <see cref="ObservableGroupedCollection{TKey, TValue}"/>.
    /// </summary>
    public static class ObservableGroupedCollectionExtensions
    {
        /// <summary>
        /// Return the first group with <paramref name="key"/> key.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group to query.</param>
        /// <returns>The first group matching <paramref name="key"/>.</returns>
        /// <exception cref="InvalidOperationException">The target group does not exist.</exception>
        public static ObservableGroup<TKey, TValue> First<TKey, TValue>(this ObservableGroupedCollection<TKey, TValue> source, TKey key)
            => source.First(group => GroupKeyPredicate(group, key));

        /// <summary>
        /// Return the first group with <paramref name="key"/> key or null if not found.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group to query.</param>
        /// <returns>The first group matching <paramref name="key"/> or null.</returns>
        public static ObservableGroup<TKey, TValue> FirstOrDefault<TKey, TValue>(this ObservableGroupedCollection<TKey, TValue> source, TKey key)
            => source.FirstOrDefault(group => GroupKeyPredicate(group, key));

        /// <summary>
        /// Return the element at position <paramref name="index"/> from the first group with <paramref name="key"/> key.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group to query.</param>
        /// <param name="index">The index of the item from the targeted group.</param>
        /// <returns>The element.</returns>
        /// <exception cref="InvalidOperationException">The target group does not exist.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or <paramref name="index"/> is greater than the group elements' count.</exception>
        public static TValue ElementAt<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            int index)
            => source.First(key)[index];

        /// <summary>
        /// Return the element at position <paramref name="index"/> from the first group with <paramref name="key"/> key.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group to query.</param>
        /// <param name="index">The index of the item from the targeted group.</param>
        /// <returns>The element or default(TValue) if it does not exist.</returns>
        public static TValue ElementAtOrDefault<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            int index)
        {
            var existingGroup = source.FirstOrDefault(key);
            if (existingGroup is null)
            {
                return default;
            }

            return existingGroup.ElementAtOrDefault(index);
        }

        /// <summary>
        /// Adds a key-value <see cref="ObservableGroup{TKey, TValue}"/> item into a target <see cref="ObservableGroupedCollection{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group where <paramref name="value"/> will be added.</param>
        /// <param name="value">The value to add.</param>
        /// <returns>The added <see cref="ObservableGroup{TKey, TValue}"/>.</returns>
        public static ObservableGroup<TKey, TValue> AddGroup<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            TValue value)
        => AddGroup(source, key, new[] { value });

        /// <summary>
        /// Adds a key-collection <see cref="ObservableGroup{TKey, TValue}"/> item into a target <see cref="ObservableGroupedCollection{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group where <paramref name="collection"/> will be added.</param>
        /// <param name="collection">The collection to add.</param>
        /// <returns>The added <see cref="ObservableGroup{TKey, TValue}"/>.</returns>
        public static ObservableGroup<TKey, TValue> AddGroup<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            params TValue[] collection)
            => source.AddGroup(key, (IEnumerable<TValue>)collection);

        /// <summary>
        /// Adds a key-collection <see cref="ObservableGroup{TKey, TValue}"/> item into a target <see cref="ObservableGroupedCollection{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group where <paramref name="collection"/> will be added.</param>
        /// <param name="collection">The collection to add.</param>
        /// <returns>The added <see cref="ObservableGroup{TKey, TValue}"/>.</returns>
        public static ObservableGroup<TKey, TValue> AddGroup<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            IEnumerable<TValue> collection)
        {
            var group = new ObservableGroup<TKey, TValue>(key, collection);
            source.Add(group);

            return group;
        }

        /// <summary>
        /// Add <paramref name="item"/> into the first group with <paramref name="key"/> key.
        /// If the group does not exist, it will be added.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group where the <paramref name="item"/> should be added.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>The instance of the <see cref="ObservableGroup{TKey, TValue}"/> which will receive the value. It will either be an existing group or a new group.</returns>
        public static ObservableGroup<TKey, TValue> AddItem<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            TValue item)
        {
            var existingGroup = source.FirstOrDefault(key);
            if (existingGroup is null)
            {
                existingGroup = new ObservableGroup<TKey, TValue>(key);
                source.Add(existingGroup);
            }

            existingGroup.Add(item);
            return existingGroup;
        }

        /// <summary>
        /// Insert <paramref name="item"/> into the first group with <paramref name="key"/> key at <paramref name="index"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group where to insert <paramref name="item"/>.</param>
        /// <param name="index">The index where to insert <paramref name="item"/>.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>The instance of the <see cref="ObservableGroup{TKey, TValue}"/> which will receive the value.</returns>
        /// <exception cref="InvalidOperationException">The target group does not exist.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or <paramref name="index"/> is greater than the group elements' count.</exception>
        public static ObservableGroup<TKey, TValue> InsertItem<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            int index,
            TValue item)
        {
            var existingGroup = source.First(key);
            existingGroup.Insert(index, item);
            return existingGroup;
        }

        /// <summary>
        /// Replace the element at <paramref name="index"/> with <paramref name="item"/> in the first group with <paramref name="key"/> key.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group where to replace the item.</param>
        /// <param name="index">The index where to insert <paramref name="item"/>.</param>
        /// <param name="item">The item to add.</param>
        /// <returns>The instance of the <see cref="ObservableGroup{TKey, TValue}"/> which will receive the value.</returns>
        /// <exception cref="InvalidOperationException">The target group does not exist.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or <paramref name="index"/> is greater than the group elements' count.</exception>
        public static ObservableGroup<TKey, TValue> SetItem<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            int index,
            TValue item)
        {
            var existingGroup = source.First(key);
            existingGroup[index] = item;
            return existingGroup;
        }

        /// <summary>
        /// Remove the first occurrence of the group with <paramref name="key"/> from the <paramref name="source"/> grouped collection.
        /// It will not do anything if the group does not exist.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group to remove.</param>
        public static void RemoveGroup<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key)
        {
            var index = 0;
            foreach (var group in source)
            {
                if (GroupKeyPredicate(group, key))
                {
                    source.RemoveAt(index);
                    return;
                }

                index++;
            }
        }

        /// <summary>
        /// Remove the first <paramref name="item"/> from the first group with <paramref name="key"/> from the <paramref name="source"/> grouped collection.
        /// It will not do anything if the group or the item does not exist.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group where the <paramref name="item"/> should be removed.</param>
        /// <param name="item">The item to remove.</param>
        /// <param name="removeGroupIfEmpty">If true (default value), the group will be removed once it becomes empty.</param>
        public static void RemoveItem<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            TValue item,
            bool removeGroupIfEmpty = true)
        {
            var index = 0;
            foreach (var group in source)
            {
                if (GroupKeyPredicate(group, key))
                {
                    group.Remove(item);

                    if (removeGroupIfEmpty && group.Count == 0)
                    {
                        source.RemoveAt(index);
                    }

                    return;
                }

                index++;
            }
        }

        /// <summary>
        /// Remove the item at <paramref name="index"/> from the first group with <paramref name="key"/> from the <paramref name="source"/> grouped collection.
        /// It will not do anything if the group or the item does not exist.
        /// </summary>
        /// <typeparam name="TKey">The type of the group key.</typeparam>
        /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
        /// <param name="source">The source <see cref="ObservableGroupedCollection{TKey, TValue}"/> instance.</param>
        /// <param name="key">The key of the group where the item at <paramref name="index"/> should be removed.</param>
        /// <param name="index">The index of the item to remove in the group.</param>
        /// <param name="removeGroupIfEmpty">If true (default value), the group will be removed once it becomes empty.</param>
        public static void RemoveItemAt<TKey, TValue>(
            this ObservableGroupedCollection<TKey, TValue> source,
            TKey key,
            int index,
            bool removeGroupIfEmpty = true)
        {
            var groupIndex = 0;
            foreach (var group in source)
            {
                if (GroupKeyPredicate(group, key))
                {
                    group.RemoveAt(index);

                    if (removeGroupIfEmpty && group.Count == 0)
                    {
                        source.RemoveAt(groupIndex);
                    }

                    return;
                }

                groupIndex++;
            }
        }

        private static bool GroupKeyPredicate<TKey, TValue>(ObservableGroup<TKey, TValue> group, TKey expectedKey)
            => EqualityComparer<TKey>.Default.Equals(group.Key, expectedKey);
    }

    /// <summary>
    /// A read-only observable group. It associates a <see cref="Key"/> to a <see cref="ReadOnlyObservableCollection{T}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the group key.</typeparam>
    /// <typeparam name="TValue">The type of the items in the collection.</typeparam>
    public sealed class ReadOnlyObservableGroup<TKey, TValue> : ReadOnlyObservableCollection<TValue>, IGrouping<TKey, TValue>, IReadOnlyObservableGroup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyObservableGroup{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="key">The key of the group.</param>
        /// <param name="collection">The collection of items to add in the group.</param>
        public ReadOnlyObservableGroup(TKey key, ObservableCollection<TValue> collection)
            : base(collection)
        {
            Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyObservableGroup{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="group">The <see cref="ObservableGroup{TKey, TValue}"/> to wrap.</param>
        public ReadOnlyObservableGroup(ObservableGroup<TKey, TValue> group)
            : base(group)
        {
            Key = group.Key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyObservableGroup{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="key">The key of the group.</param>
        /// <param name="collection">The collection of items to add in the group.</param>
        public ReadOnlyObservableGroup(TKey key, IEnumerable<TValue> collection)
            : base(new ObservableCollection<TValue>(collection))
        {
            Key = key;
        }

        /// <inheritdoc/>
        public TKey Key { get; }

        /// <inheritdoc/>
        object IReadOnlyObservableGroup.Key => Key;
    }

    /// <summary>
    /// A read-only list of groups.
    /// </summary>
    /// <typeparam name="TKey">The type of the group key.</typeparam>
    /// <typeparam name = "TValue" > The type of the items in the collection.</typeparam>
    public sealed class ReadOnlyObservableGroupedCollection<TKey, TValue> : ReadOnlyObservableCollection<ReadOnlyObservableGroup<TKey, TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyObservableGroupedCollection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="collection">The source collection to wrap.</param>
        public ReadOnlyObservableGroupedCollection(ObservableGroupedCollection<TKey, TValue> collection)
            : this(collection.Select(g => new ReadOnlyObservableGroup<TKey, TValue>(g)))
        {
            ((INotifyCollectionChanged)collection).CollectionChanged += this.OnSourceCollectionChanged;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyObservableGroupedCollection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="collection">The initial data to add in the grouped collection.</param>
        public ReadOnlyObservableGroupedCollection(IEnumerable<ReadOnlyObservableGroup<TKey, TValue>> collection)
            : base(new ObservableCollection<ReadOnlyObservableGroup<TKey, TValue>>(collection))
        {
            Source = new CollectionViewSource
            {
                IsSourceGrouped = true,
                Source = collection
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyObservableGroupedCollection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="collection">The initial data to add in the grouped collection.</param>
        public ReadOnlyObservableGroupedCollection(IEnumerable<IGrouping<TKey, TValue>> collection)
            : this(collection.Select(g => new ReadOnlyObservableGroup<TKey, TValue>(g.Key, g)))
        {
        }

        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Even if the NotifyCollectionChangedEventArgs allows multiple items, the actual implementation is only
            // reporting the changes one by one. We consider only this case for now.
            if (e.OldItems?.Count > 1 || e.NewItems?.Count > 1)
            {
                Debug.Fail("OldItems and NewItems should contain at most 1 item");
                throw new NotSupportedException();
            }

            var newItem = e.NewItems?.Cast<ObservableGroup<TKey, TValue>>()?.FirstOrDefault();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Items.Insert(e.NewStartingIndex, new ReadOnlyObservableGroup<TKey, TValue>(newItem));
                    break;
                case NotifyCollectionChangedAction.Move:
                    // Our inner Items list is our own ObservableCollection<ReadOnlyObservableGroup<TKey, TValue>> so we can safely cast Items to its concrete type here.
                    ((ObservableCollection<ReadOnlyObservableGroup<TKey, TValue>>)Items).Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Items.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Items[e.OldStartingIndex] = new ReadOnlyObservableGroup<TKey, TValue>(newItem);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Items.Clear();
                    break;
                default:
                    Debug.Fail("unsupported value");
                    break;
            }
        }

        public CollectionViewSource Source { get; set; }

        // Zoom In
        public ICollectionView SourceView => Source.View;

        // Zoom Out
        public IObservableVector<object> SourceViewCollectionGroups => Source.View.CollectionGroups;
    }
}

// https://gist.github.com/Sergio0694/ba235dc4ef2c4fec9c4a8091522e475b
//public class ObservableGroupedCollection
//{
//    public ObservableGroupedCollection()
//    {
//        Source = new CollectionViewSource
//        {
//            IsSourceGrouped = true,
//            Source = Items
//        };
//    }

//    public ObservableCollection<GroupInfoList> Items { get; set; } = new ObservableCollection<GroupInfoList>();

//    public CollectionViewSource Source { get; set; }

//    // Zoom In
//    public ICollectionView SourceView => Source.View;

//    // Zoom Out
//    public IObservableVector<object> SourceViewCollectionGroups => Source.View.CollectionGroups;
//}