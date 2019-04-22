using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using OpenRem.Core;

namespace OpenRem.Common.Application.Autofac
{
    public static class AutofacConfiguration
    {
        public static IContainer BuildContainer(AssemblyFilter assemblyFilter)
        {
            var builder = new ContainerBuilder();

            builder.RegisterSoftwareModules(assemblyFilter);

            var container = builder.Build();
            return container;
        }

        private static string GetApplicationPath()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static IEnumerable<string> GetAssemblyNames(string path)
        {
            List<string> assemblyNames = new List<string>();
            foreach (var searchPattern in new[] {"OpenRem*.dll", "OpenRem*.exe"})
            {
                assemblyNames.AddRange(Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly));
            }

            return assemblyNames;
        }

        private static void RegisterSoftwareModules(this ContainerBuilder builder, AssemblyFilter assemblyFilter)
        {
            var path = GetApplicationPath();

            //Preload assemblies
            var assemblyNames = GetAssemblyNames(path);


            var assemblies = assemblyNames.Select(Assembly.LoadFrom);

            foreach (var assembly in assemblies)
            {
                if (FilterAssembly(assemblyFilter, assembly)) continue;

                var modules = assembly.GetTypes()
                    .Where(p => typeof(IModule).IsAssignableFrom(p) && !p.IsAbstract)
                    .Select(p => (IModule) Activator.CreateInstance(p));

                foreach (var module in modules)
                {
                    builder.RegisterModule(module);
                }
            }
        }

        private static bool FilterAssembly(AssemblyFilter assemblyFilter, Assembly assembly)
        {
            if (assemblyFilter != AssemblyFilter.Everything)
            {
                var attributes = assembly.GetCustomAttributes<ApplicationLayerAttribute>().ToArray();
                if (attributes.Length > 0)
                {
                    if (assemblyFilter == AssemblyFilter.OnlyLogic)
                    {
                        return true;
                    }
                }
                else
                {
                    if (assemblyFilter == AssemblyFilter.OnlyApplicationLayer)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}