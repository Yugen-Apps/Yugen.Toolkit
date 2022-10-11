using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Controls
{
    public sealed class SampleTemplatedControl : Control
    {
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            nameof(Message),
            typeof(string),
            typeof(SampleTemplatedControl),
            new PropertyMetadata(string.Empty, OnMessagePropertyChanged));

        private static void OnMessagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SampleTemplatedControl control)
            {
                control.Message3 = (string)e.NewValue;
                control.Message4 = (string)e.NewValue;
            }
        }

        private string _message2;

        public string Message2
        {
            get => _message2;
            set
            {
                _message2 = value;
                Message3 = value;
                Message4 = value;
            }
        }

        public string Message3
        {
            get { return (string)GetValue(Message3Property); }
            set { SetValue(Message3Property, value); }
        }

        public static readonly DependencyProperty Message3Property = DependencyProperty.Register(
            nameof(Message3),
            typeof(string),
            typeof(SampleTemplatedControl),
            new PropertyMetadata(string.Empty));

        public string Message4 { get; set; }

        public SampleTemplatedControl()
        {
            this.DefaultStyleKey = typeof(SampleTemplatedControl);
        }
    }
}
