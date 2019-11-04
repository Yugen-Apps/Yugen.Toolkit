using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Helpers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Helpers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FilePickerPage : Page
    {
        public FilePickerPage()
        {
            this.InitializeComponent();
        }

        private async void Open_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await FilePickerHelper.OpenFile(".jpg");
        }

        private async void Open2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await FilePickerHelper.OpenFile(new List<string> { ".jpg", ".png" });
        }

        private async void Save_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await FilePickerHelper.SaveFile("filename", "Image", ".jpg");
        }

        private async void Save2_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await FilePickerHelper.SaveFile("filename", new Dictionary<string, List<string>>() { { "Image", new List<string>() { ".jpg", ".png" } } });
        }
    }
}
