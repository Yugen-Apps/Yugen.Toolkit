using System;
using Windows.UI.Xaml.Data;

namespace Yugen.Toolkit.Uwp.CodeChallenge.Interfaces
{
    public class PasscodeInputDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? "Please confirm your pin:" : "Set a six-digit passcode:";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
