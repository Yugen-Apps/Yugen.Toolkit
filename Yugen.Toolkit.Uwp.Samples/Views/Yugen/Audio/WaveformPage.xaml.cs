using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Audio.Samples.ViewModels;
using Yugen.Toolkit.Uwp.Samples;

namespace Yugen.Audio.Samples.Views
{
    public sealed partial class WaveformPage : Page
    {
        public WaveformPage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<WaveformViewModel>();
        }

        private WaveformViewModel ViewModel => (WaveformViewModel)DataContext;
    }
}