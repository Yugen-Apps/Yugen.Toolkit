using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Common.Uwp.Controls
{
    public sealed partial class PageBarUserControl : UserControl
    {
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string), typeof(PageBarUserControl), new PropertyMetadata(string.Empty));

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register(nameof(ImageSource), typeof(string), typeof(PageBarUserControl), new PropertyMetadata(string.Empty));

        public PageBarUserControl()
        {
            this.InitializeComponent();
        }

        public void InitCommandBar(params ICommandBarElement[] commandBarElementList)
        {
            MainCommandBar.PrimaryCommands.Clear();
            foreach (var commandBarElement in commandBarElementList)
            {
                MainCommandBar.PrimaryCommands.Add(commandBarElement);
            }
        }
    }
}
