using System.Collections.Generic;
using System.Linq;
using AudioTools.Interface.DeviceDetection;
using NAudio.Wave;

namespace AudioTools.Devices
{
    public class WaveOutDeviceDetector : IAudioDeviceDetector
    {        
        private IEnumerable<WaveOutDevice> AllDevices { get; set; }

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

            // TODO: Think of better way, can't see API to get currently active device
            return AllDevices.First();
        }

        public void Refresh()
        {
            var allDevices = new List<WaveOutDevice>();
            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                var devCap = WaveOut.GetCapabilities(i);
                var waveOutDevice = new WaveOutDevice(devCap);
                allDevices.Add(waveOutDevice);
            }

            AllDevices = allDevices;
        }
    }
}