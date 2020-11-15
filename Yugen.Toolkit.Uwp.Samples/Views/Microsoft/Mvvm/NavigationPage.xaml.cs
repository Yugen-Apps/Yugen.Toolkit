using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Mvvm
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