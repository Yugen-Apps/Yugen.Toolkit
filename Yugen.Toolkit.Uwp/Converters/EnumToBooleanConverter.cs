using System;
using Windows.UI.Xaml.Data;

namespace Yugen.Mosaic.Uwp.Helpers
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public Type EnumType { get; set; }

        /// <summary>
        /// Converts an enum type parameter to bool.
        /// </summary>
        /// <returns>
        /// Returns true if the parameter is included in the enum value, else false.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter is string enumString)
            {
                if (!Enum.IsDefined(EnumType, value))
                {
                    throw new ArgumentException("ExceptionEnumToBooleanConverterValueMustBeAnEnum");
                }

                var enumValue = Enum.Parse(EnumType, enumString);

                return enumValue.Equals(value);
            }

            throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (parameter is string enumString)
            {
                return Enum.Parse(EnumType, enumString);
            }

            throw new ArgumentException("ExceptionEnumToBooleanConverterParameterMustBeAnEnumName");
        }
    }
}
