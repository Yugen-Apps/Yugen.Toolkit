using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Collections
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StretchedCollectionPage : Page
    {
        public StretchedCollectionPage()
        {
            this.InitializeComponent();
        }

        public CollectionViewModel ViewModel { get; set; } = new CollectionViewModel();
    }
}