using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using UwpCommunity.Standard.Handlers;

namespace UwpCommunity.Uwp.Converters
{
    public class CommandToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is CommandHandler ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var v = value as Visibility?;
            return v == null ? (object)null : v.Value == Visibility.Visible;
        }
    }
}