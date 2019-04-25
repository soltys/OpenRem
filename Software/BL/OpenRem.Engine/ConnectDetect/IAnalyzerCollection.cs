using System;

namespace OpenRem.Engine
{
    internal interface IAnalyzerCollection
    {
        AnalyzerData this[Guid id] { get; }
        Guid Add(ArduinoDevice arduinoDevice);
        Guid Add(Emulator emulator);
    }
}