using AudioTools.Interface.DeviceDetection;
using NAudio.CoreAudioApi;

namespace AudioTools.DeviceDetection
{
    public class MmAudioDevice : IAudioDevice
    {
        private readonly MMDevice _mmDevice;

        public MmAudioDevice(MMDevice mmDevice)
        {
            _mmDevice = mmDevice;
        }
        
        public string Name => _mmDevice.FriendlyName;
        
        public DeviceType DeviceType => (DeviceType)_mmDevice.DataFlow;
        
        public string Id => _mmDevice.ID;

        public override string ToString()
        {
            return Name;
        }
    }
}