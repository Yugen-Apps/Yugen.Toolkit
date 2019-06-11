using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using UwpCommunity.Standard.Helpers;
using UwpCommunity.Uwp.Helpers;
using UwpCommunity.Standard.Validation;

namespace UwpCommunity.Uwp.Controls.Validation
{
    public sealed partial class ValidatingPasswordBoxUserControl
    {
        #region TextProperty

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ValidatingPasswordBoxUserControl),
            new PropertyMetadata(null));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #region OtherPasswordControlProperty

        public static readonly DependencyProperty OtherPasswordControlProperty = DependencyProperty.Register(
            nameof(OtherPasswordControl),
            typeof(string),
            typeof(ValidatingPasswordBoxUserControl),
            new PropertyMetadata(null));

        public string OtherPasswordControl
        {
            get { return (string)GetValue(OtherPasswordControlProperty); }
            set { SetValue(OtherPasswordControlProperty, value); }
        }

        #endregion

        #region IsConfirmPasswordProperty

        public static readonly DependencyProperty IsConfirmPasswordProperty = DependencyProperty.Register(
            nameof(IsConfirmPassword),
            typeof(bool),
            typeof(ValidatingPasswordBoxUserControl),
            new PropertyMetadata(false));

        public bool IsConfirmPassword
        {
            get { return (bool)GetValue(IsConfirmPasswordProperty); }
            set { SetValue(IsConfirmPasswordProperty, value); }
        }

        #endregion
        
        private ValidatingPasswordBoxUserControl OtherPassword { get; set; }

        public ValidatingPasswordBoxUserControl()
        {
            InitializeComponent();

            base.Init(ErrorMessage, MyPasswordBox);

            this.Loaded += ValidatingPasswordBoxUserControl_Loaded;
        }

        private void ValidatingPasswordBoxUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            InitOtherPasswordProperties();
        }

        private void InitOtherPasswordProperties()
        {
            if (!IsConfirmPassword) return;

            var validatingControlsList = ControlHelper.GetControlList<ValidatingPasswordBoxUserControl>(this.Parent);

            foreach (var control in validatingControlsList)
            {
                var passwordControl = control as ValidatingPasswordBoxUserControl;

                if (passwordControl?.IsConfirmPassword == false)
                    OtherPassword = passwordControl;
            }
        }

        public override bool IsValid()
        {
            var isValid = base.IsValid();

            if (OtherPassword == null)
                return isValid;

            if (!(ValidationRules?.Rules?.FirstOrDefault() is PasswordValidationRule rule))
                return isValid;

            ErrorMessage.Text = string.IsNullOrEmpty(RuleValidationMessage)
                ? rule.ErrorMessage
                : RuleValidationMessage;

            isValid = rule.IsValid(MyPasswordBox.Password, OtherPassword.MyPasswordBox?.Password);
            return isValid;
        }

        public event TypedEventHandler<PasswordBox, PasswordBoxPasswordChangingEventArgs> MyPasswordBoxOnTextChanging;

        private void MyPasswordBox_OnTextChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs e)
        {
            MyPasswordBoxOnTextChanging?.Invoke(sender, e);

            Text = MyPasswordBox.Password;
            LoggerHelper.WriteLine(GetType(), Text);
        }

        public event RoutedEventHandler MyPasswordOnPasswordChanged;

        private void MyPasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            base.ResetCustomValidation();
            MyPasswordOnPasswordChanged?.Invoke(sender, e);
        }
    }
}