using Microsoft.Toolkit.Mvvm.Input;
using System.Linq;
using System.Windows.Input;
using Yugen.Toolkit.Uwp.Collections;
using Yugen.Toolkit.Uwp.Extensions;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    public class GroupedCollectionViewModel
    {
        public GroupedCollectionViewModel()
        {
            GroupCollection();

            ButtonCommand = new RelayCommand(ButtonCommandBehavior);
        }

        public ObservableGroupedCollection<string, Person> GroupedCollection { get; set; }

        public ICommand ButtonCommand { get; }

        private static string GetGroupName(Person person) => person.Name.First().ToString().ToUpper();

        private void GroupCollection()
        {
            //var grouped = contacts
            //                .GroupBy(item => item.Name.First().ToString().ToUpper())
            //                    .OrderBy(g => g.Key);

            var grouped = Data.ContactList.GroupByFirstLetterAscending(item => item.Name);
            GroupedCollection = new ObservableGroupedCollection<string, Person>(grouped);
        }

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