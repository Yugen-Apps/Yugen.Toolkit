using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Helpers;

namespace Yugen.Toolkit.Uwp.Samples.Views.Helpers
{
    public sealed partial class FindControlPage : Page
    {
        public FindControlPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<FindControlViewModel>();
        }

        private FindControlViewModel ViewModel => (FindControlViewModel)DataContext;
    }
}