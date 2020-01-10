using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Controls.Menu
{
    public sealed partial class TitleImageCommandBar : UserControl
    {
        #region DependencyProperties

        /// <summary>
        /// Gets or sets a title
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="Title"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), 
            typeof(string), 
            typeof(TitleImageCommandBar), 
            new PropertyMetadata(string.Empty));

        /// <summary>
        /// Gets or sets an image
        /// </summary>
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        /// <summary>
        /// Identifies <see cref="ImageSource"/> dependency property.
        /// </summary>
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

        /// <summary>
        /// Initialize the command bar
        /// </summary>
        /// <param name="commandBarElementList"></param>
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
