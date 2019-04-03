namespace OpenRem.Engine
{
    internal interface IDeviceFinder
    {
        ArduinoDevice[] GetArduinoDevices();
    }
}