using System;
using Windows.UI.Xaml.Data;

namespace UwpCommunity.Uwp.Controls.Graphs
{
    public class RealGoalConverter : IValueConverter
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