using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Common.Uwp.Controls.Validation.Helpers;
using Common.Uwp.Helpers;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace Common.Uwp.Controls.Validation
{
    public sealed class ValidatingButtonControl : Button
    {
        public static readonly DependencyProperty IsEnterEnabledProperty = DependencyProperty.Register(
            nameof(IsEnterEnabled),
            typeof(bool),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata(false));

        public bool IsEnterEnabled
        {
            get { return (bool)GetValue(IsEnterEnabledProperty); }
            set { SetValue(IsEnterEnabledProperty, value); }
        }

        public new event TappedEventHandler Tapped;
        private DependencyObject _page;

        public ValidatingButtonControl()
        {
            base.Tapped += ValidatingButtonControl_Tapped;
            this.Loaded += ValidatingButtonControl_Loaded;
        }

        private void ValidatingButtonControl_Loaded(object sender, RoutedEventArgs e)
        {
            _page = ControlHelper.FindParentControl<Page>(sender);
            ValidatingFormHelper.Init(_page);

            if (!IsEnterEnabled) return;
            var keyboardAccelerator = new KeyboardAccelerator { Key = VirtualKey.Enter };
            keyboardAccelerator.Invoked += KeyboardAccelerator_Invoked;
            this.KeyboardAccelerators.Add(keyboardAccelerator);
        }

        private void KeyboardAccelerator_Invoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            if (ValidatingFormHelper.FormIsValid())
                Tapped?.Invoke(args.Element, null);
        }

        private void ValidatingButtonControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (ValidatingFormHelper.FormIsValid())
                Tapped?.Invoke(sender, e);
        }
    }
}
