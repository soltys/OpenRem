using System.Collections.Generic;
using System.IO;
using OpenRem.Common;

namespace OpenRem.Config
{
    internal class EmbeddedConfig : IEmbeddedConfig
    {
        public Stream GetConfigFile(string fileName)
        {
            return typeof(EmbeddedConfig).Assembly.GetResourceStream(fileName);
        }

        public IEnumerable<string> GetFiles(string extension)
        {
            return typeof(EmbeddedConfig).Assembly.GetResourcesEndsWith(extension);
        }
    }
}
