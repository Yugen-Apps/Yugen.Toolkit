using System.Collections.ObjectModel;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    public class CollectionViewModel
    {
        public CollectionViewModel()
        {
            Collection = new ObservableCollection<Person>(Data.ContactList);
        }

        public ObservableCollection<Person> Collection { get; }
    }
}