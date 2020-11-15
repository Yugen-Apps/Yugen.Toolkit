using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    public sealed partial class SampleInAppControlPage : Page
    {
        public SampleInAppControlPage()
        {
            this.InitializeComponent();

            //MyUserControl.DataContext = new SampleControlViewModel("ccc");
        }

        public SampleInAppControlViewModel SampleControlViewModel { get; set; } = new SampleInAppControlViewModel("bbb");
    }
}