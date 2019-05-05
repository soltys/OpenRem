using AudioTools.Interface.DeviceDetection;
using NAudio.CoreAudioApi;

namespace AudioTools.DeviceDetection
{
    public class MMAudioDevice : IAudioDevice
    {
        private readonly MMDevice _mmDevice;

        public MMAudioDevice(MMDevice mmDevice)
        {
            _mmDevice = mmDevice;
        }
        
        public string Name => _mmDevice.FriendlyName;

        public override string ToString()
        {
            return Name;
        }
    }
}