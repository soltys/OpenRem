using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using OpenRem.Common.Config;

namespace OpenRem.Config.Infrastructure
{
    class BusinessLogicConfigurationProvider : IBusinessLogicConfigurationProvider
    {
        public IBusinessLogicConfiguration GetConfigurationRoot()
        {
            var manifestEmbeddedProvider =
                new ManifestEmbeddedFileProvider(typeof(BusinessLogicConfigurationProvider).Assembly);
            return new ConfigurationBuilder()
                .AddEmbeddedJsonFiles(manifestEmbeddedProvider, "Config")
                .Build()
                .ToBusinessLogicConfiguration();
        }
    }
}