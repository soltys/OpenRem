using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRem.Common
{
    public static class AppDomainHelper
    {
        public static string EngineAssemblyName => "OpenRem.Engine";
        public static string EngineInterfaceAssemblyName => "OpenRem.Engine.Interface";

        public static IEnumerable<Type> GetReferenceTypes(string assemblyName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.StartsWith(assemblyName))
                ?.GetTypes()
                .Where(x => x.IsClass);
        }

        public static IEnumerable<Type> GetInterfaceTypes(string assemblyName)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.StartsWith(assemblyName))
                ?.GetTypes()
                .Where(x => x.IsInterface);
        }
    }
}