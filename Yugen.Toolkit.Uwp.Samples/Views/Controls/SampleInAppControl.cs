using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    public sealed partial class SampleInAppControl : UserControl
    {
        private SampleInAppControlViewModel ViewModel => (SampleInAppControlViewModel)DataContext;

        public SampleInAppControl()
        {
            this.InitializeComponent();

            //DataContextChanged += (s, e) =>
            //{
            //    this.Bindings.Update();
            //};
        }
    }
}
