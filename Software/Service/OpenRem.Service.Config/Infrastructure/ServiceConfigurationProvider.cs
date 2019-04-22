using Microsoft.Extensions.Configuration;

namespace OpenRem.Service.Config
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