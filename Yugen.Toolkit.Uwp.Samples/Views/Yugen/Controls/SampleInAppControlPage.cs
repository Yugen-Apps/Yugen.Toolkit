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

        public SampleInAppControlViewModel SampleControlViewModel { get; set; } = new SampleInAppControlViewModel();

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SampleControlViewModel.Text = "Button_Click";
        }

        private void Button2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            SampleControlViewModel.Text2 = "Button2_Click";
        }
    }
}