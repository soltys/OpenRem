using System;
using System.Collections.Generic;
using System.Linq;
using OpenRem.Arduino;
using OpenRem.Config;
using OpenRem.Emulator;

namespace OpenRem.Engine
{
    class AnalyzerCollection : IAnalyzerCollection
    {
        private readonly HashSet<AnalyzerData> inMemoryAnalyzers = new HashSet<AnalyzerData>();

        private readonly IEmulatorFactory emulatorFactory;
        private readonly IArduinoFactory arduinoFactory;
        private readonly IAnalyzerConfigReader analyzerConfigReader;

        public AnalyzerCollection(IEmulatorFactory emulatorFactory, IArduinoFactory arduinoFactory,
            IAnalyzerConfigReader analyzerConfigReader)
        {
            this.emulatorFactory = emulatorFactory;
            this.arduinoFactory = arduinoFactory;
            this.analyzerConfigReader = analyzerConfigReader;
        }
        
        public Guid Add(ArduinoDevice arduinoDevice)
        {
            var hardwareKey = new HardwareKey(arduinoDevice.Name + arduinoDevice.ComPort);

            var alreadyExists = this.inMemoryAnalyzers.FirstOrDefault(x => x.HardwareKey.Equals(hardwareKey));
            if (alreadyExists != null)
            {
                return alreadyExists.Id;
            }

            var guid = Guid.NewGuid();
            var arduinoType = ArduinoNameParser.ToArduinoType(arduinoDevice.Name);
            var analyzerData = new AnalyzerData(guid, hardwareKey)
            {
                Factory = () => this.arduinoFactory.Create(arduinoDevice.ComPort, arduinoType),
                AnalyzerConfig = this.analyzerConfigReader.GetConfig(arduinoType.ToString())
            };
            this.inMemoryAnalyzers.Add(analyzerData);

            return guid;
        }

        public Guid Add(Emulator emulator)
        {
            var hardwareKey = new HardwareKey("Emulator" + emulator.SignalName);

            var alreadyExists = this.inMemoryAnalyzers.FirstOrDefault(x => x.HardwareKey.Equals(hardwareKey));
            if (alreadyExists != null)
            {
                return alreadyExists.Id;
            }


            var guid = Guid.NewGuid();
            var analyzerData = new AnalyzerData(guid, hardwareKey)
            {
                Factory = () => this.emulatorFactory.Create(emulator.SignalName),
                AnalyzerConfig = this.analyzerConfigReader.GetConfig("Emulator")
            };

            this.inMemoryAnalyzers.Add(analyzerData);

            return guid;
        }

        public AnalyzerData this[Guid id]  => this.inMemoryAnalyzers.FirstOrDefault(x => x.Id == id);
    }
}