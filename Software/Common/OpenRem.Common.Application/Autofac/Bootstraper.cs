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
    public static class Bootstraper
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
            foreach (var searchPattern in new[] { "OpenRem*.dll" })
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
                if (OmitAssembly(assemblyFilter, assembly)) continue;

                var modules = assembly.GetTypes()
                    .Where(p => typeof(IModule).IsAssignableFrom(p) && !p.IsAbstract)
                    .Select(p => (IModule)Activator.CreateInstance(p));

                foreach (var module in modules)
                {
                    builder.RegisterModule(module);
                }
            }
        }

        private static bool OmitAssembly(AssemblyFilter assemblyFilter, Assembly assembly)
        {
            if (assemblyFilter != AssemblyFilter.Everything)
            {
                if (assemblyFilter == AssemblyFilter.OmitServiceLayer)
                {
                    var serviceLayerAttribute = assembly.GetCustomAttribute<ServiceLayerAttribute>();
                    if (serviceLayerAttribute != null)
                    {
                        return true;
                    }
                }

                var attribute = assembly.GetCustomAttribute<ApplicationLayerAttribute>();
                if (attribute != null)
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