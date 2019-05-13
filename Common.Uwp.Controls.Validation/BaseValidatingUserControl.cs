using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Common.Standard.Validation;
using System.Linq;
using Common.Uwp.Extensions;

namespace Common.Uwp.Controls.Validation
{
    public class BaseValidatingUserControl : UserControl
    {
        #region PlaceholderTextProperty

        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.Register(
            nameof(PlaceholderText),
            typeof(string),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata(null));

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        #endregion

        #region HeaderProperty

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata(string.Empty));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

        #region IsValidByDefaultProperty

        public static readonly DependencyProperty IsValidByDefaultProperty = DependencyProperty.Register(
            nameof(IsValidByDefault),
            typeof(bool),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata(true));

        public bool IsValidByDefault
        {
            get { return (bool)GetValue(IsValidByDefaultProperty); }
            set { SetValue(IsValidByDefaultProperty, value); }
        }

        #endregion

        #region IsMandatoryProperty

        public static readonly DependencyProperty IsMandatoryProperty = DependencyProperty.Register(
            nameof(IsMandatory),
            typeof(bool),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata(false));

        public bool IsMandatory
        {
            get { return (bool)GetValue(IsMandatoryProperty); }
            set { SetValue(IsMandatoryProperty, value); }
        }

        #endregion

        #region MandatoryValidationMessageProperty

        public static readonly DependencyProperty MandatoryValidationMessageProperty = DependencyProperty.Register(
            nameof(MandatoryValidationMessage),
            typeof(string),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata("A value is required."));

        public string MandatoryValidationMessage
        {
            get { return (string)GetValue(MandatoryValidationMessageProperty); }
            set { SetValue(MandatoryValidationMessageProperty, value); }
        }

        #endregion

        #region IsCustomValidProperty

        public static readonly DependencyProperty IsCustomValidProperty = DependencyProperty.Register(
            nameof(IsCustomValid),
            typeof(bool),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata(true, Update));

        public bool IsCustomValid
        {
            get { return (bool)GetValue(IsCustomValidProperty); }
            set { SetValue(IsCustomValidProperty, value); }
        }

        #endregion

        #region ValidationRulesProperty

        public static readonly DependencyProperty ValidationRulesProperty =
            DependencyProperty.Register(
                nameof(ValidationRules),
                typeof(ValidationRules),
                typeof(ValidatingComboBoxUserControl),
                new PropertyMetadata(null));

        public ValidationRules ValidationRules
        {
            get { return (ValidationRules)GetValue(ValidationRulesProperty); }
            set { SetValue(ValidationRulesProperty, value); }
        }

        #endregion

        #region RuleValidationMessageProperty

        public static readonly DependencyProperty RuleValidationMessageProperty =
            DependencyProperty.Register(
                nameof(RuleValidationMessage),
                typeof(string),
                typeof(ValidatingComboBoxUserControl),
                new PropertyMetadata(null));

        public string RuleValidationMessage
        {
            get { return (string)GetValue(RuleValidationMessageProperty); }
            set { SetValue(RuleValidationMessageProperty, value); }
        }

        #endregion

        #region CustomValidationMessageProperty

        public static readonly DependencyProperty CustomValidationMessageProperty =
            DependencyProperty.Register(
                nameof(CustomValidationMessage),
                typeof(string),
                typeof(ValidatingComboBoxUserControl),
                new PropertyMetadata("Custom validation failed", UpdateCustomMessage));

        public string CustomValidationMessage
        {
            get { return (string)GetValue(CustomValidationMessageProperty); }
            set { SetValue(CustomValidationMessageProperty, value); }
        }

        #endregion

        private TextBlock _errorMessage;
        private TextBox _myTextBox;
        private PasswordBox _myPasswordBox;
        private ComboBox _myComboBox;

        public BaseValidatingUserControl()
        {
            this.Loaded += BaseValidatingUserControl_Loaded;
        }

        private void BaseValidatingUserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListenToProperty(nameof(Visibility), OnVisibilityChanged);

            if (IsValidByDefault)
                VisualStateManager.GoToState(this, "Valid", true);
            else
                Update();
        }

        private void OnVisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != e.OldValue)
                Update();
        }

        protected void Init(TextBlock errorMessage, TextBox myTextBox)
        {
            _errorMessage = errorMessage;
            _myTextBox = myTextBox;
        }

        protected void Init(TextBlock errorMessage, PasswordBox myPasswordBox)
        {
            _errorMessage = errorMessage;
            _myPasswordBox = myPasswordBox;
        }

        protected void Init(TextBlock errorMessage, ComboBox myComboBox)
        {
            _errorMessage = errorMessage;
            _myComboBox = myComboBox;
        }

        public void Update()
        {
            VisualStateManager.GoToState(this, IsValid() ? "Valid" : "Invalid", true);
        }

        public virtual bool IsValid()
        {
            var isValid = IsMandatoryValidationMet();

            if (_myComboBox != null)
                return isValid;

            if (!isValid)
                return false;

            isValid = IsRuleValidationMet();
            if (!isValid)
                return false;

            isValid = IsCustomValidationMet();
            return isValid;
        }

        private bool IsMandatoryValidationMet()
        {
            if (!IsMandatory)
                return true;

            if (!string.IsNullOrWhiteSpace(_myTextBox?.Text))
                return true;

            if (!string.IsNullOrWhiteSpace(_myPasswordBox?.Password))
                return true;

            if (_myComboBox?.SelectedItem != null)
                return true;

            _errorMessage.Text = MandatoryValidationMessage;
            VisualStateManager.GoToState(this, "Mandatory", true);
            return false;
        }

        private bool IsRuleValidationMet()
        {
            if (ValidationRules == null) return true;

            bool[] isValid = { true };

            foreach (var rule in ValidationRules.Rules.TakeWhile(rule => isValid[0]))
            {
                if (_myTextBox != null)
                    isValid[0] = rule.IsValid(_myTextBox.Text);

                if (_myPasswordBox != null)
                    isValid[0] = rule.IsValid(_myPasswordBox.Password);

                if (!isValid[0])
                    _errorMessage.Text = string.IsNullOrEmpty(RuleValidationMessage)
                        ? rule.ErrorMessage
                        : RuleValidationMessage;
            }

            return isValid[0];
        }

        private bool IsCustomValidationMet()
        {
            if (IsCustomValid)
                return true;

            _errorMessage.Text = CustomValidationMessage;
            return false;
        }

        private void UpdateCustomMessage()
        {
            _errorMessage.Text = CustomValidationMessage;
        }

        public void ResetCustomValidation()
        {
            IsCustomValid = true;

            Update();
        }


        public static void UpdateCustomMessage(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BaseValidatingUserControl;
            control?.UpdateCustomMessage();
        }

        public static void Update(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as BaseValidatingUserControl;
            control?.Update();
        }
    }
}