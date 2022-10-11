using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Media.Devices;

namespace Yugen.Toolkit.Uwp.Audio.Services.Common.Helpers
{
    public static class AudioDevicesHelper
    {
        public static DeviceInformation MasterAudioDeviceInformation;
        public static DeviceInformation HeadphonesAudioDeviceInformation;
        public static DeviceInformationCollection DeviceInformationCollection;

        public static async Task Initialize()
        {
            var defaultAudioDeviceId = MediaDevice.GetDefaultAudioRenderId(AudioDeviceRole.Default);
            DeviceInformationCollection = await DeviceInformation.FindAllAsync(DeviceClass.AudioRender);

            MasterAudioDeviceInformation = DeviceInformationCollection.FirstOrDefault(
                x => x.Id.Equals(defaultAudioDeviceId));

            HeadphonesAudioDeviceInformation = DeviceInformationCollection.FirstOrDefault(
                x => !x.Id.Equals(defaultAudioDeviceId));
        }
    }
}