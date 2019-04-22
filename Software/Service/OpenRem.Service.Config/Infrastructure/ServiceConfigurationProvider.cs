using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using OpenRem.Common.Config;
namespace OpenRem.Config
{
    class ServiceConfigurationProvider : IServiceConfigurationProvider
    {
        public ISerivceConfiguration GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("Config/ServiceConfig.json")
                .Build()
                .ToApplicationConfiguration();
        }


    }
}
