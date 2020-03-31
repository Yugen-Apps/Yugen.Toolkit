using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Standard.Collections;
using Yugen.Toolkit.Standard.Extensions;
using Yugen.Toolkit.Standard.Helpers;
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
        public ObservableCollection<GroupInfoList> GroupedCollection { get; set; } = new ObservableCollection<GroupInfoList>();

        public GroupedCollectionViewModel()
        {
            GroupedCollection.Refresh(GetProductListGroupedByCategoryName());
        }

        public List<GroupInfoList> GetProductListGroupedByCategoryName()
        {
            var list = new List<Graph>()
            {
                new Graph(){Title = "Alpha", Value = 10},
                new Graph(){Title = "Alpha", Value = 20},
                new Graph(){Title = "Bravo", Value = 30},
                new Graph(){Title = "Bravo", Value = 40},
            };
            return GroupProductViewModelCollection(list);
        }

        public List<GroupInfoList> GroupProductViewModelCollection(List<Graph> productList)
        {
            var query = productList.GroupBy(item => item.Title);
            return CollectionHelper.GroupCollectionAscending(query).ToList();
        }
    }
}
