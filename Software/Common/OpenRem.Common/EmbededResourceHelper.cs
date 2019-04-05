using System.Collections.Generic;
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
    }
}
