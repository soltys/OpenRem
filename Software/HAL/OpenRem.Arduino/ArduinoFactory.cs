using OpenRem.HAL;

namespace OpenRem.Arduino
{
    class ArduinoFactory : IArduinoFactory
    {
        public IDataStream Create(string comPort, ArduinoType arduinoType)
        {
            return new ArduinoDataStream(comPort, arduinoType);
        }
    }
}
