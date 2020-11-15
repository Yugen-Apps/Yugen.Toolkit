using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Yugen.Mvvm
{
    public sealed partial class ObservableSettingsPage : Page
    {
        public ObservableSettingsPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<ObservableSettingsViewModel>();
        }

        private ObservableSettingsViewModel ViewModel => (ObservableSettingsViewModel)DataContext;
    }
}