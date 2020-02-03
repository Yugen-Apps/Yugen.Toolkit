using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Yugen.Toolkit.Uwp.Controls.Validation
{
    public sealed class ValidatingButton : Button
    {
        #region DependencyProperties

        /// <summary>
        /// Enable or disable Enter KeyboardAccelerator
        /// </summary>
        public bool IsEnterEnabled
        {
            get { return (bool)GetValue(IsEnterEnabledProperty); }
            set { SetValue(IsEnterEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="IsEnterEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsEnterEnabledProperty = DependencyProperty.Register(
            nameof(IsEnterEnabled),
            typeof(bool),
            typeof(ValidatingComboBox),
            new PropertyMetadata(false));

        #endregion

        /// <summary>
        ///    Occurs when an otherwise unhandled **Tap** interaction occurs over the hit test
        ///     area of this element.
        /// </summary>
        public new event TappedEventHandler Tapped;

        /// <summary>
        /// Represents the method that will handle routed events.
        /// </summary>
        public new event RoutedEventHandler Click;

        /// <summary>
        /// Get or Set the ValidatingForm control
        /// </summary>
        public ValidatingForm ValidatingForm { get; set; }

        public ValidatingButton()
        {
            this.Loaded += ValidatingButtonControl_Loaded;
            base.Click += ValidatingButton_Click;
        }

        private void ValidatingButtonControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsEnterEnabled) return;
            var keyboardAccelerator = new KeyboardAccelerator { Key = VirtualKey.Enter };
            keyboardAccelerator.Invoked += KeyboardAccelerator_Invoked;
            this.KeyboardAccelerators.Add(keyboardAccelerator);
        }

        private void KeyboardAccelerator_Invoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            if (ValidatingForm.IsValid())
            {
                Tapped?.Invoke(args.Element, null);
                Click?.Invoke(args.Element, null);
            }
        }

        private void ValidatingButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatingForm.IsValid())
                Click?.Invoke(sender, e);
        }
    }
}
