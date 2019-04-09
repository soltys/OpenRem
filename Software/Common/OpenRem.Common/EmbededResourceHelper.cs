using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OpenRem.Common
{
    public static class AssemblyExtensions
    {
        public static IEnumerable<string> GetResourcesEndsWith(this Assembly assembly, string resourceEndsWith)
        {
            return assembly.GetManifestResourceNames().Where(str => str.EndsWith(resourceEndsWith));
        }

        public static byte[] ReadResourceAllBytes(this Assembly assembly, string resource)
        {
            var resourceStream = assembly.GetManifestResourceStream(resource);
            return resourceStream.ReadAllBytes();
        }

        public static Stream GetResourceStream(this Assembly assembly, string resourceName)
        {
            var fullResourceName = assembly.GetManifestResourceNames()
                                           .Single(str => str.EndsWith(resourceName));
            return assembly.GetManifestResourceStream(fullResourceName);
        }
    }
}
