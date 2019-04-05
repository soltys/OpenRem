using System;
using System.Collections.Generic;
using OpenRem.Arduino;
using OpenRem.Common;
using OpenRem.Emulator.Interface;

namespace OpenRem.Engine
{
    class AnalyzerCollection: IAnalyzerCollection
    {
        private readonly Dictionary<Guid, Func<IDataStream>> inMemoryAnalyzers = new Dictionary<Guid, Func<IDataStream>>();

        private readonly IEmulatorFactory emulatorFactory;
        private readonly IArduinoFactory arduinoFactory;

        public AnalyzerCollection(IEmulatorFactory emulatorFactory, IArduinoFactory arduinoFactory)
        {
            this.emulatorFactory = emulatorFactory;
            this.arduinoFactory = arduinoFactory;
        }

        public void Clear()
        {
            this.inMemoryAnalyzers.Clear();
        }

        public Guid Add(ArduinoDevice arduinoDevice)
        {
            var guid = Guid.NewGuid();
            this.inMemoryAnalyzers.Add(guid,
               ()=> this.arduinoFactory.Create(arduinoDevice.ComPort, ArduinoNameParser.ToArduinoType(arduinoDevice.Name))
           );
            return guid;
        }

        public Guid Add(Emulator emulator)
        {
            var guid = Guid.NewGuid();
            this.inMemoryAnalyzers.Add(guid,
                () => this.emulatorFactory.Create(emulator.SignalName)
            );
            return guid;
        }

        public Func<IDataStream> this[Guid guid] => this.inMemoryAnalyzers[guid];
    }
}
