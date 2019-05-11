using System.Collections.Generic;
using System.Linq;
using AudioTools.Interface.DeviceDetection;
using NAudio.CoreAudioApi;

namespace AudioTools.Devices
{
    public class MmAudioDeviceDetector : IAudioDeviceDetector
    {
        private MMDeviceEnumerator _deviceEnumerator;
        private MMDeviceEnumerator DeviceEnumerator => _deviceEnumerator ?? (_deviceEnumerator = new MMDeviceEnumerator());
        private IEnumerable<MmAudioDevice> AllDevices { get; set; }

        public MmAudioDeviceDetector()
        {
            Refresh();
        }

        public IEnumerable<IAudioDevice> GetAllDevices()
        {
            if (AllDevices == null)
                Refresh();

            return AllDevices;
        }

        public IEnumerable<IAudioDevice> GetOutputDevices()
        {
            if (AllDevices == null)
                Refresh();

            var devices = AllDevices.Where(device => device.DeviceType == DeviceType.Output).ToList();
            return devices;
        }

        public IAudioDevice GetDeviceSelectedInSystem()
        {
            if (AllDevices == null)
                Refresh();

            var defaultAudioEndpoint = DeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            return AllDevices.First(device => device.Id == defaultAudioEndpoint.ID);
        }

        public void Refresh()
        {
            var mmDeviceCollection = DeviceEnumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);

            AllDevices = mmDeviceCollection.Select(device => new MmAudioDevice(device)).ToList();
        }
    }
}