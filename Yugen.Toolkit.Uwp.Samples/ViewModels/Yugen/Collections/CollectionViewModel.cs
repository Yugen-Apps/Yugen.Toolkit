using System.Collections.ObjectModel;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Collections
{
    public class CollectionViewModel
    {
        public CollectionViewModel()
        {
            Collection = new ObservableCollection<Person>(DataConstants.ContactList);
        }

        public ObservableCollection<Person> Collection { get; }
    }
}