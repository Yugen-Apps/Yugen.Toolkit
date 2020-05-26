using Yugen.Toolkit.Uwp.Collections;
using Yugen.Toolkit.Uwp.Extensions;
using Yugen.Toolkit.Uwp.Samples.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    public class GroupedCollectionViewModel
    {
        public GroupedCollectionViewModel()
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

            //var grouped = contacts
            //                .GroupBy(item => item.Name.First().ToString().ToUpper())
            //                    .OrderBy(g => g.Key);

            var grouped = contacts.GroupByFirstLetterAscending(item => item.Name);
            GroupedCollection = new ObservableGroupedCollection<string, Person>(grouped);
        }

        public ObservableGroupedCollection<string, Person> GroupedCollection { get; }
    }
}