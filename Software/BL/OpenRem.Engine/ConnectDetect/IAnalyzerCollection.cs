using System;

namespace OpenRem.Engine
{
    internal interface IAnalyzerCollection
    {
        AnalyzerData this[Guid guid] { get; }
        void Clear();
        Guid Add(ArduinoDevice arduinoDevice);
        Guid Add(Emulator emulator);
    }
}