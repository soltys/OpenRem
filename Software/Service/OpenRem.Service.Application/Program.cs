using System;
using Autofac;
using OpenRem.Common.Application.Autofac;
using OpenRem.Service.Interface;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = AutofacConfiguration.BuildContainer(AssemblyFilter.OnlyLogic);

            var serviceWrapper = container.Resolve<IEngineServiceHost>();
            serviceWrapper.Start();

            Console.WriteLine($"Running OpenRem service on {serviceWrapper.HostName}:{serviceWrapper.Port}. Press Enter to close...");
            Console.ReadLine();

            serviceWrapper.StopAsync();
        }
    }
}
