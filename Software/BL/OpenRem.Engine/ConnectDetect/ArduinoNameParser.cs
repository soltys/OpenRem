using System;
using System.Text.RegularExpressions;
using OpenRem.Arduino;

namespace OpenRem.Engine
{
    internal static class ArduinoNameParser
    {
        public static ArduinoType ToArduinoType(string name)
        {
            if (name == null)
            {
                return ArduinoType.MKRZERO;
            }

            Regex arduinoNameRegex = new Regex(@"Arduino (\w+)");

            var match = arduinoNameRegex.Match(name);

            if (match.Success)
            {
                return (ArduinoType) Enum.Parse(typeof(ArduinoType), match.Groups[1].Value);
            }
            else
            {
                return ArduinoType.MKRZERO;
            }
        }
    }
}