using Microsoft.Extensions.Configuration;

namespace OpenRem.Config
{
    internal interface IApplicationConfigurationProvider
    {
        IApplicationConfiguration GetConfigurationRoot();
    }
}