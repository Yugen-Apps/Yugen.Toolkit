using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media;

namespace Yugen.Toolkit.Uwp.Samples.Models
{
    public class Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string ImagePath { get; set; }

        public string FaToken { get; set; }

        public ImageSource ImageSource { get; set; }

        public SoftwareBitmap SoftwareBitmap { get; set; }

        public IRandomAccessStream RandomAccessStream { get; set; }

        public StorageFile StorageFile { get; set; }
    }
}