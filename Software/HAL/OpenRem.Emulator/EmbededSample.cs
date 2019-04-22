using System.Collections.Generic;
using OpenRem.Common;

namespace OpenRem.Emulator
{
    public class EmbeddedSample : IEmbeddedSample
    {
        public IEnumerable<string> GetSamples()
        {
            return typeof(EmbeddedSample).Assembly.GetResourcesEndsWith(".raw");
        }
    }
}