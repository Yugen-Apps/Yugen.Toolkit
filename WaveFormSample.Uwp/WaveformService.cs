using System.Collections.Generic;
using System.IO;
using NAudio.Wave; 

namespace WaveFormSample.Uwp
{
    public class WaveformService
    {
        private IPeakProvider _peakProvider = new MaxPeakProvider();

        public WaveformService()
        {
        }

        public WaveformService(WaveformRendererSettings settings, IPeakProvider peakProvider)
        {
            Settings = settings;
            _peakProvider = peakProvider;
        }

        public WaveformRendererSettings Settings { get; } = new WaveformRendererSettings();

        public List<(float min, float max)> GenerateAudioData(Stream stream)
        {
            ISampleProvider isp;
            long samples;

            using (var reader = new StreamMediaFoundationReader(stream))
            {
                isp = reader.ToSampleProvider();
                float[] Buffer = new float[reader.Length / 2];
                isp.Read(Buffer, 0, Buffer.Length);

                int bytesPerSample = reader.WaveFormat.BitsPerSample / 8;
                samples = reader.Length / bytesPerSample;

                //int sampleRate = isp.WaveFormat.SampleRate;
                //double totalMinutes = reader.TotalTime.TotalMinutes;
            }

            return GenerateAudioData(isp, samples);
        }

        public List<(float min, float max)> GenerateAudioData(byte[] audioBytes) => throw new System.NotImplementedException();

        private List<(float min, float max)> GenerateAudioData(ISampleProvider isp, long samples)
        {
            var samplesPerPixel = (int)(samples / Settings.Width);
            var stepSize = Settings.PixelsPerPeak + Settings.SpacerPixels;
            _peakProvider.Init(isp, samplesPerPixel * stepSize);

            // DecibelScale - if true, convert values to decibels for a logarithmic waveform
            if (Settings.DecibelScale)
            {
                _peakProvider = new DecibelPeakProvider(_peakProvider, 48);
            }

            var peakList = new List<(float min, float max)>();
            for (int i = 0; i < Settings.Width; i++)
            {
                var peak = _peakProvider.GetNextPeak();
                //System.Diagnostics.Debug.WriteLine($"{peak.Min} , {peak.Max}");
                peakList.Add((peak.Min, peak.Max));
            }
            return peakList;
        }
    }
}