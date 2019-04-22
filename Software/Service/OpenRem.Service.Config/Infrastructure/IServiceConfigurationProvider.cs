using Microsoft.Extensions.Configuration;

namespace OpenRem.Config
{
    internal interface IServiceConfigurationProvider
    {
        ISerivceConfiguration GetConfigurationRoot();
    }
}