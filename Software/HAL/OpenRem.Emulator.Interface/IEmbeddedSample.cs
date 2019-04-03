using System.Collections.Generic;

namespace OpenRem.Emulator
{
    public interface IEmbeddedSample
    {
        IEnumerable<string> GetSamples();
    }
}