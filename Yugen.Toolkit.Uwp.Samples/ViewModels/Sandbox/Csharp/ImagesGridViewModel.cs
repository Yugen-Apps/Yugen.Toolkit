using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Yugen.Toolkit.Uwp.Helpers;
using Yugen.Toolkit.Uwp.Samples.Models;

namespace Yugen.Toolkit.Uwp.Samples.ViewModels.Sandbox.Csharp
{
    public class ImagesGridViewModel : ObservableObject
    {
        public ObservableCollection<Person> ImageCollection = new ObservableCollection<Person>();
        public ObservableCollection<Person> Image5Collection = new ObservableCollection<Person>();

        private string _image = "ms-appx:///";
        private ImageSource _imageSource;
        private Person _selectedPerson;

        public ImagesGridViewModel()
        {
            StartBitmapImageAsyncCommand = new AsyncRelayCommand(StartBitmapImageAsyncCommandBehavior);
            StartGetThumbnailAsyncAsyncCommand = new AsyncRelayCommand(StartGetThumbnailAsyncAsyncCommandBehavior);
            StartSoftwareBitmapSourceAsyncCommand = new AsyncRelayCommand(StartSoftwareBitmapSourceAsyncCommandBehavior);
            StartWriteableBitmapAsyncCommand = new AsyncRelayCommand(StartWriteableBitmapAsyncCommandBehavior);
            StartStorageFilePathAsyncCommand = new AsyncRelayCommand(StartStorageFilePathAsyncCommandBehavior);
            //StartLoadImageAsyncCommand = new RelayCommand(() => Image = "ms-appx:///Assets/LargeTile.scale-400.png");
        }

        public IAsyncRelayCommand StartBitmapImageAsyncCommand { get; }

        public IAsyncRelayCommand StartGetThumbnailAsyncAsyncCommand { get; }

        public IAsyncRelayCommand StartSoftwareBitmapSourceAsyncCommand { get; }

        public IAsyncRelayCommand StartWriteableBitmapAsyncCommand { get; }

        public IAsyncRelayCommand StartStorageFilePathAsyncCommand { get; }

        //public IRelayCommand StartLoadImageAsyncCommand { get; }

        //public string Image
        //{
        //    get => _image;
        //    set => SetProperty(ref _image, value);
        //}

        //public ImageSource ImageSource
        //{
        //    get => _imageSource;
        //    set => SetProperty(ref _imageSource, value);
        //}

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                if (value != null)
                {
                    if (SetProperty(ref _selectedPerson, value))
                    {
                        //if (_selectedPerson.ImagePath != null)
                        //{
                        //    Image = _selectedPerson.ImagePath;
                        //}

                        SetSelectedPersonImageSource(_selectedPerson.FaToken);
                    }
                }
            }
        }

        private async void SetSelectedPersonImageSource(string faToken)
        {
            if (faToken != null)
            {
                StorageFile file = await StorageApplicationPermissions.FutureAccessList.GetFileAsync(faToken);

                BitmapImage bitmapImage = new BitmapImage();
                using (StorageItemThumbnail thumbnail = await file.GetThumbnailAsync(
                    ThumbnailMode.SingleItem, 200, ThumbnailOptions.None))
                {
                    await bitmapImage.SetSourceAsync(thumbnail);
                }

                //ImageSource = bitmapImage;

                ContentDialog dialog = new ContentDialog
                {
                    Title = "Image",
                    Content = new Image
                    {
                        Source = bitmapImage
                    },
                    CloseButtonText = "Ok"
                };

                ContentDialogResult result = await dialog.ShowAsync();
            }
        }

        // OK 300MB - OK 150MB
        private async Task StartBitmapImageAsyncCommandBehavior()
        {
            var list = await FilePickerHelper.OpenFiles();

            foreach (var storageFile in list)
            {
                var bitmap = new BitmapImage()
                {
                    // 150mb
                    DecodePixelHeight = 200,
                    DecodePixelType = DecodePixelType.Logical
                };

                using (var stream = await storageFile.OpenReadAsync())
                {
                    await bitmap.SetSourceAsync(stream);
                }

                ImageCollection.Add(new Person
                {
                    Name = storageFile.FolderRelativeId,
                    ImageSource = bitmap
                });
            }
        }

        // OK 10MB
        private async Task StartGetThumbnailAsyncAsyncCommandBehavior()
        {
            StorageApplicationPermissions.FutureAccessList.Clear();

            var list = await FilePickerHelper.OpenFiles();

            foreach (var storageFile in list)
            {
                string faToken = StorageApplicationPermissions.FutureAccessList.Add(storageFile);

                BitmapImage bitmapImage = new BitmapImage();
                using (StorageItemThumbnail thumbnail = await storageFile.GetThumbnailAsync(
                       ThumbnailMode.SingleItem, 200, ThumbnailOptions.None))
                {
                    await bitmapImage.SetSourceAsync(thumbnail);
                }

                IRandomAccessStream randomAccessStream = await storageFile.OpenAsync(FileAccessMode.Read);

                ImageCollection.Add(new Person
                {
                    Name = storageFile.FolderRelativeId,
                    ImageSource = bitmapImage,
                    FaToken = faToken,
                    RandomAccessStream = randomAccessStream,
                    StorageFile = storageFile
                });
            }
        }

        // 30MB - OK 1GB
        private async Task StartSoftwareBitmapSourceAsyncCommandBehavior()
        {
            var list = await FilePickerHelper.OpenFiles();

            foreach (var storageFile in list)
            {
                var source = new SoftwareBitmapSource();

                using (var stream = await storageFile.OpenAsync(FileAccessMode.Read))
                {
                    // Create the decoder from the stream
                    BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

                    // Get the SoftwareBitmap representation of the file
                    var softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                    //1GB
                    //if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
                    //    softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
                    //{
                    //    softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
                    //}

                    //await source.SetBitmapAsync(softwareBitmap);
                }

                ImageCollection.Add(new Person
                {
                    Name = storageFile.FolderRelativeId,
                    ImageSource = source
                });
            }
        }

        // OK 1.5GB
        private async Task StartWriteableBitmapAsyncCommandBehavior()
        {
            var list = await FilePickerHelper.OpenFiles();

            foreach (var storageFile in list)
            {
                var writeableBitmap = new WriteableBitmap(200, 200);

                using (var stream = await storageFile.OpenReadAsync())
                {
                    await writeableBitmap.SetSourceAsync(stream);
                }

                ImageCollection.Add(new Person
                {
                    Name = storageFile.FolderRelativeId,
                    ImageSource = writeableBitmap
                });
            }
        }

        // OK 150MB
        private async Task StartStorageFilePathAsyncCommandBehavior()
        {
            var list = await FilePickerHelper.OpenFiles();

            foreach (var storageFile in list)
            {
                StorageFolder tempFolder = ApplicationData.Current.TemporaryFolder;
                StorageFile copiedFile = await storageFile.CopyAsync(tempFolder, storageFile.Name, NameCollisionOption.ReplaceExisting);
                var fileFromTemp = await ApplicationData.Current.TemporaryFolder.GetFileAsync(copiedFile.Name);

                Image5Collection.Add(new Person
                {
                    Name = fileFromTemp.FolderRelativeId,
                    ImagePath = fileFromTemp.Path
                });
            }
        }

        // KO
        private async Task StartAsyncCommandBehavior()
        {
            var list = await FilePickerHelper.OpenFiles();

            foreach (var storageFile in list)
            {
                ImageCollection.Add(new Person
                {
                    Name = storageFile.FolderRelativeId,
                    ImagePath = storageFile.Path,
                    ImageSource = new BitmapImage(new Uri(storageFile.Path))
                });
            }
        }
    }
}