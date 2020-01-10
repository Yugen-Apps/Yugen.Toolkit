using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NotificationBannerPage : Page
    {
        public NotificationBannerPage()
        {
            this.InitializeComponent();
        }

        private void SuccessButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MyBanner.ShowSuccess();
        }

        private void WarningButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MyBanner.ShowWarning();
        }

        private void ErrorButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MyBanner.ShowError();
        }
    }
}
