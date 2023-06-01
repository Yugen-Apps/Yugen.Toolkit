using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Storage.Pickers;

namespace WaveFormSample.Uwp
{
    public class MainViewModel : ObservableObject
    {
        private readonly WaveformService _waveformService = new WaveformService();
        private List<(float min, float max)> _peakList;

        public MainViewModel()
        {
            OpenCommand = new AsyncRelayCommand(OpenCommandBehavior);
        }

        public ICommand OpenCommand { get; }

        public List<(float min, float max)> PeakList
        {
            get => _peakList;
            set => SetProperty(ref _peakList, value);
        }

        private async Task OpenCommandBehavior()
        {
            var audioFile = await FilePickerHelper.OpenFile(
                new List<string> { ".mp3" },
                PickerLocationId.MusicLibrary
            );

            if (audioFile != null)
            {
                var audioStream = await audioFile.OpenStreamForReadAsync();
                await NaudioGenerateAudioData(audioStream);
            }
        }

        private async Task NaudioGenerateAudioData(Stream audioStream)
        {
            List<(float min, float max)> peakList = null;

            await Task.Run(() => { peakList = _waveformService.GenerateAudioData(audioStream); });

            PeakList = peakList;
        }
    }
}

    