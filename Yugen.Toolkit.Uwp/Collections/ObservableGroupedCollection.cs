using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;

namespace Yugen.Toolkit.Uwp.Collections
{
    /// <summary>
    /// A read-only list of groups.
    /// </summary>
    /// <typeparam name="TKey">The type of the group key.</typeparam>
    /// <typeparam name = "TValue" > The type of the items in the collection.</typeparam>
    public sealed class ObservableGroupedCollection<TKey, TValue> : ObservableCollection<ObservableGroup<TKey, TValue>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyObservableGroupedCollection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="collection">The initial data to add in the grouped collection.</param>
        public ObservableGroupedCollection(IEnumerable<ObservableGroup<TKey, TValue>> collection)
            : base(new ObservableCollection<ObservableGroup<TKey, TValue>>(collection))
        {
            this.CollectionChanged += this.OnSourceCollectionChanged;

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
        public ObservableGroupedCollection(IEnumerable<IGrouping<TKey, TValue>> collection)
            : this(collection.Select(g => new ObservableGroup<TKey, TValue>(g.Key, g)))
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
                    //Items.Insert(e.NewStartingIndex, new ObservableGroup<TKey, TValue>(newItem));
                    break;
                case NotifyCollectionChangedAction.Move:
                    // Our inner Items list is our own ObservableCollection<ReadOnlyObservableGroup<TKey, TValue>> so we can safely cast Items to its concrete type here.
                    ((ObservableCollection<ObservableGroup<TKey, TValue>>)Items).Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Items.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Items[e.OldStartingIndex] = new ObservableGroup<TKey, TValue>(newItem);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Items.Clear();
                    break;
                default:
                    Debug.Fail("unsupported value");
                    break;
            }

            Refresh();
        }

        public void Replace(ObservableGroup<TKey, TValue> targetGroup, TValue newItem)
        {
            Items.Remove(targetGroup);
            targetGroup.Add(newItem);
            Items.Add(targetGroup);
            Refresh();
        }

        public void Refresh()
        {
            Source = new CollectionViewSource
            {
                IsSourceGrouped = true,
                Source = Items
            };
        }

        public CollectionViewSource Source { get; set; }

        // Zoom In
        public ICollectionView SourceView => Source.View;

        // Zoom Out
        public IObservableVector<object> SourceViewCollectionGroups => Source.View.CollectionGroups;
    }
}