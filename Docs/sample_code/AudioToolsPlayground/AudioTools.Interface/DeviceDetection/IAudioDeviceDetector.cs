using System.Collections.Generic;

namespace AudioTools.Interface.DeviceDetection
{
    public interface IAudioDeviceDetector
    {
        IEnumerable<IAudioDevice> GetAllDevices();

        IEnumerable<IAudioDevice> GetOutputDevices();

        IAudioDevice GetDefaultOutputDevice();

        void Refresh();
    }
}