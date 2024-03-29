﻿using System;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Storage;

namespace Yugen.Toolkit.Uwp.Audio.Services.Abstractions
{
    public interface IAudioPlaybackService
    {
        event EventHandler<TimeSpan> PositionChanged;
        event EventHandler<float> RmsChanged;

        TimeSpan NaturalDuration { get; }

        AudioFileInputNode MasterFileInput { get; }

        Task Initialize();

        Task LoadSong(StorageFile audioFile);

        Task LoadSong(byte[] audioBytes);

        void TogglePlay(bool isPaused);

        void ChangePitch(double pitch);

        void ChangeVolume(double volume, double fader);

        void IsHeadphones(bool isHeadphone);

        Task Scratch(bool isTouched, bool isClockwise, float crossProduct);

        void ChangeEq(int band, double gain);
    }
}