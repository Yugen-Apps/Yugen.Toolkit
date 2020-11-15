using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    public sealed partial class GroupedCollectionPage : Page
    {
        public GroupedCollectionPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<GroupedCollectionViewModel>();
        }

        private GroupedCollectionViewModel ViewModel => (GroupedCollectionViewModel)DataContext;
    }
}