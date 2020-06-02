using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Yugen.Toolkit.Standard.Mvvm.ComponentModel
{
    /// <summary>
    /// A base class for objects of which the properties must be observable.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        /// <inheritdoc cref="INotifyPropertyChanged.PropertyChanged"/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <inheritdoc cref="INotifyPropertyChanging.PropertyChanging"/>
        public event PropertyChangingEventHandler PropertyChanging;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event if needed.
        /// </summary>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanging"/> event if needed.
        /// </summary>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        protected virtual void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        /// <summary>
        /// Compares the current and new values for a given property. If the value has changed,
        /// raises the <see cref="PropertyChanging"/> event, updates the property with the new
        /// value, then raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <typeparam name="T">The type of the property that changed.</typeparam>
        /// <param name="field">The field storing the property's value.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> events are not raised
        /// if the current and new value for the target property are the same.
        /// </remarks>
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            OnPropertyChanging(propertyName);

            field = newValue;

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Compares the current and new values for a given nested property. If the value has changed,
        /// raises the <see cref="PropertyChanging"/> event, updates the property and then raises the
        /// <see cref="PropertyChanged"/> event. The behavior mirrors that of <see cref="Set{T}(ref T,T,string)"/>,
        /// with the difference being that this method is used to relay properties from a wrapped model in the
        /// current instance. This type is useful when creating wrapping, bindable objects that operate over
        /// models that lack support for notification (eg. for CRUD operations).
        /// Suppose we have this model (eg. for a database row in a table):
        /// <code>
        /// public class Person
        /// {
        ///     public string Name { get; set; }
        /// }
        /// </code>
        /// We can then use a property to wrap instances of this type into our observable model (which supports
        /// notifications), injecting the notification to theproperties of that model, like so:
        /// <code>
        /// public class BindablePerson : ObservableObject
        /// {
        ///     public Model { get; }
        ///
        ///     public BindablePerson(Person model)
        ///     {
        ///         Model = model;
        ///     }
        ///
        ///     public string Name
        ///     {
        ///         get => Model.Name;
        ///         set => Set(() => Model.Name, value);
        ///     }
        /// }
        /// </code>
        /// This way we can then use the wrapping object in our application, and all those "proxy" properties will
        /// also raise notifications when changed. Note that this method is not meant to be a replacement for
        /// <see cref="Set{T}(ref T,T,string)"/>, which offers better performance and less memory usage. Only use this
        /// overload when relaying properties to a model that doesn't support notifications, and only if you can't
        /// implement notifications to that model directly (eg. by having it inherit from <see cref="ObservableObject"/>).
        /// </summary>
        /// <typeparam name="T">The type of property to set.</typeparam>
        /// <param name="propertyExpression">An <see cref="Expression{TDelegate}"/> returning the property to update.</param>
        /// <param name="newValue">The property's value after the change occurred.</param>
        /// <param name="propertyName">(optional) The name of the property that changed.</param>
        /// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
        /// <remarks>
        /// The <see cref="PropertyChanging"/> and <see cref="PropertyChanged"/> events are not raised
        /// if the current and new value for the target property are the same. Additionally, <paramref name="propertyExpression"/>
        /// must return a property from a model that is stored as another property in the current instance.
        /// This method only supports one level of indirection: <paramref name="propertyExpression"/> can only
        /// be used to access properties of a model that is directly stored as a property of the current instance.
        /// Additionally, this method can only be used if the wrapped item is a reference type.
        /// </remarks>
        protected bool Set<T>(Expression<Func<T>> propertyExpression, T newValue, [CallerMemberName] string propertyName = null)
        {
            // Get the target property info
            if (!(propertyExpression.Body is MemberExpression targetExpression &&
                  targetExpression.Member is PropertyInfo targetPropertyInfo &&
                  targetExpression.Expression is MemberExpression parentExpression &&
                  parentExpression.Member is PropertyInfo parentPropertyInfo &&
                  parentExpression.Expression is ConstantExpression instanceExpression &&
                  instanceExpression.Value is object instance))
            {
                ThrowArgumentExceptionForInvalidPropertyExpression();

                // This is never executed, as the method above always throws
                return false;
            }

            object parent = parentPropertyInfo.GetValue(instance);
            T oldValue = (T)targetPropertyInfo.GetValue(parent);

            if (EqualityComparer<T>.Default.Equals(oldValue, newValue))
            {
                return false;
            }

            OnPropertyChanging(propertyName);

            targetPropertyInfo.SetValue(parent, newValue);

            OnPropertyChanged(propertyName);

            return true;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> when a given <see cref="Expression{TDelegate}"/> is invalid for a property.
        /// </summary>
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowArgumentExceptionForInvalidPropertyExpression()
        {
            throw new ArgumentException("The given expression must be in the form () => MyModel.MyProperty");
        }

        /// <summary>
        /// SetField(()=> somewhere.Name = value; somewhere.Name, value)
        /// Advanced case where you rely on another property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentValue"></param>
        /// <param name="newValue"></param>
        /// <param name="propertyAction"></param>
        /// <param name="propertyName"></param>
        //protected void Set<T>(T currentValue, T newValue, Action propertyAction, [CallerMemberName] string propertyName = null)
        //{
        //    if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
        //    {
        //        return;
        //    }

        //    OnPropertyChanging(propertyName);

        //    propertyAction.Invoke();

        //    OnPropertyChanged(propertyName);
        //}

        protected void Set<T>(object objectName, T newValue, [CallerMemberName] string propertyName = null)
        {
            var property = objectName.GetType().GetProperty(propertyName);
            var currentValue = (T)property.GetValue(objectName);

            if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
            {
                return;
            }

            OnPropertyChanging(propertyName);

            property.SetValue(objectName, newValue, null);

            OnPropertyChanged(propertyName);
        }
    }
}


