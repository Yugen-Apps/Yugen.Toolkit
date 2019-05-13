using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Common.Uwp.Converters
{
    public class StringFormatterConverter : IValueConverter
    {
        // This converts the value object to the string to display.
        // This will work with most simple types.
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            // Retrieve the format string and use it to format the value.
            string formatString = parameter as string;

            // If the format string is null or empty, simply call ToString() on the value.
            return string.IsNullOrEmpty(formatString) ? 
                value.ToString() : 
                string.Format(formatString, value, CultureInfo.CurrentCulture);
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}