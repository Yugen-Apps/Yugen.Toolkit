using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    public sealed partial class SampleInAppControl : UserControl
    {
        public SampleInAppControl()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<SampleInAppControlViewModel>();
        }

        private SampleInAppControlViewModel ViewModel => (SampleInAppControlViewModel)DataContext;
    }
}