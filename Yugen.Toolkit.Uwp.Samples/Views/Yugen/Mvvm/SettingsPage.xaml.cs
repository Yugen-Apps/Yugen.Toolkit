using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Yugen.Mvvm
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            DataContext = AppContainer.Services.GetService<SettingsViewModel>();
        }

        private SettingsViewModel ViewModel => (SettingsViewModel)DataContext;
    }
}
