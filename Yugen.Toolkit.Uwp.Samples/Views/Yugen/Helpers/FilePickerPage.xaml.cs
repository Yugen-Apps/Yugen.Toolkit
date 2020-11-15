using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Helpers;

namespace Yugen.Toolkit.Uwp.Samples.Views.Helpers
{
    public sealed partial class FilePickerPage : Page
    {
        public FilePickerPage()
        {
            this.InitializeComponent();
        }

        private async void Open_Click(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            await FilePickerHelper.OpenFile();
        }

        private async void Open2_Click(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            await FilePickerHelper.OpenFile(".jpg");
        }

        private async void Open3_Click(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            await FilePickerHelper.OpenFile(new List<string> { ".jpg", ".png" });
        }

        private async void Save_Click(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            await FilePickerHelper.SaveFile("filename", "Image", ".jpg");
        }

        private async void Save2_Click(object _1, Windows.UI.Xaml.RoutedEventArgs _2)
        {
            await FilePickerHelper.SaveFile("filename", new Dictionary<string, List<string>>() { { "Image", new List<string>() { ".jpg", ".png" } } });
        }
    }
}