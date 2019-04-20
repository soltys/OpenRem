using Microsoft.Extensions.Configuration;

namespace OpenRem.Config
{
    internal interface IConfigurationRootProvider
    {
        IConfigurationRoot GetConfigurationRoot();
    }
}