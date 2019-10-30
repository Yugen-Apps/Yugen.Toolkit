using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Standard.Helpers;

namespace Yugen.Toolkit.Uwp.Controls.Validation
{
    public sealed partial class ValidatingTextBoxUserControl
    {
        #region DependencyProperties

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(null));

        public bool IsSpellCheckEnabled
        {
            get { return (bool)GetValue(IsSpellCheckEnabledProperty); }
            set { SetValue(IsSpellCheckEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsSpellCheckEnabledProperty = DependencyProperty.Register(
            nameof(IsSpellCheckEnabled),
            typeof(bool),
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(true));

        public string InputScope
        {
            get { return (string)GetValue(InputScopeProperty); }
            set { SetValue(InputScopeProperty, value); }
        }

        public static readonly DependencyProperty InputScopeProperty =
            DependencyProperty.Register(
                nameof(InputScope),
                typeof(string),
                typeof(ValidatingTextBoxUserControl),
                new PropertyMetadata("Default"));

        public bool IsRealTimeValidationEnabled
        {
            get { return (bool)GetValue(IsRealTimeValidationEnabledProperty); }
            set { SetValue(IsRealTimeValidationEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsRealTimeValidationEnabledProperty = DependencyProperty.Register(
            nameof(IsRealTimeValidationEnabled),
            typeof(bool),
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(false));

        public bool IsSelectAllOnFocusEnabled
        {
            get { return (bool)GetValue(IsSelectAllOnFocusEnabledProperty); }
            set { SetValue(IsSelectAllOnFocusEnabledProperty, value); }
        }

        public static readonly DependencyProperty IsSelectAllOnFocusEnabledProperty = DependencyProperty.Register(
            nameof(IsSelectAllOnFocusEnabled),
            typeof(bool),
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(false));

        public Style TextBoxStyle
        {
            get { return (Style)GetValue(TextBoxStyleProperty); }
            set { SetValue(TextBoxStyleProperty, value); }
        }

        public static readonly DependencyProperty TextBoxStyleProperty = DependencyProperty.Register(
            nameof(TextBoxStyle), 
            typeof(Style), 
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(default(Style)));

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