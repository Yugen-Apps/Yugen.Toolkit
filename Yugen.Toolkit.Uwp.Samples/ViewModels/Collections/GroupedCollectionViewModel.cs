using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Yugen.Toolkit.Uwp.Collections;
using Yugen.Toolkit.Uwp.Extensions;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    public class GroupedCollectionViewModel
    {
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

            var grouped = Data.ContactList.GroupByFirstLetterAscending(item => item.Name);
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