using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Audio.Samples.ViewModels;
using Yugen.Toolkit.Uwp.Samples;

namespace Yugen.Audio.Samples.Views
{
    public sealed partial class VinylPage : Page
    {
        public VinylPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<VinylViewModel>();
        }

        private VinylViewModel ViewModel => (VinylViewModel)DataContext;
    }
}