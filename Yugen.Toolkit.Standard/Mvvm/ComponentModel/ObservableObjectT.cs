using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace Yugen.Toolkit.Standard.Mvvm.ComponentModel
{
    /// <summary>
    /// A base class for Generic objects of which the properties must be observable.
    /// </summary>
    public abstract class ObservableObject<T> : ObservableObject where T : class, new()
    {
        /// <summary>
        /// A model wrapped in every ObservableObject(T) object
        /// </summary>
        protected T Model { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObservableObject"/> class.
        /// </summary>
        /// <param name="model">The wrapped model</param>
        public ObservableObject(T model = null)
        {
            Model = model ?? new T();
        }

        /// <summary>
        /// Creates a T object from observableObject. 
        /// </summary>
        /// <param name="observableObject">the observableObject</param>
        public static implicit operator T(ObservableObject<T> observableObject) => 
            observableObject.Model;
    }
}