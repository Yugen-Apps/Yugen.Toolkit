using Windows.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using Yugen.Toolkit.Uwp.CodeChallenge.ViewModel;

namespace Yugen.Toolkit.Uwp.CodeChallenge.View
{
    public sealed partial class ValuesPage : Page
    {
        public ValuesPage()
        {
            InitializeComponent();

            DataContext = App.Current.Services.GetService<ValuesViewModel>();
        }

        private ValuesViewModel ViewModel => (ValuesViewModel)DataContext;
    }
}
