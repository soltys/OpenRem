using System;
using System.Collections.Generic;
using OpenRem.Arduino;
using OpenRem.Config;
using OpenRem.Emulator;

namespace OpenRem.Engine
{
    class AnalyzerCollection : IAnalyzerCollection
    {
        private readonly Dictionary<Guid, AnalyzerData> inMemoryAnalyzers = new Dictionary<Guid, AnalyzerData>();

        private readonly IEmulatorFactory emulatorFactory;
        private readonly IArduinoFactory arduinoFactory;
        private readonly IAnalyzerConfigReader analyzerConfigReader;

        public AnalyzerCollection(IEmulatorFactory emulatorFactory, IArduinoFactory arduinoFactory, IAnalyzerConfigReader analyzerConfigReader)
        {
            this.emulatorFactory = emulatorFactory;
            this.arduinoFactory = arduinoFactory;
            this.analyzerConfigReader = analyzerConfigReader;
        }

        public void Clear()
        {
            this.inMemoryAnalyzers.Clear();
        }

        public Guid Add(ArduinoDevice arduinoDevice)
        {
            var guid = Guid.NewGuid();
            var arduinoType = ArduinoNameParser.ToArduinoType(arduinoDevice.Name);
            this.inMemoryAnalyzers.Add(guid,
                new AnalyzerData()
                {
                    FactoryMethod = () => this.arduinoFactory.Create(arduinoDevice.ComPort, arduinoType),
                    AnalyzerConfig = this.analyzerConfigReader.GetConfig(arduinoType.ToString())
                });
           
            return guid;
        }

        public Guid Add(Emulator emulator)
        {
            var guid = Guid.NewGuid();

            this.inMemoryAnalyzers.Add(guid,
                new AnalyzerData()
                {
                    FactoryMethod = () => this.emulatorFactory.Create(emulator.SignalName),
                    AnalyzerConfig = this.analyzerConfigReader.GetConfig("Emulator")
                });

            return guid;
        }

        public AnalyzerData this[Guid guid] => this.inMemoryAnalyzers[guid];
    }
}
