using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Yugen.Toolkit.Uwp.Converters
{
    public class DebugToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a DEBUG value to a Visibility value.
        /// </summary>
        /// <returns>
        /// Returns Visibility.Visible if true, else Visibility.Collapsed.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
#if DEBUG
            return Visibility.Visible;
#else
            return Visibility.Collapsed;
#endif
        }

        /// <summary>
        /// Converts a Visibility value to a bool value.
        /// </summary>
        /// <returns>
        /// Returns true if Visibility.Visible, else false.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var v = value as Visibility?;
            return v == null ? (object)null : v.Value == Visibility.Visible;
        }
    }
}