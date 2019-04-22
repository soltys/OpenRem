using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpenRem.Engine
{
    class DetectManager : IDetectManager
    {
        private readonly IDeviceFinder deviceFinder;
        private readonly IEmulatorFinder emulatorFinder;
        private readonly IAnalyzerCollection analyzerCollection;

        public DetectManager(IAnalyzerCollection analyzerCollection, IDeviceFinder deviceFinder,
            IEmulatorFinder emulatorFinder)
        {
            this.analyzerCollection = analyzerCollection;
            this.deviceFinder = deviceFinder;
            this.emulatorFinder = emulatorFinder;
        }

        public Task<Analyzer[]> GetAnalyzersAsync()
        {
            var analyzers = new List<Analyzer>();

            this.analyzerCollection.Clear();
            var arduinoDevices = this.deviceFinder.GetArduinoDevices();
            foreach (var arduinoDevice in arduinoDevices)
            {
                var guid = this.analyzerCollection.Add(arduinoDevice);

                analyzers.Add(ToAnalyzer(guid, arduinoDevice));
            }

            var emulators = this.emulatorFinder.GetEmulators();
            foreach (var emulator in emulators)
            {
                var guid = this.analyzerCollection.Add(emulator);

                analyzers.Add(ToAnalyzer(guid, emulator));
            }

            return Task.FromResult(analyzers.ToArray());
        }

        private Analyzer ToAnalyzer(Guid id, ArduinoDevice arduinoDevice)
        {
            return new Analyzer
            {
                Id = id,
                Name = arduinoDevice.Name
            };
        }

        private Analyzer ToAnalyzer(Guid id, Emulator emulator)
        {
            return new Analyzer
            {
                Id = id,
                Name = "Emulator - " + emulator.SignalName
            };
        }
    }
}