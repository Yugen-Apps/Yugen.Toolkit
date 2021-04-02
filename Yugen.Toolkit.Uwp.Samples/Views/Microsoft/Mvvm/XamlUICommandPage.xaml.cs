using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Microsoft.Mvvm;

namespace Yugen.Toolkit.Uwp.Samples.Views.Microsoft.Mvvm
{
    public sealed partial class XamlUICommandPage : Page
    {
        public XamlUICommandPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<XamlUICommandViewModel>();
        }

        private XamlUICommandViewModel ViewModel => (XamlUICommandViewModel)DataContext;
    }
}