using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace Yugen.Toolkit.Uwp.Mvvm
{
    public class ObservableSettings : INotifyPropertyChanged
    {
        private readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (typeof(T).IsValueType ||
                typeof(T) == typeof(string))
            {
                return SetInternal(value, propertyName);
            }
            else
            {
                ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue();
                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    composite[property.Name] = property.GetValue(value);
                }                
                return SetInternal(composite, propertyName);
            }
        }

        private bool SetInternal<T>(T value, string propertyName)
        {
            if (localSettings.Values.ContainsKey(propertyName))
            {
                var currentValue = (T)localSettings.Values[propertyName];
                if (EqualityComparer<T>.Default.Equals(currentValue, value))
                    return false;
            }

            localSettings.Values[propertyName] = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }


        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (typeof(T).IsValueType ||
              typeof(T) == typeof(string))
            {
                return GetInternal<T>(propertyName);
            }
            else
            {
                ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)localSettings.Values[propertyName];        
                var newObject = (T)Activator.CreateInstance(typeof(T));

                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    var v = composite[property.Name];
                    newObject.GetType().GetProperty(property.Name).SetValue(newObject, v);                    
                }

                return newObject;
            }
        }

        private T GetInternal<T>(string propertyName)
        {
            if (localSettings.Values.ContainsKey(propertyName))
                return (T)localSettings.Values[propertyName];

            var attributes = GetType().GetTypeInfo().GetDeclaredProperty(propertyName).CustomAttributes.Where(ca => ca.AttributeType == typeof(DefaultSettingValueAttribute)).ToList();
            if (attributes.Count == 1)
                return (T)attributes[0].NamedArguments[0].TypedValue.Value;

            return default;
        }
    }
}