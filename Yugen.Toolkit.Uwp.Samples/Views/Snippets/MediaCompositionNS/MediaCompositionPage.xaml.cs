using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Capture;
using Windows.Graphics.Imaging;
using Windows.Media.Audio;
using Windows.Media.Capture;
using Windows.Media.Core;
using Windows.Media.Editing;
using Windows.Media.MediaProperties;
using Windows.Media.Render;
using Windows.Media.Transcoding;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Samples.Views.Snippets.MediaCompositionNS
{
    public sealed partial class MediaCompositionPage : Page
    {
        private readonly MediaComposition _mediaComposition = null;
        private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        private Direct3D11CaptureFramePool _franePool = null;
        private GraphicsCaptureSession _session = null;
        private TimeSpan _timeSpan;
        private long performanceFrequency;
        private AudioGraph _audioGraph;
        private IStorageFile _audioFile;

        public MediaCompositionPage()
        {
            this.InitializeComponent();

            if (_mediaComposition == null)
            {
                _mediaComposition = new MediaComposition();
            }
        }

        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        public static extern bool QueryPerformanceFrequency(out long lpFrequency);

        public void UpdateSource()
        {
            MediaStreamSource streamSource = _mediaComposition.GeneratePreviewMediaStreamSource((int)mediaPlayerElement.ActualWidth, (int)mediaPlayerElement.ActualHeight);
            var source = MediaSource.CreateFromMediaStreamSource(streamSource);
            mediaPlayerElement.Source = source;
        }

        public async void CaptureAudio()
        {
            AudioGraphSettings audioGraphSettings = new AudioGraphSettings(AudioRenderCategory.Speech);
            var result = await AudioGraph.CreateAsync(audioGraphSettings);
            if (result.Status != AudioGraphCreationStatus.Success)
            {
                return;
            }
            _audioGraph = result.Graph;

            var deviceInputNodeResult = await _audioGraph.CreateDeviceInputNodeAsync(MediaCategory.Speech);
            if (deviceInputNodeResult.Status != AudioDeviceNodeCreationStatus.Success)
            {
                return;
            }
            var deviceInputNode = deviceInputNodeResult.DeviceInputNode;

            _audioFile = await ApplicationData.Current.TemporaryFolder
                    .CreateFileAsync("speech", CreationCollisionOption.ReplaceExisting);

            var mediaEncodingProfile = MediaEncodingProfile.CreateMp3(AudioEncodingQuality.High);
            var fileOutputNodeResult = await _audioGraph.CreateFileOutputNodeAsync(_audioFile, mediaEncodingProfile);
            if (fileOutputNodeResult.Status != AudioFileNodeCreationStatus.Success)
            {
                return;
            }
            var fileOutputNode = fileOutputNodeResult.FileOutputNode;

            deviceInputNode.AddOutgoingConnection(fileOutputNode);

            _audioGraph.Start();
        }

        private async void OnAddClipClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.VideosLibrary
            };
            fileOpenPicker.FileTypeFilter.Add(".mp4");
            var file = await fileOpenPicker.PickSingleFileAsync();
            if (file == null)
                return;
            MediaClip clip = await MediaClip.CreateFromFileAsync(file);
            _mediaComposition.Clips.Add(clip);

            UpdateSource();
        }

        private async void OnSaveClipClick(object sender, RoutedEventArgs e)
        {
            FileSavePicker fileSavePicker = new FileSavePicker();
            fileSavePicker.FileTypeChoices.Add("MP4 files", new List<string>() { ".mp4" });
            fileSavePicker.SuggestedStartLocation = PickerLocationId.VideosLibrary;

            var file = await fileSavePicker.PickSaveFileAsync();
            if (file == null)
            {
                return;
            }

            btnSave.Visibility = Visibility.Collapsed;
            progressRing.Visibility = Visibility.Visible;
            tbProgress.Visibility = Visibility.Visible;

            var render = _mediaComposition.RenderToFileAsync(file, MediaTrimmingPreference.Precise);
            render.Progress = new AsyncOperationProgressHandler<TranscodeFailureReason, double>((reson, value) =>
            {
                _synchronizationContext.Post((param) =>
                {
                    tbProgress.Text = value.ToString("0.00") + "%";
                }, null);
            });
            render.Completed = new AsyncOperationWithProgressCompletedHandler<TranscodeFailureReason, double>((reason, status) =>
            {
                string msg = "Successful";

                var re = reason.GetResults();
                if (re != TranscodeFailureReason.None || status != AsyncStatus.Completed)
                {
                    msg = "Unsuccessful";
                }
                _synchronizationContext.Post((param) =>
                {
                    btnSave.Visibility = Visibility.Visible;
                    progressRing.Visibility = Visibility.Collapsed;
                    tbProgress.Visibility = Visibility.Collapsed;
                    tbProgress.Text = msg;
                }, null);
            });
        }

        private async void OnAddAudioClick(object sender, RoutedEventArgs e)
        {
            // Add background audio
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.MusicLibrary
            };
            picker.FileTypeFilter.Add(".mp3");
            picker.FileTypeFilter.Add(".wav");
            picker.FileTypeFilter.Add(".flac");
            StorageFile audioFile = await picker.PickSingleFileAsync();
            if (audioFile == null)
            {
                return;
            }

            // These files could be picked from a location that we won't have access to later
            var storageItemAccessList = StorageApplicationPermissions.FutureAccessList;
            storageItemAccessList.Add(audioFile);

            var backgroundTrack = await BackgroundAudioTrack.CreateFromFileAsync(audioFile);

            _mediaComposition.BackgroundAudioTracks.Add(backgroundTrack);

            UpdateSource();
        }

        private void OnAddOverlayClick(object sender, RoutedEventArgs e)
        {
            var colorClip = MediaClip.CreateFromColor(Color.FromArgb(255, 125, 0, 0), TimeSpan.FromSeconds(10));

            var colorOverlay = new MediaOverlay(colorClip, new Rect(10, 10, 50, 50), 1);

            MediaOverlayLayer colorLayer = new MediaOverlayLayer();
            colorLayer.Overlays.Add(colorOverlay);

            _mediaComposition.OverlayLayers.Add(colorLayer);

            UpdateSource();
        }

        private async void OnCaptureScreenClick(object sender, RoutedEventArgs e)
        {
            GraphicsCapturePicker graphicsCapturePicker = new GraphicsCapturePicker();
            GraphicsCaptureItem graphicsCaptureItem = await graphicsCapturePicker.PickSingleItemAsync();

            if (graphicsCaptureItem == null)
                return;
            CanvasDevice canvasDevice = new CanvasDevice();
            _franePool = Direct3D11CaptureFramePool.Create(canvasDevice, Windows.Graphics.DirectX.DirectXPixelFormat.B8G8R8A8UIntNormalized, 2, graphicsCaptureItem.Size);

            _franePool.FrameArrived += (s, args) =>
            {
                using (var frame = _franePool.TryGetNextFrame())
                {
                    var canvasBitmap = CanvasBitmap.CreateFromDirect3D11Surface(canvasDevice, frame.Surface);

                    //await CpatureToImageFile(canvasBitmap);
                    CreateClipInMemory(canvasDevice, canvasBitmap);
                }
            };

            graphicsCaptureItem.Closed += (s, o) =>
            {
                OnCaptureStopClick(null, null);
            };

            _session = _franePool.CreateCaptureSession(graphicsCaptureItem);

            timeSpans = new List<TimeSpan>();
            count = 0;

            _session.StartCapture();

            QueryPerformanceCounter(out long qpc);
            QueryPerformanceFrequency(out long frq);
            performanceFrequency = frq;
            var milliseconds = 1000f * qpc / performanceFrequency;
            _timeSpan = TimeSpan.FromMilliseconds(milliseconds);

            CaptureAudio();

            btnCaptureStop.Visibility = Visibility.Visible;
            btnCaptureStart.Visibility = Visibility.Collapsed;
        }

        private async void OnCaptureStopClick(object sender, RoutedEventArgs e)
        {
            _session.Dispose();
            _franePool.Dispose();

            _audioGraph?.Stop();
            if (_audioFile != null)
            {
                var audioTrack = await BackgroundAudioTrack.CreateFromFileAsync(_audioFile);
                _mediaComposition.BackgroundAudioTracks.Add(audioTrack);
            }

            //await CreateClipFromImageFile();

            UpdateSource();

            btnCaptureStop.Visibility = Visibility.Collapsed;
            btnCaptureStart.Visibility = Visibility.Visible;
        }

        #region Capture screen and create MediaClip in Memory

        /// <summary>
        /// TODO: need to dispose CanvasRenderTarget.
        /// </summary>
        /// <param name="canvasDevice"></param>
        /// <param name="canvasBitmap"></param>
        private void CreateClipInMemory(CanvasDevice canvasDevice, CanvasBitmap canvasBitmap)
        {
            QueryPerformanceCounter(out long counter);
            var currentTime = TimeSpan.FromMilliseconds(1000f * counter / performanceFrequency);
            try
            {
                CanvasRenderTarget rendertarget = new CanvasRenderTarget(canvasDevice, canvasBitmap.SizeInPixels.Width, canvasBitmap.SizeInPixels.Height, canvasBitmap.Dpi, canvasBitmap.Format, canvasBitmap.AlphaMode);
                using (CanvasDrawingSession ds = rendertarget.CreateDrawingSession())
                {
                    ds.Clear(Colors.Transparent);
                    ds.DrawImage(canvasBitmap);
                }
                _mediaComposition.Clips.Add(MediaClip.CreateFromSurface(rendertarget, currentTime - _timeSpan));
            }
            catch
            {
            }
            finally
            {
                _timeSpan = currentTime;
            }
        }

        #endregion Capture screen and create MediaClip in Memory

        #region Capture screen and save it to disk.

        private int count = 0;
        private List<TimeSpan> timeSpans = new List<TimeSpan>();

        public async Task CreateClipFromImageFile()
        {
            for (int i = 0; i < count; i++)
            {
                string FileName = $"capture{i}.";
                FileName += "jpg";
                var file = await ApplicationData.Current.TemporaryFolder.GetFileAsync(FileName);

                _mediaComposition.Clips.Add(await MediaClip.CreateFromImageFileAsync(file, timeSpans[i]));
            }
        }

        private async Task CpatureToImageFile(CanvasBitmap WB)
        {
            try
            {
                string FileName = $"capture{count++}.";
                Guid BitmapEncoderGuid = BitmapEncoder.JpegEncoderId;
                FileName += "jpg";

                var file = await ApplicationData.Current.TemporaryFolder
                    .CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoderGuid, stream);
                    byte[] pixels = WB.GetPixelBytes();
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore,
                              (uint)WB.SizeInPixels.Width,
                              (uint)WB.SizeInPixels.Height,
                              96.0,
                              96.0,
                              pixels);
                    await encoder.FlushAsync();
                }

                QueryPerformanceCounter(out long qpc);
                var milliseconds = 1000f * qpc / performanceFrequency;
                var currentTime = TimeSpan.FromMilliseconds(milliseconds);

                timeSpans.Add(currentTime - _timeSpan);
                _timeSpan = currentTime;
            }
            catch
            {
            }
        }

        #endregion Capture screen and save it to disk.
    }
}