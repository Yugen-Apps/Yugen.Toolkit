using Windows.UI.Xaml.Controls;

namespace WaveFormSample.Uwp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();


            DataContext = new MainViewModel();
        }

        private MainViewModel ViewModel => (MainViewModel)DataContext;
    }
}
