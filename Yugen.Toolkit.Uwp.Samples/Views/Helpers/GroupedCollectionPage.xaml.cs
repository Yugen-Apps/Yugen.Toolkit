using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Yugen.Toolkit.Standard.Extensions;
using Yugen.Toolkit.Standard.Helpers;
using Yugen.Toolkit.Uwp.Collections;
using Yugen.Toolkit.Uwp.Samples.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Helpers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupedCollectionPage : Page
    {
        public GroupedCollectionViewModel ViewModel { get; set; } = new GroupedCollectionViewModel(); 

        public GroupedCollectionPage()
        {
            this.InitializeComponent();
        }
    }

    public class GroupedCollectionViewModel
    {
        public GroupedCollectionViewModel()
        {
            var contacts = new[]
            {
                new Person { Name = "Staff" },
                new Person { Name = "Swan" },
                new Person { Name = "Orchid" },
                new Person { Name = "Flame" },
                new Person { Name = "Arrow" },
                new Person { Name = "Tempest" },
                new Person { Name = "Pearl" },
                new Person { Name = "Hydra" },
                new Person { Name = "Lamp Post" },
                new Person { Name = "Looking Glass" },
            };

            var grouped = contacts.GroupBy(GetGroupName).OrderBy(g => g.Key);

            GroupedCollection = new ObservableGroupedCollection<string, Person>(grouped);
            Contacts = new ReadOnlyObservableGroupedCollection<string, Person>(GroupedCollection);
        }

        public ObservableGroupedCollection<string, Person> GroupedCollection { get; }
        public ReadOnlyObservableGroupedCollection<string, Person> Contacts { get; }

        private static string GetGroupName(Person person) => person.Name.First().ToString().ToUpper();
    }
}
