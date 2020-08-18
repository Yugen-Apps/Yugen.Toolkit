using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GroupedCollectionPage : Page
    {
        public GroupedCollectionPage()
        {
            this.InitializeComponent();
        }

        public GroupedCollectionViewModel ViewModel { get; set; } = new GroupedCollectionViewModel();
    }
}