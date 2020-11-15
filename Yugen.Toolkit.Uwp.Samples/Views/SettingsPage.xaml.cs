using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen;

namespace Yugen.Toolkit.Uwp.Samples.Views.Yugen
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<SettingsViewModel>();
        }

        private SettingsViewModel ViewModel => (SettingsViewModel)DataContext;
    }
}