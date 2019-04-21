using Microsoft.Extensions.Configuration;

namespace OpenRem.Config
{
    internal interface IBusinessLogicConfigurationProvider
    {
        IBusinessLogicConfiguration GetConfigurationRoot();
    }
}