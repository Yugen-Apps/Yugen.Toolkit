using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Yugen.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Yugen.Controls
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