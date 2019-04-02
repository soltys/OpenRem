using System.Collections.Generic;

namespace OpenRem.Engine.OS
{
    internal interface IPnPDevice
    {
        IEnumerable<string> GetDevices();
    }
}