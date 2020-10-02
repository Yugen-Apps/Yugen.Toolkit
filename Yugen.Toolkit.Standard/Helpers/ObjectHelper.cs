using System;
using System.Collections.Generic;
using System.Text;

namespace Yugen.Toolkit.Standard.Helpers
{
    public static class ObjectHelper
    {
        public static object GetObjectProperty(object objectName, string propertyName) =>
            objectName.GetType().GetProperty(propertyName).GetValue(objectName);

        public static void SetObjectProperty<T>(object objectName, string propertyName, string subPopertyName, T value)
        {
            var propertyValue = GetObjectProperty(objectName, propertyName);

            SetObjectProperty(propertyValue, subPopertyName, value);
            SetObjectProperty(objectName, propertyName, propertyValue);
        }

        /// <summary>
        /// Some processing on the rest of the code to make sure we actually want to set this value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectName"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public static void SetObjectProperty<T>(object objectName, string propertyName, T value) =>
            objectName.GetType().GetProperty(propertyName).SetValue(objectName, value, null);
    }
}
