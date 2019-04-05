using System;
using OpenRem.HAL;

namespace OpenRem.Engine
{
    internal interface IAnalyzerCollection
    {
        Func<IDataStream> this[Guid guid] { get; }
        void Clear();
        Guid Add(ArduinoDevice arduinoDevice);
        Guid Add(Emulator emulator);
    }
}