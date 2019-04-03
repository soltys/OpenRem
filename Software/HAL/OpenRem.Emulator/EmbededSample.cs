using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
