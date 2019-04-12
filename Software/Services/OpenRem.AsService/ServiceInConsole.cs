using System;
using Autofac;
using OpenRem.Common;
using OpenRem.Service;
using OpenRem.Service.Interface;

namespace AsService
{
    class ServiceInConsole
    {
        static void Main(string[] args)
        {
            var container = AutofacConfiguration.BuildContainer();

            var serviceWrapper = container.Resolve<IServiceWrapper>();
            serviceWrapper.StartServer();

            Console.WriteLine($"Running OpenRem service on {OpenRemServiceConfig.ServiceAddress}:{OpenRemServiceConfig.ServicePort}. Press Enter to close...");
            Console.ReadLine();

            serviceWrapper.StopServerIfInternalInstance();
        }
    }
}
