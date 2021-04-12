using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Sandbox.Csharp;

namespace Yugen.Toolkit.Uwp.Samples.Views.Sandbox.Csharp
{
    public sealed partial class DeferralPage : Page
    {
        public DeferralPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<DeferralViewModel>();
        }

        private DeferralViewModel ViewModel => (DeferralViewModel)DataContext;
    }
}
