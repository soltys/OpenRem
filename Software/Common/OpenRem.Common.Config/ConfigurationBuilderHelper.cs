using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace OpenRem.Common.Config
{
    public static class ConfigurationBuilderHelper
    {
        public static ConfigurationBuilder AddEmbeddedJsonFiles(this ConfigurationBuilder builder,
            ManifestEmbeddedFileProvider manifestEmbeddedProvider, string folderName)
        {
            var dirContent = manifestEmbeddedProvider.GetDirectoryContents(folderName);
            foreach (var entry in dirContent)
            {
                builder.AddJsonFile(manifestEmbeddedProvider, $"{folderName}/{entry.Name}", false, false);
            }

            return builder;
        }
    }
}