using OpenRem.Common;

namespace OpenRem.Arduino.Interface
{
    public interface IArduinoFactory
    {
        IDataStream Create(string comPort, ArduinoType arduinoType);
    }
}