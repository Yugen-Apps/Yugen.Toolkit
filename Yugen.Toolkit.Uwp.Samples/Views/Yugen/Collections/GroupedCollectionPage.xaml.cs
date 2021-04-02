using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Collections;

namespace Yugen.Toolkit.Uwp.Samples.Views.Yugen.Collections
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