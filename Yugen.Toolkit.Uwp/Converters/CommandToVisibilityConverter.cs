using System;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Yugen.Toolkit.Uwp.Converters
{
    public class CommandToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a command value to a Visibility value.
        /// </summary>
        /// <returns>
        /// Returns Visibility.Visible if is a commandHandler, else Visibility.Collapsed.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is ICommand ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var v = value as Visibility?;
            return v == null ? (object)null : v.Value == Visibility.Visible;
        }
    }
}