using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace OpenRem.Config
{
    class ConfigurationRootProvider : IConfigurationRootProvider
    {
        public IConfigurationRoot GetConfigurationRoot()
        {
            var manifestEmbeddedProvider = new ManifestEmbeddedFileProvider(typeof(ConfigurationRootProvider).Assembly);
            return new ConfigurationBuilder()
                        .AddJsonFile(manifestEmbeddedProvider, "Config/Analyzer.json", false, false)
                        .Build();
        }
    }
}
