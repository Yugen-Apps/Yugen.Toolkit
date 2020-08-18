using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Samples.ViewModels.Helpers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Helpers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FindControlPage : Page
    {
        public FindControlViewModel ViewModel { get; set; } = new FindControlViewModel();

        public FindControlPage()
        {
            this.InitializeComponent();
        }
    }
}
