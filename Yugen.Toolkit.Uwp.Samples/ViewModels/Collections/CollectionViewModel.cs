using System.Collections.ObjectModel;
using Yugen.Toolkit.Uwp.Samples.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    public class CollectionViewModel
    {
        public CollectionViewModel()
        {
            var contacts = new[]
            {
                new Person { Name = "Staff", Surname="Surname" },
                new Person { Name = "Swan", Surname="Surname" },
                new Person { Name = "Orchid", Surname="Surname" },
                new Person { Name = "Flame", Surname="Surname" },
                new Person { Name = "Arrow", Surname="Surname" },
                new Person { Name = "Tempest", Surname="Surname" },
                new Person { Name = "Pearl", Surname="Surname" },
                new Person { Name = "Pearl", Surname="Surname" },
                new Person { Name = "Lamp Post", Surname="Surname" },
                new Person { Name = "Looking Glass", Surname="Surname" },
            };

            Collection = new ObservableCollection<Person>(contacts);
        }

        public ObservableCollection<Person> Collection { get; }
    }
}