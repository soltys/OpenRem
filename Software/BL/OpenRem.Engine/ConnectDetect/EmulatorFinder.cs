using System.Collections.Generic;
using System.Linq;
using OpenRem.Emulator.Interface;

namespace OpenRem.Engine
{
    class EmulatorFinder : IEmulatorFinder
    {
        private readonly IEmbeddedSample embeddedSample;

        public EmulatorFinder(IEmbeddedSample embeddedSample)
        {
            this.embeddedSample = embeddedSample;
        }

        public Emulator[] GetEmulators()
        {
            var emulators = new List<Emulator>();
            var samples = this.embeddedSample.GetSamples();
            if (samples == null)
            {
                return emulators.ToArray();
            }

            var embeddedEmulators = samples.Select(x => new Emulator
            {
                SignalName = x,
                EmbeddedSignal = true
            });
            emulators.AddRange(embeddedEmulators);

            return emulators.ToArray();
        }
    }
}
