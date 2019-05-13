using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Common.Standard.Helpers;

namespace Common.Uwp.Controls.Validation
{
    public sealed partial class ValidatingTextBoxUserControl
    {
        #region TextProperty

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(null));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #region IsSpellCheckEnabledProperty

        public static readonly DependencyProperty IsSpellCheckEnabledProperty = DependencyProperty.Register(
            nameof(IsSpellCheckEnabled),
            typeof(bool),
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(true));

        public bool IsSpellCheckEnabled
        {
            get { return (bool)GetValue(IsSpellCheckEnabledProperty); }
            set { SetValue(IsSpellCheckEnabledProperty, value); }
        }

        #endregion

        #region InputScopeProperty

        public static readonly DependencyProperty InputScopeProperty =
            DependencyProperty.Register(
                nameof(InputScope),
                typeof(string),
                typeof(ValidatingTextBoxUserControl),
                new PropertyMetadata("Default"));

        public string InputScope
        {
            get { return (string)GetValue(InputScopeProperty); }
            set { SetValue(InputScopeProperty, value); }
        }

        #endregion

        #region IsRealTimeValidationEnabledProperty

        public static readonly DependencyProperty IsRealTimeValidationEnabledProperty = DependencyProperty.Register(
            nameof(IsRealTimeValidationEnabled),
            typeof(bool),
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(false));

        public bool IsRealTimeValidationEnabled
        {
            get { return (bool)GetValue(IsRealTimeValidationEnabledProperty); }
            set { SetValue(IsRealTimeValidationEnabledProperty, value); }
        }

        #endregion

        #region IsSelectAllOnFocusEnabledProperty

        public static readonly DependencyProperty IsSelectAllOnFocusEnabledProperty = DependencyProperty.Register(
            nameof(IsSelectAllOnFocusEnabled),
            typeof(bool),
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(false));

        public bool IsSelectAllOnFocusEnabled
        {
            get { return (bool)GetValue(IsSelectAllOnFocusEnabledProperty); }
            set { SetValue(IsSelectAllOnFocusEnabledProperty, value); }
        }

        #endregion

        #region TextBoxSyleProperty

        public static readonly DependencyProperty TextBoxStyleProperty =
            DependencyProperty.Register(nameof(TextBoxStyle), typeof(Style), typeof(ValidatingTextBoxUserControl),
                new PropertyMetadata(default(Style)));

        public Style TextBoxStyle
        {
            get { return (Style)GetValue(TextBoxStyleProperty); }
            set { SetValue(TextBoxStyleProperty, value); }
        }

        #endregion

        public ValidatingTextBoxUserControl()
        {
            InitializeComponent();

            base.Init(ErrorMessage, MyTextBox);
        }
        
        private bool IsRealTimeRuleValidationMet()
        {
            if (ValidationRules == null) return true;

            bool[] isValid = { true };

            foreach (var rule in ValidationRules.Rules.TakeWhile(rule => isValid[0]))
            {
                isValid[0] = rule.IsValid(MyTextBox.Text);
            }

            return isValid[0];
        }


        public event TypedEventHandler<TextBox, TextBoxTextChangingEventArgs> MyTextBoxOnTextChanging;

        private void MyTextBox_OnTextChanging(TextBox sender, TextBoxTextChangingEventArgs e)
        {
            MyTextBoxOnTextChanging?.Invoke(sender, e);

            Text = MyTextBox.Text;
            LoggerHelper.WriteLine(GetType(), Text);

            if (string.IsNullOrEmpty(MyTextBox.Text)) return;
            if (!IsRealTimeValidationEnabled) return;
            if (IsRealTimeRuleValidationMet()) return;

            var selectionStart = MyTextBox.SelectionStart - 1;
            MyTextBox.Text = MyTextBox.Text.Remove(selectionStart, 1);
            Text = MyTextBox.Text;
            MyTextBox.SelectionStart = selectionStart;
            LoggerHelper.WriteLine(GetType(), Text);
        }

        public event TextChangedEventHandler MyTextBoxOnTextChanged;

        private void MyTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            base.ResetCustomValidation();
            MyTextBoxOnTextChanged?.Invoke(sender, e);
        }

        public event RoutedEventHandler MyTextBoxOnGotFocus;

        private void MyTextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            MyTextBoxOnGotFocus?.Invoke(sender, e);

            if (!IsSelectAllOnFocusEnabled) return;

            (sender as TextBox)?.SelectAll();
        }
    }
}