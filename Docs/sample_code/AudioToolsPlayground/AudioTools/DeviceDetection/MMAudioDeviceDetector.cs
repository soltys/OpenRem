using System.Collections.Generic;
using System.Linq;
using AudioTools.Interface.DeviceDetection;
using NAudio.CoreAudioApi;

namespace AudioTools.DeviceDetection
{
    public class MMAudioDeviceDetector : IAudioDeviceDetector
    {
        public MMAudioDeviceDetector()
        {
            Refresh();
        }

        private IEnumerable<MMAudioDevice> AllDevices { get; set; }

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

            var defaultAudioEndpoint = new MMDeviceEnumerator().GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            return AllDevices.First(device => device.Id == defaultAudioEndpoint.ID);
        }

        public void Refresh()
        {
            var mmDeviceCollection = new MMDeviceEnumerator().EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);

            AllDevices = mmDeviceCollection.Select(device => new MMAudioDevice(device)).ToList();
        }
    }
}