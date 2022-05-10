using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels;

namespace Yugen.Toolkit.Uwp.Samples.Views
{
    /// <summary>
    /// HomePage
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<HomeViewModel>();
        }

        private HomeViewModel ViewModel => (HomeViewModel)DataContext;
    }
}