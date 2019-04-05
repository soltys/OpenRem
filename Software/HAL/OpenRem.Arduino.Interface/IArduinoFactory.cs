using OpenRem.HAL;

namespace OpenRem.Arduino
{
    public interface IArduinoFactory
    {
        IDataStream Create(string comPort, ArduinoType arduinoType);
    }
}