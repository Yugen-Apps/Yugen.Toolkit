using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Controls.Validation
{
    public partial class ValidatingComboBox
    {
        #region DependencyProperties
        /// <summary>
        /// Get or set the itemsSource
        /// </summary>
        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="ItemsSource"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(object),
            typeof(ValidatingComboBox),
            new PropertyMetadata(null));

        /// <summary>
        /// Get or set the selected item
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="SelectedItem"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(ValidatingComboBox),
            new PropertyMetadata(null));

        /// <summary>
        /// Get or set the Item DataTemplate
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        /// <summary>
        /// Identifies the <see cref="ItemTemplate"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(ValidatingComboBox),
            new PropertyMetadata(null));

        #endregion

        public ValidatingComboBox()
        {
            InitializeComponent();

            base.Init(ErrorMessageTextBlock, MyComboBox);
        }

        /// <summary>
        ///  Occurs when the currently selected item changes.
        /// </summary>
        public event SelectionChangedEventHandler MyComboBoxOnSelectionChanged;

        private void MyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            base.ResetCustomValidation();
            MyComboBoxOnSelectionChanged?.Invoke(sender, e);
        }
    }
}