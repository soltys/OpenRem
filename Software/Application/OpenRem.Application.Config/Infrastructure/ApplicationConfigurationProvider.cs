using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using OpenRem.Common.Config;
namespace OpenRem.Config
{
    class ApplicationConfigurationProvider : IApplicationConfigurationProvider
    {
        public IApplicationConfiguration GetConfigurationRoot()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("Config/ApplicationConfig.json")
                .Build()
                .ToApplicationConfiguration();
        }


    }
}
