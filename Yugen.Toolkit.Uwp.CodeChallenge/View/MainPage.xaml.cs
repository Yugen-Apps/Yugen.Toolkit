using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using Yugen.Toolkit.Uwp.CodeChallenge.ViewModel;

namespace Yugen.Toolkit.Uwp.CodeChallenge.View
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.Current.Services.GetService<MainViewModel>();
        }

        private MainViewModel ViewModel => (MainViewModel)DataContext;
    }
}
