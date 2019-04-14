﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using OpenRem.Common;
using OpenRem.Service.Interface;

namespace OpenRem.Service
{
    public class EngineServiceHost : IEngineServceHost
    {
        private readonly List<ServiceHost> hosts = new List<ServiceHost>();
        private readonly ILifetimeScope scope;

        public EngineServiceHost(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public void Start()
        {
            var servicesTypes = GetEngineTypes();

            Task.Factory.StartNew(() =>
            {
                foreach (var publicType in servicesTypes)
                {
                    var host = CreateServiceHost(publicType);
                    this.hosts.Add(host);
                }

            });
        }
       
        private ServiceHost CreateServiceHost(ServiceType publicType)
        {
            Uri address = new Uri(OpenRemServiceConfig.GetAddress(publicType.Implementation));
            ServiceHost host = new ServiceHost(publicType.Implementation);
            foreach (var @interface in publicType.Interfaces)
            {
                host.AddServiceEndpoint(@interface, OpenRemServiceConfig.Binding, address);
            }

            host.AddDependencyInjectionBehavior(publicType.Implementation, scope);
            host.Open();
            return host;
        }

        public void Stop()
        {
            foreach (var host in this.hosts)
            {
                host.Close();
            }
            this.hosts.Clear();
        }


        public static ServiceType[] GetEngineTypes()
        {
            var engineImplementation = AppDomainHelper.GetReferenceTypes(AppDomainHelper.EngineAssemblyName);
            var engineInterfaces = AppDomainHelper.GetInterfaceTypes(AppDomainHelper.EngineInterfaceAssemblyName);

            if (engineImplementation == null || engineInterfaces == null)
            {
                throw new InvalidOperationException("Types are not found");
            }

            var servicesTypes = ServiceType.GetServiceTypes(engineImplementation, engineInterfaces);
            return servicesTypes;
        }

    }
}