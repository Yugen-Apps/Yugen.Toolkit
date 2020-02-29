using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Standard.Validation;
using Yugen.Toolkit.Uwp.Helpers;

namespace Yugen.Toolkit.Uwp.Controls.Validation
{
    public sealed partial class ValidatingPasswordBox
    {
        #region DependencyProperties

        /// <summary>
        /// Get or set the text
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="Text"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ValidatingPasswordBox),
            new PropertyMetadata(null));

        /// <summary>
        /// Get or set this as a confirmation password field
        /// </summary>
        public bool IsConfirmPassword
        {
            get { return (bool)GetValue(IsConfirmPasswordProperty); }
            set { SetValue(IsConfirmPasswordProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="IsConfirmPassword"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsConfirmPasswordProperty = DependencyProperty.Register(
            nameof(IsConfirmPassword),
            typeof(bool),
            typeof(ValidatingPasswordBox),
            new PropertyMetadata(false));

        /// <summary>
        /// Get or set if the selection of all text on focuse is enabled
        /// </summary>
        public bool IsSelectAllOnFocusEnabled
        {
            get { return (bool)GetValue(IsSelectAllOnFocusEnabledProperty); }
            set { SetValue(IsSelectAllOnFocusEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="IsSelectAllOnFocusEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSelectAllOnFocusEnabledProperty = DependencyProperty.Register(
            nameof(IsSelectAllOnFocusEnabled),
            typeof(bool),
            typeof(ValidatingTextBox),
            new PropertyMetadata(false));

        #endregion

        /// <summary>
        /// Get or set the other password control
        /// </summary>
        public ValidatingPasswordBox OtherPassword { get; set; }

        public ValidatingPasswordBox()
        {
            InitializeComponent();

            base.Init(ErrorMessageTextBlock, MyPasswordBox);
        }

        public override bool IsValid()
        {
            var isValid = base.IsValid();

            if (OtherPassword == null)
                return isValid;

            if (!(ValidationRules?.Rules?.FirstOrDefault() is PasswordValidationRule rule))
                return isValid;

            ErrorMessageTextBlock.Text = string.IsNullOrEmpty(RuleValidationMessage)
                ? rule.ErrorMessage
                : RuleValidationMessage;

            isValid = rule.IsValid(MyPasswordBox.Password, OtherPassword.MyPasswordBox?.Password);
            return isValid;
        }

        /// <summary>
        ///  Occurs when the currently text is changing.
        /// </summary>
        public event TypedEventHandler<PasswordBox, PasswordBoxPasswordChangingEventArgs> MyPasswordBoxOnTextChanging;

        private void MyPasswordBox_OnTextChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs e)
        {
            MyPasswordBoxOnTextChanging?.Invoke(sender, e);
        }

        /// <summary>
        ///  Occurs when the currently text is changed.
        /// </summary>
        public event RoutedEventHandler MyPasswordOnPasswordChanged;

        private void MyPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            base.ResetCustomValidation();
            MyPasswordOnPasswordChanged?.Invoke(sender, e);
        }


        /// <summary>
        ///  Occurs when a UIElement receives focus.
        /// </summary>
        public event RoutedEventHandler MyTextBoxOnGotFocus;

        private void MyTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            MyTextBoxOnGotFocus?.Invoke(sender, e);

            if (!IsSelectAllOnFocusEnabled) return;

            (sender as TextBox)?.SelectAll();
        }
    }
}