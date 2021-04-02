using Microsoft.Toolkit.Mvvm.Input;
using System.Linq;
using Yugen.Toolkit.Standard.Mvvm;
using Yugen.Toolkit.Uwp.Extensions;
using Yugen.Toolkit.Uwp.Samples.Constants;
using Yugen.Toolkit.Uwp.Samples.Models;
using Microsoft.Toolkit.Collections;
using Windows.UI.Xaml.Data;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Collections
{
    public class GroupedCollectionViewModel : ViewModelBase
    {
        private string _newName;

        public GroupedCollectionViewModel()
        {
            GroupCollection();

            AddCommand = new RelayCommand(AddCommandBehavior);
        }

        public string NewName
        {
            get => _newName;
            set => SetProperty(ref _newName, value);
        }

        public ObservableGroupedCollection<string, Person> GroupedCollection { get; set; }

        public IRelayCommand AddCommand { get; }

        public CollectionViewSource Cvs { get; set; }

        public ICollectionView CvsView => Cvs.View;

        private static string GetGroupName(Person person) => person.Name.First().ToString().ToUpper();

        private void GroupCollection()
        {
            //var grouped = contacts
            //                .GroupBy(item => item.Name.First().ToString().ToUpper())
            //                    .OrderBy(g => g.Key);

            var grouped = DataConstants.ContactList.GroupByFirstLetterAscending(item => item.Name);
            GroupedCollection = new ObservableGroupedCollection<string, Person>(grouped);

            Cvs = new CollectionViewSource
            {
                IsSourceGrouped = true,
                Source = GroupedCollection,
            };
        }

        private void AddCommandBehavior()
        {
            var newContact = new Person
            {
                Name = NewName,
                Surname = "Surname"
            };

            var groupName = GetGroupName(newContact);
            GroupedCollection.AddOrReplaceSorted(groupName, newContact, group => group.Key == groupName, x => x.Name);
        }
    }
}