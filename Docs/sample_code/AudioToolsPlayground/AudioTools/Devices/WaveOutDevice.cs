using AudioTools.Interface.DeviceDetection;
using NAudio.Wave;

namespace AudioTools.Devices
{
    public class WaveOutDevice : IAudioDevice
    {
        public WaveOutDevice(WaveOutCapabilities capabilities)
        {
            Name = capabilities.ProductName;
            DeviceType = DeviceType.Output; // TODO: handle properly
            Id = capabilities.ProductGuid.ToString();
        }
        
        public string Name { get; }
        public DeviceType DeviceType { get; }
        public string Id { get; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}