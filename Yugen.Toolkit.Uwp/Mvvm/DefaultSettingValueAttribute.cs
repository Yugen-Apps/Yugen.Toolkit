using System;

namespace Yugen.Toolkit.Uwp.Mvvm
{
    public sealed class DefaultSettingValueAttribute : Attribute
    {
        public DefaultSettingValueAttribute() { }

        public DefaultSettingValueAttribute(object value) => Value = value;

        public object Value { get; set; }
    }
}