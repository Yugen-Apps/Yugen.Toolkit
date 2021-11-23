using System;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views
{
    /// <summary>
    /// HomePage
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private async void OnLoadButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.Downloads,
            };
            picker.FileTypeFilter.Add("*");

            var file = await picker.PickSingleFileAsync();
            
            byte[] bytes;
            if (file != null)
            {
                using (IRandomAccessStream stream = await file.OpenReadAsync())
                {
                    using (var reader = new DataReader(stream.GetInputStreamAt(0)))
                    {
                        await reader.LoadAsync((uint)stream.Size);
                        bytes = new byte[stream.Size];
                        reader.ReadBytes(bytes);
                    }
                }
            }
        //    bytes = null;
        }

        // using Microsoft.Toolkit.Uwp.Helpers;
        // file.ReadBytesAsync

        //private async void OnLoadButtonClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    FileOpenPicker picker = new FileOpenPicker
        //    {
        //        SuggestedStartLocation = PickerLocationId.Downloads,
        //    };
        //    picker.FileTypeFilter.Add("*");

        //    var file = await picker.PickSingleFileAsync();
        //    if (file != null)
        //    {
        //        
        //        using (Stream stream = await file.OpenStreamForReadAsync())
        //        {
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                stream.CopyTo(ms);
        //                bytes = ms.ToArray();
        //            }
        //        }
        //    }
        //}
    }
}