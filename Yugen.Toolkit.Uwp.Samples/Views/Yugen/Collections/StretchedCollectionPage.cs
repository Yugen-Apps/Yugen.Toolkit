using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Collections;

namespace Yugen.Toolkit.Uwp.Samples.Views.Yugen.Collections
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