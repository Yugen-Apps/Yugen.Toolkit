namespace Yugen.Toolkit.Uwp.Audio.Services.Abstractions
{
    public interface IAudioPlaybackServiceProvider
    {
        void Initialize();

        IAudioPlaybackService Get(Side side);
    }
}