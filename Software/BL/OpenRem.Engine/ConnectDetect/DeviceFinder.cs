using System.Collections.Generic;
using System.Text.RegularExpressions;
using OpenRem.Engine.OS;

namespace OpenRem.Engine
{
    internal class DeviceFinder : IDeviceFinder
    {
        private readonly IPnPDevice pnpDevice;

        public DeviceFinder(IPnPDevice pnpDevice)
        {
            this.pnpDevice = pnpDevice;
        }

        public IEnumerable<ArduinoDevice> GetArduinoDevices()
        {
            foreach (var deviceName in this.pnpDevice.GetDevices())
            {
                if (!deviceName.ToUpperInvariant().Contains("ARDUINO"))
                {
                    continue;
                }

                Regex nameRegex = new Regex(@"([\w ]+) \((COM\d+)\)");
                var match = nameRegex.Match(deviceName);
                if (match.Success)
                {
                    yield return
                        new ArduinoDevice
                        {
                            Name = match.Groups[1].Value,
                            ComPort = match.Groups[2].Value
                        };
                }
            }
        }
    }
}
