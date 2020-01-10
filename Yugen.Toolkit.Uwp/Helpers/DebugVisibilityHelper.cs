using Windows.UI.Xaml;

namespace Yugen.Toolkit.Uwp.Helpers
{
    public static class DebugVisibilityHelper
    {
        public static readonly DependencyProperty IsVisibleProperty = DependencyProperty.RegisterAttached(
            "Debug", typeof(bool), typeof(DebugVisibilityHelper), new PropertyMetadata(default(bool), IsVisibleChangedCallback));

        private static void IsVisibleChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FrameworkElement fe))
                return;
#if DEBUG
            fe.Visibility = Visibility.Visible;
#else
            fe.Visibility = Visibility.Collapsed;
#endif
        }

        public static void SetIsVisible(DependencyObject element, bool value)
        {
            element.SetValue(IsVisibleProperty, value);
        }

        public static bool GetIsVisible(DependencyObject element)
        {
            return (bool)element.GetValue(IsVisibleProperty);
        }
    }
}