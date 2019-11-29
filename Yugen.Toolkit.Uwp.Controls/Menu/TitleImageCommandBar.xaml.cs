using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Controls.Menu
{
    public sealed partial class TitleImageCommandBar : UserControl
    {
        #region DependencyProperties

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), 
            typeof(string), 
            typeof(TitleImageCommandBar), 
            new PropertyMetadata(string.Empty));

        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
            nameof(ImageSource), 
            typeof(string), 
            typeof(TitleImageCommandBar), 
            new PropertyMetadata(string.Empty));

        #endregion

        public TitleImageCommandBar()
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
