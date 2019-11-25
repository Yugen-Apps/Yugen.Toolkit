using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace Yugen.Toolkit.Uwp.Converters
{
    public class DecimalConverter : IValueConverter
    {
        /// <summary>
        /// Converts a decimal value to a string value.
        /// </summary>
        /// <returns>
        /// Returns a string.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var stringConverted = ((decimal)value).ToString(CultureInfo.InvariantCulture);
            return stringConverted;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            decimal.TryParse(value.ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out decimal newValue);
            return newValue;
        }
    }
}