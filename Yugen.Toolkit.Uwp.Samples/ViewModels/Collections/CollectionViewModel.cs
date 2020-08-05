using System.Collections.ObjectModel;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

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