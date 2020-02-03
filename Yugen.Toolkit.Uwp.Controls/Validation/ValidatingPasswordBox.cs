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

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ValidatingPasswordBox),
            new PropertyMetadata(null));

        public bool IsConfirmPassword
        {
            get { return (bool)GetValue(IsConfirmPasswordProperty); }
            set { SetValue(IsConfirmPasswordProperty, value); }
        }

        public static readonly DependencyProperty IsConfirmPasswordProperty = DependencyProperty.Register(
            nameof(IsConfirmPassword),
            typeof(bool),
            typeof(ValidatingPasswordBox),
            new PropertyMetadata(false));

        #endregion
        
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

        public event TypedEventHandler<PasswordBox, PasswordBoxPasswordChangingEventArgs> MyPasswordBoxOnTextChanging;

        private void MyPasswordBox_OnTextChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs e)
        {
            MyPasswordBoxOnTextChanging?.Invoke(sender, e);
        }

        public event RoutedEventHandler MyPasswordOnPasswordChanged;

        private void MyPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            base.ResetCustomValidation();
            MyPasswordOnPasswordChanged?.Invoke(sender, e);
        }
    }
}