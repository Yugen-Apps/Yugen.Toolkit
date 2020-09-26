using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Mvvm
{
    public sealed partial class XamlUICommandPage : Page
    {
        public XamlUICommandPage()
        {
            this.InitializeComponent();

            DataContext = AppContainer.Services.GetService<XamlUICommandViewModel>();
        }

        private XamlUICommandViewModel ViewModel => (XamlUICommandViewModel)DataContext;
    }
}
