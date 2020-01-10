using System;
using Windows.UI.Xaml.Data;

namespace Yugen.Toolkit.Uwp.Converters
{
    /// <summary>
    /// TODO: sample how to use parameter in a converter
    /// </summary>
    public class ParameterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int intParam = (int)parameter;
            int intValue = (int)value;

            return intValue * intParam / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return 20;
        }
    }
}