using NAudio.Wave;

namespace WaveFormSample.Uwp
{
    public interface IPeakProvider
    {
        void Init(ISampleProvider reader, int samplesPerPixel);

        PeakInfo GetNextPeak();
    }
}