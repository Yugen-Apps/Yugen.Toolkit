using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    public sealed partial class StretchedCollectionPage : Page
    {
        public StretchedCollectionPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<CollectionViewModel>();
        }

        private CollectionViewModel ViewModel => (CollectionViewModel)DataContext;
    }
}