using System.IO;

namespace OpenRem.Config
{
    internal interface IEmbeddedConfig
    {
        Stream GetConfigFile(string fileName);
    }
}