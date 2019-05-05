using System.Collections.Generic;
using System.Linq;
using AudioTools.Interface.DeviceDetection;
using NAudio.CoreAudioApi;

namespace AudioTools.DeviceDetection
{
    public class MMAudioDeviceDetector : IAudioDeviceDetector
    {
        public IEnumerable<IAudioDevice> GetAllDevices()
        {
            var mmDeviceEnumerator = new MMDeviceEnumerator();
            var mmDeviceCollection = mmDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);

            var devices = mmDeviceCollection.Select(device => new MMAudioDevice(device));
            return devices;
        }

        public IEnumerable<IAudioDevice> GetOutputDevices()
        {
            var mmDeviceEnumerator = new MMDeviceEnumerator();
            var mmDeviceCollection = mmDeviceEnumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            var devices = mmDeviceCollection.Select(device => new MMAudioDevice(device));
            return devices;
        }

        public IAudioDevice GetDefaultOutputDevice()
        {
            var mmDeviceEnumerator = new MMDeviceEnumerator();
            var defaultAudioEndpoint = mmDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            return new MMAudioDevice(defaultAudioEndpoint);
        }
    }
}