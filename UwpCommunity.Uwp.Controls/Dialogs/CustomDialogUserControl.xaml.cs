﻿using System;
using System.Threading.Tasks;
using UwpCommunity.Uwp.Controls.Dialogs;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace UwpCommunity.Uwp.Controls.Dialogs
{
    /// <summary>
    /// A dialog
    /// </summary>
    public sealed partial class CustomDialogUserControl : UserControl
    {
        #region DependencyProperties

        /// <summary>
        /// Dialog title
        /// </summary>
        public string DialogTitle
        {
            get { return (string)GetValue(DialogTitleProperty); }
            set { SetValue(DialogTitleProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for DialogTitle.  This enables animation, styling, binding, etc...
        /// </summary>        
        public static readonly DependencyProperty DialogTitleProperty = DependencyProperty.Register(
            nameof(DialogTitle), 
            typeof(string), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(string.Empty));
        
        /// <summary>
        /// Close button X Foreground
        /// </summary>
        public SolidColorBrush CloseGlyphForeground
        {
            get { return (SolidColorBrush)GetValue(CloseGlyphForegroundProperty); }
            set { SetValue(CloseGlyphForegroundProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for CloseGlyphForeground.  This enables animation, styling, binding, etc...
        /// </summary>        
        public static readonly DependencyProperty CloseGlyphForegroundProperty = DependencyProperty.Register(
            nameof(CloseGlyphForeground), 
            typeof(SolidColorBrush), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Top bar Background
        /// </summary>
        public Brush TopBarBackground
        {
            get { return (Brush)GetValue(TopBarBackgroundProperty); }
            set { SetValue(TopBarBackgroundProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for TopBarBackground.  This enables animation, styling, binding, etc... 
        /// </summary>
        public static readonly DependencyProperty TopBarBackgroundProperty = DependencyProperty.Register(
            nameof(TopBarBackground), 
            typeof(Brush), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(new SolidColorBrush(Colors.WhiteSmoke)));
        
        /// <summary>
        /// Top Bar foreground
        /// </summary>
        public SolidColorBrush TopBarForeground
        {
            get { return (SolidColorBrush)GetValue(TopBarForegroundProperty); }
            set { SetValue(TopBarForegroundProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for TopBarForeground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TopBarForegroundProperty = DependencyProperty.Register(
            nameof(TopBarForeground), 
            typeof(SolidColorBrush), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(new SolidColorBrush(Colors.Black)));
    
        /// <summary>
        /// Content Background
        /// </summary>
        public Brush ContentBackground
        {
            get { return (Brush)GetValue(ContentBackgroundProperty); }
            set { SetValue(ContentBackgroundProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ContentBackgroundProperty = DependencyProperty.Register(
            nameof(ContentBackground), 
            typeof(Brush), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(new SolidColorBrush(Colors.WhiteSmoke)));

        /// <summary>
        /// The main content
        /// </summary>
        public new UIElement Content
        {
            get { return (UIElement)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for MessageFontFamily.  This enables animation, styling, binding, etc...
        /// </summary>
        public new static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            nameof(ContentProperty), 
            typeof(UIElement), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(null));
    
        /// <summary>
        /// Button bar background
        /// </summary>
        public Brush ButtonAreaBackground
        {
            get { return (Brush)GetValue(ButtonAreaBackgroundProperty); }
            set { SetValue(ButtonAreaBackgroundProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ButtonAreaBackgroundProperty = DependencyProperty.Register(
            nameof(ButtonAreaBackground), 
            typeof(Brush), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(new SolidColorBrush(Colors.WhiteSmoke)));

        /// <summary>
        /// Left button style
        /// </summary>
        public Style LeftButtonStyle
        {
            get { return (Style)GetValue(LeftButtonStyleProperty); }
            set { SetValue(LeftButtonStyleProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty LeftButtonStyleProperty = DependencyProperty.Register(
            nameof(LeftButtonStyle), 
            typeof(Style), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(null));

        public string LeftButtonText
        {
            get { return (string)GetValue(LeftButtonTextProperty); }
            set { SetValue(LeftButtonTextProperty, value); }
        }
   
        public static readonly DependencyProperty LeftButtonTextProperty = DependencyProperty.Register(
            nameof(LeftButtonText), 
            typeof(string), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(string.Empty));

        /// <summary>
        /// Right button style
        /// </summary>
        public Style RightButtonStyle
        {
            get { return (Style)GetValue(RightButtonStyleProperty); }
            set { SetValue(RightButtonStyleProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty RightButtonStyleProperty = DependencyProperty.Register(
            nameof(RightButtonStyle), 
            typeof(Style), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(null));

        public string RightButtonText
        {
            get { return (string)GetValue(RightButtonTextProperty); }
            set { SetValue(RightButtonTextProperty, value); }
        }

        public static readonly DependencyProperty RightButtonTextProperty = DependencyProperty.Register(
            nameof(RightButtonText), 
            typeof(string), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(default(string)));

        /// <summary>
        /// Dialog type 
        /// </summary>
        public CustomDialogTypeEnum Type
        {
            get { return (CustomDialogTypeEnum)GetValue(TypeProperty); }
            set
            {
                SetValue(TypeProperty, value);
                switch (value)
                {
                    case CustomDialogTypeEnum.OnlyTopBar:
                        {
                            IsTopBarVisible = true;
                            IsButtonAreaVisible = false;
                            IsButtonLeftVisible = false;
                            break;
                        }
                    case CustomDialogTypeEnum.TopBarWithButtons:
                        {
                            IsTopBarVisible = true;
                            IsButtonAreaVisible = true;
                            IsButtonLeftVisible = true;
                            break;
                        }
                    case CustomDialogTypeEnum.Nothing:
                        {
                            IsTopBarVisible = false;
                            IsButtonAreaVisible = false;
                            IsButtonLeftVisible = false;
                            break;
                        }
                    case CustomDialogTypeEnum.TopBarAcceptButton:
                        {
                            IsTopBarVisible = true;
                            IsButtonAreaVisible = true;
                            IsButtonLeftVisible = false;
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            nameof(Type), 
            typeof(CustomDialogTypeEnum), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(CustomDialogTypeEnum.OnlyTopBar));

        /// <summary>
        /// Control visibility
        /// </summary>
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.Register(
            nameof(IsVisible), 
            typeof(bool), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(true));

        /// <summary>
        /// Top bar visibility
        /// </summary>
        public bool IsTopBarVisible
        {
            get { return (bool)GetValue(IsTopBarVisibleProperty); }
            set { SetValue(IsTopBarVisibleProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsTopBarVisibleProperty = DependencyProperty.Register(
            nameof(IsTopBarVisible), 
            typeof(bool), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(true));

        /// <summary>
        /// Bottom bar visibility
        /// </summary>
        public bool IsButtonAreaVisible
        {
            get { return (bool)GetValue(IsButtonAreaVisibleProperty); }
            set { SetValue(IsButtonAreaVisibleProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsButtonAreaVisibleProperty = DependencyProperty.Register(
            nameof(IsButtonAreaVisible), 
            typeof(bool), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(true));

        /// <summary>
        /// Bottom bar visibility
        /// </summary>
        public bool IsButtonLeftVisible
        {
            get { return (bool)GetValue(IsButtonLeftVisibleProperty); }
            set { SetValue(IsButtonLeftVisibleProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContentBackground.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsButtonLeftVisibleProperty = DependencyProperty.Register(
            nameof(IsButtonLeftVisible), 
            typeof(bool), 
            typeof(CustomDialogUserControl), 
            new PropertyMetadata(true));

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register(
            nameof(Duration),
            typeof(TimeSpan),
            typeof(NotificationBannerUserControl),
            new PropertyMetadata(null));

        #endregion

        #region Events

        /// <summary>
        /// Left Button tapped event
        /// </summary>
        public event TappedEventHandler LeftButtonTapped;

        /// <summary>
        /// Right button tapped event
        /// </summary>
        public event TappedEventHandler RightButtonTapped;

        /// <summary>
        /// Close button tapped event (append to close event)
        /// </summary>
        public event TappedEventHandler CloseButtonTapped;

        /// <summary>
        /// Event in opening
        /// </summary>
        /// <param name="content"></param>
        public event Action<object> DialogOpening;

        //public event OpenEventHandler DialogOpening;
        //public delegate void OpenEventHandler(object content);

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public CustomDialogUserControl()
        {
            InitializeComponent();
            FadeStoryboard.Completed += FadeStoryboard_Completed;
        }

        /// <summary>
        /// Left button tapped method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeftButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button b = sender as Button;
            b.IsEnabled = false;
            LeftButtonTapped?.Invoke(Content, e);
            b.IsEnabled = true;
        }

        /// <summary>
        /// Right button tapped method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RightButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Button b = sender as Button;
            b.IsEnabled = false;
            RightButtonTapped?.Invoke(Content, e);
            b.IsEnabled = true;
        }

        /// <summary>
        /// Close button tapped method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Grid g = sender as Grid;
            //Storyboard s = g.Resources["CloseButtonStoryboard"] as Storyboard;
            CloseButtonStoryboard.Begin();
            CloseButtonTapped?.Invoke(Content, e);
            HideDialog();
        }

        /// <summary>
        /// Show a dialog with fadein animation
        /// </summary>
        /// <param name="animationDuration">the translate time</param>
        public void ShowDialog(int animationDuration = 1000)
        {
            if (IsVisible) return;
            DialogOpening?.Invoke(Content);

            IsVisible = true;            
            Fade(0, 1, animationDuration);
        }

        /// <summary>
        /// Hide dialog with fadeout animation
        /// <param name="animationDuration">the translate time</param>
        /// </summary>
        public void HideDialog(int animationDuration = 1000)
        {
            Fade(1, 0, animationDuration);
        }

        private void Fade(int from, int to, int animationDuration = 1000)
        {
            Duration = new TimeSpan(0, 0, 0, 0, animationDuration);
            FadeAnimation.From = from;
            FadeAnimation.To = to;
            FadeStoryboard.Begin();
        }

        private void FadeStoryboard_Completed(object sender, object e)
        {
            if(Opacity == 0)
                IsVisible = false;
        }
    }
}