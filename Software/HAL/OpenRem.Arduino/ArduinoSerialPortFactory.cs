using System.IO.Ports;

namespace OpenRem.Arduino
{
    public static class ArduinoSerialPortFactory
    {
        public static SerialPort Create(string comPort, ArduinoType arduinoType)
        {
            // Arduino default settings
            var baudRate = 9600;
            var parity = Parity.None;
            var dataBits = 8;
            var stopBits = StopBits.One;

            var serialPort = new SerialPort(comPort, baudRate, parity, dataBits, stopBits);

            if (arduinoType == ArduinoType.Leonardo)
            {
                serialPort.DtrEnable = true;
                serialPort.RtsEnable = true;
            }

            return serialPort;
        }
    }
}
