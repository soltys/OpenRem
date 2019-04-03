using System.Collections.Generic;

namespace OpenRem.Emulator.Interface
{
    public interface IEmbeddedSample
    {
        IEnumerable<string> GetSamples();
    }
}