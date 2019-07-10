using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpCommunity.Uwp.Controls.Validation
{
    public partial class ValidatingComboBoxUserControl
    {
        #region DependencyProperties

        public object ItemsSource
        {
            get { return GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(object),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata(null));

        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata(null));

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(ValidatingComboBoxUserControl),
            new PropertyMetadata(null));

        #endregion

        public ValidatingComboBoxUserControl()
        {
            InitializeComponent();

            base.Init(ErrorMessage, MyComboBox);
        }
        
        public event SelectionChangedEventHandler MyComboBoxOnSelectionChanged;

        private void MyComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            base.ResetCustomValidation();
            MyComboBoxOnSelectionChanged?.Invoke(sender, e);
        }
    }
}