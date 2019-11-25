using System;
using Windows.UI.Xaml.Data;

namespace Yugen.Toolkit.Uwp.Converters
{
    public class NullableToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts a nullable value to a bool value.
        /// </summary>
        /// <returns>
        /// Returns false if null, else true.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return false;

            return (bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}