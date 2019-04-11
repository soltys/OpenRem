using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Wcf;
using OpenRem.Service.Interface;

namespace OpenRem.Service
{
    public class ServiceWrapper : IServiceWrapper
    {
        private List<ServiceHost> hosts = new List<ServiceHost>();
        private readonly ILifetimeScope scope;

        public ServiceWrapper(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public void StartServer()
        {
            var engineImplementation = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.StartsWith("OpenRem.Engine"))
                ?.GetTypes()
                .Where(x => x.IsClass);

            var engineInterfaces = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(x => x.FullName.StartsWith("OpenRem.Engine.Interface"))
                ?.GetTypes()
                .Where(x => x.IsInterface);

            if (engineImplementation == null || engineInterfaces == null)
            {
                throw new InvalidOperationException("Types are not found");
                
            }

            var publicTypes = engineImplementation.Select(implementation =>
            {
                var publicInterfaces = implementation
                    .GetInterfaces()
                    .Where(i => engineInterfaces.Contains(i)).ToArray();

                return  new ServiceType  { Implementation = implementation, Interfaces = publicInterfaces };
            }).Where(x => x.Interfaces != null && x.Interfaces.Length > 0).ToArray();


            Task.Factory.StartNew(() =>
            {
                foreach (var publicType in publicTypes)
                {
                    var host = CreateServiceHost(publicType);
                    this.hosts.Add(host);
                }

            });
        }

        private ServiceHost CreateServiceHost(ServiceType publicType)
        {
            Uri address = new Uri(
                OpenRemServiceConfig.EndpointAddress(
                    OpenRemServiceConfig.ServiceAddress,
                    OpenRemServiceConfig.ServicePort,
                    publicType.Implementation.Name));
            ServiceHost host = new ServiceHost(publicType.Implementation);
            foreach (var @interface in publicType.Interfaces)
            {
                host.AddServiceEndpoint(@interface, OpenRemServiceConfig.Binding, address);
            }

            host.AddDependencyInjectionBehavior(publicType.Implementation, scope);
            host.Open();
            return host;
        }

        public void StopServerIfInternalInstance()
        {
            foreach (var host in this.hosts)
            {
                host.Close();
            }
            this.hosts.Clear();
        }

        private class ServiceType
        {
            public Type Implementation { get; set; }
            public Type[] Interfaces { get; set; }
        }
    }
}
