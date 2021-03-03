//        public ObservableGroupedCollection(IEnumerable<ObservableGroup<TKey, TValue>> collection)
//            : base(new ObservableCollection<ObservableGroup<TKey, TValue>>(collection))
//        {
//            Source = new CollectionViewSource
//            {
//                IsSourceGrouped = true,
//                Source = collection
//            };
//        }

//        public CollectionViewSource Source { get; set; }

//        // Zoom In
//        public ICollectionView SourceView => Source.View;

//        // Zoom Out
//        public IObservableVector<object> SourceViewCollectionGroups => Source.View.CollectionGroups;
//    }
//}