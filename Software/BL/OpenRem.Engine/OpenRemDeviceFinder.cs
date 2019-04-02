using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OpenRem.Engine.OS;

namespace OpenRem.Engine
{
    internal class OpenRemDeviceFinder
    {
        private IPnPDevice pnpDevice;

        public OpenRemDeviceFinder(IPnPDevice pnpDevice)
        {
            this.pnpDevice = pnpDevice;
        }

        public ArduinoDevice[] GetPossibleArduinoDevices()
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
