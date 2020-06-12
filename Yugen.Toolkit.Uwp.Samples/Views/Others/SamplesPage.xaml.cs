using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Others
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SamplesPage : Page
    {
        public SamplesPage()
        {
            this.InitializeComponent();
        }

        //public async Task RunTasks(WriteableBitmap clone)
        //{
        //    var tasks = new List<Task>();

        //    tasks.Add(Task.Run(() => DoWork(400, 1, clone)));
        //    tasks.Add(Task.Run(() => DoWork(200, 2, clone)));
        //    tasks.Add(Task.Run(() => DoWork(300, 3, clone)));

        //    await Task.WhenAll(tasks);
        //}

        //public async Task DoWork(int delay, int n, WriteableBitmap masterImageSource)
        //{
        //    await Task.Delay(delay);
        //    System.Diagnostics.Debug.WriteLine($"{n} {masterImageSource.PixelHeight}");
        //}
    }


    //    public static class SettingsStorageExtensions
    //    {
    //        private const string FileExtension = ".json";

    //        public static bool IsRoamingStorageAvailable(this ApplicationData appData)
    //        {
    //            return appData.RoamingStorageQuota == 0;
    //        }

    //        public static async Task SaveAsync<T>(this StorageFolder folder, string name, T content)
    //        {
    //            var file = await folder.CreateFileAsync(GetFileName(name), CreationCollisionOption.ReplaceExisting);
    //            var fileContent = await JsonProvider.StringifyAsync(content);

    //            await FileIO.WriteTextAsync(file, fileContent);
    //        }

    //        public static async Task<T> ReadAsync<T>(this StorageFolder folder, string name)
    //        {
    //            if (!File.Exists(Path.Combine(folder.Path, GetFileName(name))))
    //            {
    //                return default;
    //            }

    //            var file = await folder.GetFileAsync($"{name}.json");
    //            var fileContent = await FileIO.ReadTextAsync(file);

    //            return await JsonProvider.ToObjectAsync<T>(fileContent);
    //        }



    //        public static async Task<StorageFile> SaveFileAsync(this StorageFolder folder, byte[] content, string fileName, CreationCollisionOption options = CreationCollisionOption.ReplaceExisting)
    //        {
    //            if (content == null)
    //            {
    //                throw new ArgumentNullException(nameof(content));
    //            }

    //            if (string.IsNullOrEmpty(fileName))
    //            {
    //                throw new ArgumentException("ExceptionSettingsStorageExtensionsFileNameIsNullOrEmpty", nameof(fileName));
    //            }

    //            var storageFile = await folder.CreateFileAsync(fileName, options);
    //            await FileIO.WriteBytesAsync(storageFile, content);
    //            return storageFile;
    //        }

    //        public static async Task<byte[]> ReadFileAsync(this StorageFolder folder, string fileName)
    //        {
    //            var item = await folder.TryGetItemAsync(fileName).AsTask().ConfigureAwait(false);

    //            if (item != null && item.IsOfType(StorageItemTypes.File))
    //            {
    //                var storageFile = await folder.GetFileAsync(fileName);
    //                byte[] content = await storageFile.ReadBytesAsync();
    //                return content;
    //            }

    //            return null;
    //        }

    //        public static async Task<byte[]> ReadBytesAsync(this StorageFile file)
    //        {
    //            if (file != null)
    //            {
    //                using (IRandomAccessStream stream = await file.OpenReadAsync())
    //                {
    //                    using (var reader = new DataReader(stream.GetInputStreamAt(0)))
    //                    {
    //                        await reader.LoadAsync((uint)stream.Size);
    //                        var bytes = new byte[stream.Size];
    //                        reader.ReadBytes(bytes);
    //                        return bytes;
    //                    }
    //                }
    //            }

    //            return null;
    //        }

    //        private static string GetFileName(string name)
    //        {
    //            return string.Concat(name, FileExtension);
    //        }
    //    }



    //public static class MyTextConverter
    //{
    //    public static string Convert(int x) => x switch
    //    {
    //        0 => "Zero",
    //        1 => "Foo",
    //        2 => "Bar",
    //        _ => throw new ArgumentOutOfRangeException()
    //    };
    //}

    //<TextBlock
    //xmlns:converters="using:MyApp.Converters"
    //Text="{x:Bind converters:MyTextConverter.Convert(ViewModel.MyValue), Mode=OneWay}"/>


    //_ = System.Convert.ToDecimal("1.2345"); //new CultureInfo("en-US") //where the number separator is "."
    //        _ = System.Convert.ToDecimal("1.2345", new CultureInfo("it-IT")); //where the number separator is ","
    //        _ = System.Convert.ToDecimal("1.2345", CultureInfo.CurrentUICulture); //The above line returns my culture
    //        _ = System.Convert.ToDecimal("1.2345", CultureInfo.InvariantCulture); // http://stackoverflow.com/questions/9760237/what-does-cultureinfo-invariantculture-mean
    //        _ = System.Convert.ToDecimal("1,2345"); //new CultureInfo("en-US") //where the number separator is "."
    //        _ = System.Convert.ToDecimal("1,2345", new CultureInfo("it-IT")); //where the number separator is ","
    //        _ = System.Convert.ToDecimal("1,2345", CultureInfo.CurrentUICulture); //The above line returns my culture
    //        _ = System.Convert.ToDecimal("1,2345", CultureInfo.InvariantCulture); // http://stackoverflow.com/questions/9760237/what-does-cultureinfo-invariantculture-mean
      
}
