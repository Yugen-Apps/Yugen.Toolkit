using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Mvvm
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