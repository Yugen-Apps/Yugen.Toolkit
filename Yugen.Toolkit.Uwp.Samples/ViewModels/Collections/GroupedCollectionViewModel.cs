using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Yugen.Toolkit.Standard.Mvvm.Input;
using Yugen.Toolkit.Uwp.Collections;
using Yugen.Toolkit.Uwp.Extensions;
using Yugen.Toolkit.Uwp.Samples.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    public class GroupedCollectionViewModel
    {
        private readonly List<Person> contacts = new List<Person>
        {
            new Person { Name = "Staff", Surname="Surname" },
            new Person { Name = "Swan", Surname="Surname" },
            new Person { Name = "Orchid", Surname="Surname" },
            new Person { Name = "Flame", Surname="Surname" },
            new Person { Name = "Arrow", Surname="Surname" },
            new Person { Name = "Tempest", Surname="Surname" },
            new Person { Name = "Pearl", Surname="Surname" },
            new Person { Name = "Pearl", Surname="Surname" },
            new Person { Name = "Lamp Post", Surname="Surname" }
        };

        private ICommand _buttonCommand;

        public GroupedCollectionViewModel()
        {
            GroupCollection();
        }

        public ObservableGroupedCollection<string, Person> GroupedCollection { get; set; }

        public ICommand ButtonCommand => _buttonCommand
            ?? (_buttonCommand = new RelayCommand(ButtonCommandBehavior));

        private void GroupCollection()
        {
            //var grouped = contacts
            //                .GroupBy(item => item.Name.First().ToString().ToUpper())
            //                    .OrderBy(g => g.Key);

            var grouped = contacts.GroupByFirstLetterAscending(item => item.Name);
            GroupedCollection = new ObservableGroupedCollection<string, Person>(grouped);
        }

        private static string GetGroupName(Person person) => person.Name.First().ToString().ToUpper();


        private void ButtonCommandBehavior()
        {
            var newContact = new Person
            {
                Name = "zLooking Glass",
                Surname = "Surname"
            };

            var groupName = GetGroupName(newContact);
            var targetGroup = GroupedCollection.FirstOrDefault(group => group.Key == groupName);
            if (targetGroup is null)
            {
                GroupedCollection.Add(new ObservableGroup<string, Person>(groupName, new[] { newContact }));
            }
            else
            {
                GroupedCollection.Replace(targetGroup, newContact);
            }
        }
    }
}