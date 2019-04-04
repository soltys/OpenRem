using System.Collections.Generic;

namespace OpenRem.Engine
{
    internal interface IDeviceFinder
    {
        IEnumerable<ArduinoDevice> GetArduinoDevices();
    }
}