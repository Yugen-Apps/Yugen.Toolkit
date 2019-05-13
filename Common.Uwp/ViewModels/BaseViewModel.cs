using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Common.Uwp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// SetField (Name, value);
        /// Where there is a data member
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        public void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value)) return;
            storage = value;
            RaisePropertyChanged(propertyName);
        }
        
        /// <summary>
        /// SetField(()=> somewhere.Name = value; somewhere.Name, value)
        /// Advanced case where you rely on another property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentValue"></param>
        /// <param name="newValue"></param>
        /// <param name="doSet"></param>
        /// <param name="property"></param>
        protected void Set<T>(T currentValue, T newValue, Action doSet, [CallerMemberName] string property = null)
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return;
            doSet.Invoke();
            RaisePropertyChanged(property);
        }
    }

    public class BaseViewModel<T> : BaseViewModel where T : class, new()
    {
        public T Model;
        
        public static implicit operator T(BaseViewModel<T> model) { return model.Model; }
        
        public BaseViewModel(T model = null)
        {
            Model = model ?? new T();
        }
    }
}
