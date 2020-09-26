using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Mvvm
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ObservableObjectPage : Page
    {
        public ObservableObjectPage()
        {
            this.InitializeComponent();

            DataContext = AppContainer.Services.GetService<ObservableObjectViewModel>();
        }

        private ObservableObjectViewModel ViewModel => (ObservableObjectViewModel)DataContext;
    }
}
