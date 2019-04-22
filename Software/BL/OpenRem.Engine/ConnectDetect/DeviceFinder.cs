using System.Collections.Generic;
using System.Text.RegularExpressions;
using OpenRem.Engine.OS;

namespace OpenRem.Engine
{
    internal class DeviceFinder : IDeviceFinder
    {
        private IPnPDevice pnpDevice;

        public DeviceFinder(IPnPDevice pnpDevice)
        {
            this.pnpDevice = pnpDevice;
        }

        public ArduinoDevice[] GetArduinoDevices()
        {
            var deviceList = this.pnpDevice.GetDevices();

            List<ArduinoDevice> recognizedDevices = new List<ArduinoDevice>();

            foreach (var deviceName in deviceList)
            {
                if (!deviceName.ToUpperInvariant().Contains("ARDUINO"))
                {
                    continue;
                }

                Regex nameRegex = new Regex(@"([\w ]+) \((COM\d+)\)");
                var match = nameRegex.Match(deviceName);
                if (match.Success)
                {
                    recognizedDevices.Add(
                        new ArduinoDevice
                        {
                            Name = match.Groups[1].Value,
                            ComPort = match.Groups[2].Value
                        }
                    );
                }
            }

            return recognizedDevices.ToArray();
        }
    }
}