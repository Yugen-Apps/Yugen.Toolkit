using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Microsoft.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Microsoft.Mvvm
{
    public sealed partial class ObservableObjectPage : Page
    {
        public ObservableObjectPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<ObservableObjectViewModel>();
        }

        private ObservableObjectViewModel ViewModel => (ObservableObjectViewModel)DataContext;
    }
}