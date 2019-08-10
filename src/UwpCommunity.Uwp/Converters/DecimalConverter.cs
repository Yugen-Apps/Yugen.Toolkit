using System;
using System.Globalization;
using Windows.UI.Xaml.Data;

namespace UwpCommunity.Uwp.Converters
{
    public class DecimalConverter : IValueConverter
    {
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