using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenRem.Common;

namespace OpenRem.Config
{
    internal class EmbeddedConfig : IEmbeddedConfig
    {
        public Stream GetConfigFile(string fileName)
        {
            return typeof(ArduinoConfigReader).Assembly.GetResourceStream(fileName);
        }
    }
}
