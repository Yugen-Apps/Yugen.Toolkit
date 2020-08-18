using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    public sealed partial class SampleInAppControlPage : Page
    {
        public SampleInAppControlViewModel SampleControlViewModel { get; set; } = new SampleInAppControlViewModel("bbb");

        public SampleInAppControlPage()
        {
            this.InitializeComponent();

            //MyUserControl.DataContext = new SampleControlViewModel("ccc");
        }
    }
}
