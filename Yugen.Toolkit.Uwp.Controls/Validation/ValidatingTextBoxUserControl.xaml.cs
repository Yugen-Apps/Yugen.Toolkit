using System;
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
            set 
            {
                if (IsRealTimeRuleValidationMet(value))
                    SetValue(TextProperty, value);
            }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ValidatingTextBoxUserControl),
            new PropertyMetadata(string.Empty));

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
        
        private bool IsRealTimeRuleValidationMet(string value)
        {
            if (!IsRealTimeValidationEnabled) 
                return true;
            if (ValidationRules == null) 
                return true;

            bool isValid = true;

            foreach (var rule in ValidationRules.Rules.TakeWhile(rule => isValid))
            {
                isValid = rule.IsValid(value);
            }

            return isValid;
        }


        public event TypedEventHandler<TextBox, TextBoxTextChangingEventArgs> MyTextBoxOnTextChanging;

        private void MyTextBox_OnTextChanging(TextBox sender, TextBoxTextChangingEventArgs e)
        {
            MyTextBoxOnTextChanging?.Invoke(sender, e);
        }

        public event TextChangedEventHandler MyTextBoxOnTextChanged;

        private void MyTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            base.ResetCustomValidation();
            MyTextBoxOnTextChanged?.Invoke(sender, e);

            if (MyTextBox.Text != Text)
            {
                var selectionStart = MyTextBox.SelectionStart - 1;
                MyTextBox.Text = Text;
                if (selectionStart > -1)
                    MyTextBox.SelectionStart = selectionStart;
            }
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