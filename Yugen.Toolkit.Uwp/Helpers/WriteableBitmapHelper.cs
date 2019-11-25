//using System;
//using System.IO;
//using System.Runtime.InteropServices.WindowsRuntime;
//using System.Threading.Tasks;
//using Windows.Graphics.Imaging;
//using Windows.Storage;
//using Windows.Storage.Streams;
//using Windows.UI.Xaml.Media.Imaging;

//namespace Yugen.Mosaic.Uwp.Helpers
//{
//    public static class WriteableBitmapHelper
//    {
//        //"ms-appx:///Assets/Images/1.png",
//        public static async Task<WriteableBitmap> LoadBitmap(string path)
//        {
//            var imageUri = new Uri(path);
//            var bmp = await BitmapFactory.FromContent(imageUri);
//            return bmp;
//        }
        
//        public static async Task<StorageFile> WriteableBitmapToStorageFile(StorageFile file, WriteableBitmap WB, FileFormat fileFormat)
//        {
//            Guid BitmapEncoderGuid = BitmapEncoder.JpegEncoderId;

//            switch (fileFormat)
//            {
//                case FileFormat.Jpg:
//                    BitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
//                    break;

//                case FileFormat.Png:
//                    BitmapEncoderGuid = BitmapEncoder.PngEncoderId;
//                    break;

//                case FileFormat.Bmp:
//                    BitmapEncoderGuid = BitmapEncoder.BmpEncoderId;
//                    break;

//                case FileFormat.Tiff:
//                    BitmapEncoderGuid = BitmapEncoder.TiffEncoderId;
//                    break;

//                case FileFormat.Gif:
//                    BitmapEncoderGuid = BitmapEncoder.GifEncoderId;
//                    break;
//            }

//            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
//            {
//                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoderGuid, stream);
//                Stream pixelStream = WB.PixelBuffer.AsStream();
//                byte[] pixels = new byte[pixelStream.Length];
//                await pixelStream.ReadAsync(pixels, 0, pixels.Length);

//                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
//                                    (uint)WB.PixelWidth,
//                                    (uint)WB.PixelHeight,
//                                    96.0,
//                                    96.0,
//                                    pixels);
//                await encoder.FlushAsync();
//            }

//            return file;
//        }

//        public static async Task<WriteableBitmap> ImageToWriteableBitmap(Image<Rgba32> masterImage)
//        {
//            InMemoryRandomAccessStream outputStream = new InMemoryRandomAccessStream();
//            masterImage.SaveAsBmp(outputStream.AsStreamForWrite());
//            return await BitmapFactory.FromStream(outputStream);
//        }
//    }
//}
