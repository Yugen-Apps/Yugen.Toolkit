using Microsoft.Toolkit.Mvvm.Input;
using System.Linq;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Collections;
using Yugen.Toolkit.Uwp.Extensions;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    public class GroupedCollectionViewModel : ViewModelBase
    {
        private string _newName;

        public GroupedCollectionViewModel()
        {
            GroupCollection();

            ButtonCommand = new RelayCommand(ButtonCommandBehavior);
        }

        public string NewName
        {
            get => _newName;
            set => SetProperty(ref _newName, value);
        }

        public ObservableGroupedCollection<string, Person> GroupedCollection { get; set; }

        public IRelayCommand ButtonCommand { get; }

        private static string GetGroupName(Person person) => person.Name.First().ToString().ToUpper();

        private void GroupCollection()
        {
            //var grouped = contacts
            //                .GroupBy(item => item.Name.First().ToString().ToUpper())
            //                    .OrderBy(g => g.Key);

            var grouped = DataConstants.ContactList.GroupByFirstLetterAscending(item => item.Name);
            GroupedCollection = new ObservableGroupedCollection<string, Person>(grouped);
        }

        private void ButtonCommandBehavior()
        {
            var newContact = new Person
            {
                Name = NewName,
                Surname = "Surname"
            };

            var groupName = GetGroupName(newContact);
            var targetGroup = GroupedCollection.FirstOrDefault(group => group.Key == groupName);
            if (targetGroup is null)
            {
                var tempList = GroupedCollection.ToDictionary(x => x.Key).Keys.ToList();
                tempList.Add(groupName);
                tempList.Sort();
                GroupedCollection.Insert(tempList.IndexOf(groupName), new ObservableGroup<string, Person>(groupName, new[] { newContact }));

                //GroupedCollection.Add(new ObservableGroup<string, Person>(groupName, new[] { newContact }));
            }
            else
            {
                var tempList = targetGroup.ToList();
                tempList.Add(newContact);
                var query = tempList.OrderBy(x => x.Name);
                tempList = query.ToList();
                targetGroup.Insert(tempList.IndexOf(newContact), newContact);

                //GroupedCollection.Replace(targetGroup, newContact);
            }
        }
    }
}