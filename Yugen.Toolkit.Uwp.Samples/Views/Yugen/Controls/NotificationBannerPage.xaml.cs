using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Yugen.Toolkit.Uwp.Samples.Views.Controls
{
    public sealed partial class NotificationBannerPage : Page
    {
        public NotificationBannerPage()
        {
            InitializeComponent();
        }

        private void SuccessButton_Tapped(object _1, TappedRoutedEventArgs _2)
        {
            MyBanner.ShowSuccess();
        }

        private void WarningButton_Tapped(object _1, TappedRoutedEventArgs _2)
        {
            MyBanner.ShowWarning();
        }

        private void ErrorButton_Tapped(object _1, TappedRoutedEventArgs _2)
        {
            MyBanner.ShowError();
        }
    }
}