using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Snippets.Mvvm
{
    public sealed partial class XamlViewModelPage : Page
    {
        public XamlViewModelPage()
        {
            this.InitializeComponent();

            ViewModel.Text = "Hello World";
        }
    }
}