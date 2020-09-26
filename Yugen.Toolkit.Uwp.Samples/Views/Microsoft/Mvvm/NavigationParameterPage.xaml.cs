using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Mvvm
{
    public sealed partial class NavigationParameterPage : Page
    {
        public NavigationParameterPage()
        {
            this.InitializeComponent();

            DataContext = AppContainer.Services.GetService<NavigationParameterViewModel>();
        }

        public NavigationParameterViewModel ViewModel => (NavigationParameterViewModel)DataContext;
    }
}
