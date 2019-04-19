using System;
using Autofac;
using OpenRem.Common;
using OpenRem.Service;
using OpenRem.Service.Interface;

namespace OpenRem.ServiceApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = AutofacConfiguration.BuildContainer();

            var serviceWrapper = container.Resolve<IEngineServiceHost>();
            serviceWrapper.Start();

            Console.WriteLine($"Running OpenRem service on {OpenRemServiceConfig.ServiceAddress}:{OpenRemServiceConfig.ServicePort}. Press Enter to close...");
            Console.ReadLine();

            serviceWrapper.Stop();
        }
    }
}
