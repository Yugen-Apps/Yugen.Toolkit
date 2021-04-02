using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Microsoft.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Microsoft.Mvvm
{
    public sealed partial class NavigationPage : Page
    {
        public NavigationPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<NavigationViewModel>();
        }

        private NavigationViewModel ViewModel => (NavigationViewModel)DataContext;
    }
}