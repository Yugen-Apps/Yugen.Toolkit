namespace Yugen.Toolkit.Standard.Mvvm.ComponentModel
{
    /// <summary>
    /// A base class for viewmodels.
    /// </summary>
    public abstract class ObservableObject<T> : ObservableObject where T : class, new()
    {
        public T Model { get; }

        public ObservableObject(T model = null)
        {
            Model = model ?? new T();
        }

        public static implicit operator T(ObservableObject<T> model) => model.Model;
    }
}