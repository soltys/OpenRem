using System.Collections.Generic;
using OpenRem.Engine.Interface;
using OpenRem.Emulator.Interface;
using OpenRem.Arduino.Interface;

namespace OpenRem.Engine
{
    class DetectManager : IDetectManager
    {
        private readonly IEmulatorFactory emulatorFactory;
        private readonly IArduinoFactory arduinoFactory;
        private readonly IDeviceFinder deviceFinder;
        private readonly IEmulatorFinder emulatorFinder;

        public DetectManager(IEmulatorFactory emulatorFactory, IArduinoFactory arduinoFactory, IDeviceFinder deviceFinder, IEmulatorFinder emulatorFinder)
        {
            this.emulatorFactory = emulatorFactory;
            this.arduinoFactory = arduinoFactory;
            this.deviceFinder = deviceFinder;
            this.emulatorFinder = emulatorFinder;
        }

        public IEnumerable<Analyzer> GetAnalyzers()
        {
            var arduinoDevices = this.deviceFinder.GetArduinoDevices();
            foreach (var arduinoDevice in arduinoDevices)
            {
                var dataStream = this.arduinoFactory.Create(arduinoDevice.ComPort, ArduinoNameParser.ToArduinoType(arduinoDevice.Name));
                yield return new Analyzer(dataStream, arduinoDevice.Name);
            }

            var emulators = this.emulatorFinder.GetEmulators();
            foreach (var emulator in emulators)
            {
                var dataStream = this.emulatorFactory.Create(emulator.SignalName);
                yield return new Analyzer(dataStream, "Emulator - " + emulator.SignalName);
            }
        }
    }
}
