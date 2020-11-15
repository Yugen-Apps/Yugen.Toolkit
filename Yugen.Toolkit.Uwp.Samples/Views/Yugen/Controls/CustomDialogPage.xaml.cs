using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    public sealed partial class CustomDialogPage : Page
    {
        public CustomDialogPage()
        {
            InitializeComponent();
        }

        private void Button_Tapped(object _1, TappedRoutedEventArgs _2)
        {
            MyDialog.ShowDialog();
        }
    }
}