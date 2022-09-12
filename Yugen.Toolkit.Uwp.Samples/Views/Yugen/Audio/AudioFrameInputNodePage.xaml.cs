using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
using Yugen.Audio.Samples.ViewModels;
using Yugen.Toolkit.Uwp.Samples;

namespace Yugen.Audio.Samples.Views
{
    /// <summary>
    /// AudioFrameInputNodeDemo
    /// </summary>
    public sealed partial class AudioFrameInputNodePage : Page
    {
        public AudioFrameInputNodePage()
        {
            this.InitializeComponent();

            DataContext = App.Current.Services.GetService<AudioFrameInputNodeViewModel>();
        }

        private AudioFrameInputNodeViewModel ViewModel => (AudioFrameInputNodeViewModel)DataContext;
    }
}