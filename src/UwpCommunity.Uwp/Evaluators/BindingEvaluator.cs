using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace UwpCommunity.Uwp.Evaluators
{
    /// <summary>
    /// Utility class to facilitate temporary binding evaluation
    /// </summary>
    public class BindingEvaluator : FrameworkElement
    {
        /// <summary>
        /// Dependency property used to evaluate values.
        /// </summary>
        public static readonly DependencyProperty EvaluatorProperty = DependencyProperty.Register(
            "Evaluator",
            typeof(object),
            typeof(BindingEvaluator),
            null);

        private readonly string _propertyPath;

        /// <summary>
        /// Created binding evaluator and set path to the property which's value should be evaluated.
        /// </summary>
        /// <param name="propertyPath">Path to the property</param>
        public BindingEvaluator(string propertyPath)
        {
            _propertyPath = propertyPath;
        }

        /// <summary>
        /// Returns evaluated value of property on provided object source.
        /// </summary>
        /// <param name="source">Object for which property value should be evaluated</param>
        /// <returns>Value of the property</returns>
        public object Eval(object source)
        {
            ClearValue(EvaluatorProperty);
            var binding = new Binding
            {
                Path = new PropertyPath(_propertyPath),
                Mode = BindingMode.OneTime,
                Source = source
            };

            SetBinding(EvaluatorProperty, binding);
            return GetValue(EvaluatorProperty);
        }
    }
}