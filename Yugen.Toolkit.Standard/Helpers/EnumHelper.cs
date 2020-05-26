using System;
using System.ComponentModel;
using System.Reflection;

namespace Yugen.Toolkit.Standard.Helpers
{
    public static class EnumHelper
    {
        public static T GetValueFromDescription<T>(string description)
        {
            Type type = typeof(T);

            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (FieldInfo field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }

            return default;
        }
    }
}