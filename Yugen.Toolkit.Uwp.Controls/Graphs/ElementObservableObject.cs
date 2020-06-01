using Yugen.Toolkit.Standard.Mvvm.ComponentModel;

namespace Yugen.Toolkit.Uwp.Controls.Graphs
{
    public class ElementObservableObject : ObservableObject
    {
        private string _label;
        /// <summary>
        /// Get or set a value indicating the label of the element
        /// </summary>
        public string Label
        {
            get { return _label; }
            set { Set(ref _label, value); }
        }

        private int _value;
        /// <summary>
        /// Get or set a value indicating the value of the element
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { Set(ref _value, value); }
        }
    }
}