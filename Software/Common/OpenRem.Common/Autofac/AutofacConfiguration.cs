using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;

namespace OpenRem.Common
{
    public static class AutofacConfiguration
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterSoftwareModules();
            
            var container = builder.Build();
            return container;
        }

        private static string GetApplicationPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static List<string> GetAssemblyNames(string path)
        {
            List<string> assemblyNames = new List<string>();
            foreach (var searchPattern in new[]{ "OpenRem*.dll", "OpenRem*.exe" })
            {
                assemblyNames.AddRange(Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly));
            }

            return assemblyNames;
        }

        private static void RegisterSoftwareModules(this ContainerBuilder builder)
        {
            var path = GetApplicationPath();

            //Preload assemblies
            var assemblyNames = GetAssemblyNames(path);
            var assemblies = assemblyNames.Select(Assembly.LoadFrom);

            foreach (var assembly in assemblies)
            {
                var modules = assembly.GetTypes()
                    .Where(p => typeof(IModule).IsAssignableFrom(p) && !p.IsAbstract)
                    .Select(p => (IModule)Activator.CreateInstance(p));

                foreach (var module in modules)
                {
                    builder.RegisterModule(module);
                }
            }
        }

        
    }
}
