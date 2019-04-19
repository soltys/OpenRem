﻿using System;
using Autofac;
using OpenRem.Common;
using OpenRem.Common.Application.Autofac;
using OpenRem.Service;
using OpenRem.Service.Interface;
using OpenRem.Service.Protocol;

namespace OpenRem.ServiceApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = AutofacConfiguration.BuildContainer(AssemblyFilter.OnlyLogic);

            var serviceWrapper = container.Resolve<IEngineServiceHost>();
            serviceWrapper.Start();

            Console.WriteLine($"Running OpenRem service on localhost:{ServiceConfig.ServicePort}. Press Enter to close...");
            Console.ReadLine();

            serviceWrapper.StopAsync();
        }
    }
}