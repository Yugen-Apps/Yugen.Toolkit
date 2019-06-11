using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpCommunity.Uwp.Extensions
{
    public static class FocusExtension
    {
        public static bool GetIsFocused(Control obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(Control obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static bool IsFocused { get; set; }

        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached(
            nameof(IsFocused), typeof(bool), typeof(FocusExtension),
            new PropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (Control)d;

            if ((bool) e.NewValue == (bool) e.OldValue) return;

            if ((bool)e.NewValue)
            {
                control.Focus(FocusState.Programmatic);
                control.LostFocus += Control_LostFocus;
            }
            else
            {
                control.GotFocus += Control_GotFocus;
            }
        }

        private static void Control_GotFocus(object sender, RoutedEventArgs e)
        {
            var control = (Control)sender;
            control.SetValue(IsFocusedProperty, true);
            control.GotFocus -= Control_GotFocus;
        }

        private static void Control_LostFocus(object sender, RoutedEventArgs e)
        {
            var control = (Control)sender;
            control.SetValue(IsFocusedProperty, false);
            control.LostFocus -= Control_LostFocus;
        }
    }
}
