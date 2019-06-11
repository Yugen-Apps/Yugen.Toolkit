using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UwpCommunity.Uwp.Controls
{
    public sealed partial class NotificationBannerUserControl : UserControl
    {
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(NotificationBannerUserControl), new PropertyMetadata(string.Empty));

        public Style UcStyle
        {
            get { return (Style)GetValue(UcStyleProperty); }
            set { SetValue(UcStyleProperty, value); }
        }

        public static readonly DependencyProperty UcStyleProperty =
            DependencyProperty.Register(nameof(UcStyle), typeof(Style), typeof(NotificationBannerUserControl), new PropertyMetadata(default(Style)));

        public bool BannerButtonCloseVisibility
        {
            get { return (bool)GetValue(BannerButtonCloseVisibilityProperty); }
            set { SetValue(BannerButtonCloseVisibilityProperty, value); }
        }

        public static readonly DependencyProperty BannerButtonCloseVisibilityProperty =
            DependencyProperty.Register(nameof(BannerButtonCloseVisibility), typeof(bool), typeof(NotificationBannerUserControl), new PropertyMetadata(false));

        public string Duration
        {
            get { return (string)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register(nameof(Duration), typeof(string), typeof(NotificationBannerUserControl), new PropertyMetadata("0:0:1"));

        public bool AutoReverse
        {
            get { return (bool)GetValue(AutoReverseProperty); }
            set { SetValue(AutoReverseProperty, value); }
        }

        public static readonly DependencyProperty AutoReverseProperty =
            DependencyProperty.Register(nameof(AutoReverse), typeof(bool), typeof(NotificationBannerUserControl), new PropertyMetadata(true));

        public string BannerSymbol
        {
            get { return (string)GetValue(BannerSymbolProperty); }
            set { SetValue(BannerSymbolProperty, value); }
        }

        public static readonly DependencyProperty BannerSymbolProperty =
            DependencyProperty.Register(nameof(BannerSymbol), typeof(string), typeof(NotificationBannerUserControl), new PropertyMetadata("Accept"));

        public string DefaultMessage { get; set; } = "Message";
        public string DefaultSuccess { get; set; } = "Success";
        public string DefaultError { get; set; } = "Error";
        public string DefaultWarning { get; set; } = "Warning";

        public NotificationBannerUserControl()
        {
            this.InitializeComponent();
        }

        private void NotificationBannerUserControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            Message = DefaultMessage;
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Opacity > 0) return;
            FadeInStoryboard.Stop();
            this.Opacity = 1;
        }

        private void ClearButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!(sender is Button) || this.Opacity < 1) return;
            FadeInStoryboard.Stop();
            this.Opacity = 0;
        }

        public void ShowIsSuccess(bool success, string message = "", bool buttonClose = false, bool autoreverse = true, string duration = "0:0:1")
        {
            if (success)
                ShowSuccess(message, buttonClose, autoreverse, duration);
            else
                ShowError(message, buttonClose, autoreverse, duration);
        }

        public void ShowSuccess(string message = "", bool buttonClose = false, bool autoreverse = true, string duration = "0:0:1")
        {
            Message = string.IsNullOrEmpty(message) ? DefaultSuccess : message;
            BannerSymbol = "Accept";
            UcStyle = (Style)this.Resources["NotificationBannerValid"];

            ShowNotificationBanner(buttonClose, autoreverse, duration);
        }

        public void ShowWarning(string message = "", bool buttonClose = false, bool autoreverse = true, string duration = "0:0:1")
        {
            Message = string.IsNullOrEmpty(message) ? DefaultWarning : message;
            BannerSymbol = "Important";
            UcStyle = (Style)this.Resources["NotificationBannerWarning"];

            ShowNotificationBanner(buttonClose, autoreverse, duration);
        }

        public void ShowError(string message = "", bool buttonClose = false, bool autoreverse = true, string duration = "0:0:1")
        {
            Message = string.IsNullOrEmpty(message) ? DefaultError : message;
            BannerSymbol = "Important";
            UcStyle = (Style)this.Resources["NotificationBannerError"];

            ShowNotificationBanner(buttonClose, autoreverse, duration);
        }

        private void ShowNotificationBanner(bool buttonClose = false, bool autoreverse = true, string duration = "0:0:1")
        {
            BannerButtonCloseVisibility = buttonClose;
            AutoReverse = autoreverse;
            Duration = duration;

            this.Opacity = 0;
            FadeInStoryboard.Begin();
        }
    }
}
