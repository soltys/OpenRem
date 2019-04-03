using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
