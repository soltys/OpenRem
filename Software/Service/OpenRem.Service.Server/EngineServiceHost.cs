using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Grpc.Core;
using OpenRem.Common.Service;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Server
{
    public class EngineServiceHost : IEngineServiceHost
    {
        private Grpc.Core.Server server;
        private ILifetimeScope scope;
        private readonly ServiceConfig config;

        public EngineServiceHost(ILifetimeScope scope)
        {
            this.scope = scope;
            this.config = this.scope.Resolve<ServiceConfig>();
        }

        public string HostName => this.config.HostName;

        public int Port => this.config.ServicePort;

        public void Start()
        {
            var engineServiceHost = typeof(EngineServiceHost);

            var services = new List<ServerServiceDefinition>();
            services.AddRange(GetServicesInAssembly(engineServiceHost.Assembly));

            CreateServer(services);
            this.server.Start();
        }

        private void CreateServer(List<ServerServiceDefinition> services)
        {
            this.server = new Grpc.Core.Server();
            foreach (var definition in services)
            {
                this.server.Services.Add(definition);
            }

            this.server.Ports.Add(
                new ServerPort(this.config.HostName, this.config.ServicePort, ServerCredentials.Insecure)
            );
        }

        private IEnumerable<ServerServiceDefinition> GetServicesInAssembly(Assembly assembly)
        {
            var services = assembly
                .GetTypes()
                .Where(x => x.GetCustomAttribute<ServiceImplementationAttribute>() != null)
                .Select(x => new
                {
                    ServerService = x.GetCustomAttribute<ServiceImplementationAttribute>().Service,
                    Implementation = x
                })
                .ToArray();

            foreach (var service in services)
            {
                var methods = service.ServerService.GetMethods(BindingFlags.Static | BindingFlags.Public);
                var bindService = methods.FirstOrDefault(x => x.Name == "BindService" && x.GetParameters().Length == 1);
                yield return (ServerServiceDefinition)bindService.Invoke(null,
                    new[] { this.scope.Resolve(service.Implementation) });
            }
        }

        public async Task StopAsync()
        {
            await this.server.ShutdownAsync().ConfigureAwait(false);
        }
    }
}