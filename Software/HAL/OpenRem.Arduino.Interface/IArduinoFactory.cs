using OpenRem.Common;

namespace OpenRem.Arduino
{
    public interface IArduinoFactory
    {
        IDataStream Create(string comPort, ArduinoType arduinoType);
    }
}