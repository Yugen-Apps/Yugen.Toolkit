using System;
using Windows.UI.Xaml.Data;

namespace Yugen.Toolkit.Uwp.Converters
{
    public class ComboBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}