using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

namespace Yugen.Toolkit.Uwp.Controls.Dialogs
{
    /// <summary>
    /// NotificationBanner defines a control to show local notification banner in the app.
    /// </summary>
    public sealed partial class NotificationBanner : UserControl
    {
        #region DependencyProperties

        /// <summary>
        /// Gets or sets a value indicating the message of the control.
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Message"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty MessageProperty =DependencyProperty.Register(
            nameof(Message), 
            typeof(string), 
            typeof(NotificationBanner), 
            new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets a value indicating the style of the control.
        /// </summary>
        public Style UcStyle
        {
            get { return (Style)GetValue(UcStyleProperty); }
            set { SetValue(UcStyleProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="UcStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty UcStyleProperty = DependencyProperty.Register(
            nameof(UcStyle), 
            typeof(Style), 
            typeof(NotificationBanner), 
            new PropertyMetadata(default(Style)));

        /// <summary>
        /// Gets or sets a value indicating whether to show the Close button of the control.
        /// </summary>
        public bool IsCloseButtonVisible
        {
            get { return (bool)GetValue(IsCloseButtonVisibleProperty); }
            set { SetValue(IsCloseButtonVisibleProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="IsCloseButtonVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsCloseButtonVisibleProperty = DependencyProperty.Register(
            nameof(IsCloseButtonVisible), 
            typeof(bool), 
            typeof(NotificationBanner), 
            new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a value indicating the duration of the popup animation (in milliseconds).
        /// </summary>
        public TimeSpan AnimationDuration
        {
            get { return (TimeSpan)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="AnimationDuration"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
            nameof(AnimationDuration), 
            typeof(TimeSpan), 
            typeof(NotificationBanner), 
            new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a value indicating whether to autoreverse the popup animation.
        /// </summary>
        public bool AutoReverse
        {
            get { return (bool)GetValue(AutoReverseProperty); }
            set { SetValue(AutoReverseProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="AutoReverse"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AutoReverseProperty = DependencyProperty.Register(
            nameof(AutoReverse), 
            typeof(bool), 
            typeof(NotificationBanner), 
            new PropertyMetadata(true));

        /// <summary>
        /// Gets or sets a value indicating the symbol of the control.
        /// </summary>
        public Symbol Symbol
        {
            get { return (Symbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Symbol"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SymbolProperty = DependencyProperty.Register(
            nameof(Symbol), 
            typeof(Symbol), 
            typeof(NotificationBanner), 
            new PropertyMetadata(DefaultSuccessSymbol));

        #endregion

        /// <summary>
        /// Default symbol value of the control.
        /// </summary>
        private const Symbol DefaultSuccessSymbol = Symbol.Accept;

        /// <summary>
        /// Default symbol value of the control.
        /// </summary>
        private const Symbol DefaultWarningSymbol = Symbol.Help;

        /// <summary>
        /// Default symbol value of the control.
        /// </summary>
        private const Symbol DefaultErrorSymbol = Symbol.ReportHacked;

        /// <summary>
        /// Default success message value of the control.
        /// </summary>
        public string DefaultSuccessMessage { get; set; } = "Success";

        /// <summary>
        /// Default warning message value of the control.
        /// </summary>
        public string DefaultWarningMessage { get; set; } = "Warning";

        /// <summary>
        /// Default error message value of the control.
        /// </summary>
        public string DefaultErrorMessage { get; set; } = "Error";

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationBanner"/> class.
        /// </summary>
        public NotificationBanner()
        {
            InitializeComponent();
            Storyboard.SetTarget(FadeStoryboard, this);
        }
        
        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Opacity > 0) return;
            FadeStoryboard.Stop();
            Opacity = 1;
        }

        private void ClearButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!(sender is Button) || Opacity < 1) return;
            FadeStoryboard.Stop();
            Opacity = 0;
        }

        /// <summary>
        /// Show success or error notification banner according to <paramref name="isSuccess"/>
        /// </summary>     
        /// <param name="isSuccess">Value indicating whether to show the success or error notification.</param>
        /// <param name="message">Text used as the content of the notification</param>
        /// <param name="showCloseButton">Value indicating whether to show the close button of the notification.</param>
        /// <param name="autoreverse">Value indicating whether to autorever the animation of the notification.</param>
        /// <param name="duration">Animation duration of the notification in ms</param>
        public void ShowIsSuccess(bool isSuccess, string message = "", bool showCloseButton = false, bool autoreverse = true, int duration = 1000)
        {
            if (isSuccess)
                ShowSuccess(message, showCloseButton, autoreverse, duration);
            else
                ShowError(message, showCloseButton, autoreverse, duration);
        }

        /// <summary>
        /// Show success notification banner
        /// </summary>     
        /// <param name="message">Text used as the content of the notification</param>
        /// <param name="showCloseButton">Value indicating whether to show the close button of the notification.</param>
        /// <param name="autoreverse">Value indicating whether to autorever the animation of the notification.</param>
        /// <param name="duration">Animation duration of the notification in ms</param>
        public void ShowSuccess(string message = "", bool showCloseButton = false, bool autoreverse = true, int duration = 1000)
        {
            Message = string.IsNullOrEmpty(message) ? DefaultSuccessMessage : message;
            Symbol = DefaultSuccessSymbol;
            UcStyle = (Style)Resources["NotificationBannerValid"];

            ShowNotificationBanner(showCloseButton, autoreverse, duration);
        }

        /// <summary>
        /// Show warning notification banner
        /// </summary>     
        /// <param name="message">Text used as the content of the notification</param>
        /// <param name="showCloseButton">Value indicating whether to show the close button of the notification.</param>
        /// <param name="autoreverse">Value indicating whether to autorever the animation of the notification.</param>
        /// <param name="duration">Animation duration of the notification in ms</param>
        public void ShowWarning(string message = "", bool showCloseButton = false, bool autoreverse = true, int duration = 1000)
        {
            Message = string.IsNullOrEmpty(message) ? DefaultWarningMessage : message;
            Symbol = DefaultWarningSymbol;
            UcStyle = (Style)Resources["NotificationBannerWarning"];

            ShowNotificationBanner(showCloseButton, autoreverse, duration);
        }

        /// <summary>
        /// Show error notification banner
        /// </summary>     
        /// <param name="message">Text used as the content of the notification</param>
        /// <param name="showCloseButton">Value indicating whether to show the close button of the notification.</param>
        /// <param name="autoreverse">Value indicating whether to autorever the animation of the notification.</param>
        /// <param name="duration">Animation duration of the notification in ms</param>
        public void ShowError(string message = "", bool showCloseButton = false, bool autoreverse = true, int duration = 1000)
        {
            Message = string.IsNullOrEmpty(message) ? DefaultErrorMessage : message;
            Symbol = DefaultErrorSymbol;
            UcStyle = (Style)Resources["NotificationBannerError"];

            ShowNotificationBanner(showCloseButton, autoreverse, duration);
        }

        private void ShowNotificationBanner(bool showCloseButton, bool autoreverse, int duration)
        {
            IsCloseButtonVisible = showCloseButton;
            AutoReverse = autoreverse;
            AnimationDuration = new TimeSpan(0,0,0,0,duration);

            Opacity = 0;
            FadeStoryboard.Begin();
        }
    }
}
