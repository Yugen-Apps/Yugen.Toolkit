using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Common.Uwp.Providers
{
    public static class ImageProvider
    {
        public static async Task<WriteableBitmap> GetPictureAsync(string fileName, string folderName)
        {
            StorageFolder pictureFolder = await ApplicationData.Current.LocalFolder.GetFolderAsync(folderName);
            StorageFile pictureFile = await pictureFolder.GetFileAsync(fileName + ".jpg");

            using (IRandomAccessStream stream = await pictureFile.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                WriteableBitmap bmp = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);

                await bmp.SetSourceAsync(stream);

                return bmp;
            }
        }

        public static async Task SaveBitmapToFileAsync(WriteableBitmap image, string fileName, string folderName)
        {
            StorageFolder pictureFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
            var file = await pictureFolder.CreateFileAsync(fileName + ".jpg", CreationCollisionOption.ReplaceExisting);

            using (var stream = await file.OpenStreamForWriteAsync())
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream.AsRandomAccessStream());
                var pixelStream = image.PixelBuffer.AsStream();
                byte[] pixels = new byte[image.PixelBuffer.Length];

                await pixelStream.ReadAsync(pixels, 0, pixels.Length);

                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)image.PixelWidth, (uint)image.PixelHeight, 96, 96, pixels);

                await encoder.FlushAsync();
            }
        }
    }
}