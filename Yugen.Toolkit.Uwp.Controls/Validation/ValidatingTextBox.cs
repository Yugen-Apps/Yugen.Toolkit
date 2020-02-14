using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Controls.Validation
{
    public sealed partial class ValidatingTextBox
    {
        #region DependencyProperties

        /// <summary>
        /// Get or set text
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                if (IsRealTimeRuleValidationMet(value))
                    SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Text"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(ValidatingTextBox),
            new PropertyMetadata(string.Empty));

        /// <summary>
        /// Get or set if the spellcheck is enabled
        /// </summary>
        public bool IsSpellCheckEnabled
        {
            get { return (bool)GetValue(IsSpellCheckEnabledProperty); }
            set { SetValue(IsSpellCheckEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="IsSpellCheckEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsSpellCheckEnabledProperty = DependencyProperty.Register(
            nameof(IsSpellCheckEnabled),
            typeof(bool),
            typeof(ValidatingTextBox),
            new PropertyMetadata(true));

        /// <summary>
        /// Get or set the Input Scope
        /// </summary>
        public string InputScope
        {
            get { return (string)GetValue(InputScopeProperty); }
            set { SetValue(InputScopeProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="InputScope"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty InputScopeProperty =
            DependencyProperty.Register(
                nameof(InputScope),
                typeof(string),
                typeof(ValidatingTextBox),
                new PropertyMetadata("Default"));

        public bool IsRealTimeValidationEnabled
        {
            get { return (bool)GetValue(IsRealTimeValidationEnabledProperty); }
            set { SetValue(IsRealTimeValidationEnabledProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="IsRealTimeValidationEnabled"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsRealTimeValidationEnabledProperty = DependencyProperty.Register(
            nameof(IsRealTimeValidationEnabled),
            typeof(bool),
            typeof(ValidatingTextBox),
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

        /// <summary>
        /// Get or set the style
        /// </summary>
        public Style TextBoxStyle
        {
            get { return (Style)GetValue(TextBoxStyleProperty); }
            set { SetValue(TextBoxStyleProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="TextBoxStyle"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TextBoxStyleProperty = DependencyProperty.Register(
            nameof(TextBoxStyle),
            typeof(Style),
            typeof(ValidatingTextBox),
            new PropertyMetadata(default(Style)));

        #endregion

        public ValidatingTextBox()
        {
            InitializeComponent();

            base.Init(ErrorMessageTextBlock, MyTextBox);
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

        /// <summary>
        ///  Occurs when the currently text is changing.
        /// </summary>
        public event TypedEventHandler<TextBox, TextBoxTextChangingEventArgs> MyTextBoxOnTextChanging;

        private void MyTextBox_OnTextChanging(TextBox sender, TextBoxTextChangingEventArgs e)
        {
            MyTextBoxOnTextChanging?.Invoke(sender, e);
        }

        /// <summary>
        ///  Occurs when the currently text is changed.
        /// </summary>
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