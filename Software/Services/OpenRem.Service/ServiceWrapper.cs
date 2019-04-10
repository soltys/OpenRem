using System;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Autofac;
using OpenRem.Engine;
using OpenRem.Service.Interface;

namespace OpenRem.Service
{
    public class ServiceWrapper : IServiceWrapper
    {
        private ServiceHost host;
        private readonly ILifetimeScope scope;

        public ServiceWrapper(ILifetimeScope scope)
        {
            this.scope = scope;
        }

        public bool IsRunning => ServiceHelper.CanConnect();

        public void StartServer()
        {
            Task.Factory.StartNew(() =>
            {
                this.host = new ServiceHost(this.scope.Resolve<OpenRemService>());
                this.host.AddServiceEndpoint(typeof(IOpenRemService), OpenRemServiceConfig.Binding, OpenRemServiceConfig.EndpointAddress(OpenRemServiceConfig.ServiceAddress, OpenRemServiceConfig.ServicePort));
                this.host.Open();
            });
        }

        public void StopServerIfInternalInstance()
        {
            if (this.host != null)
            {
                this.host.Close();
                this.host = null;
                this.scope.Dispose();
            }
        }

        public Analyzer[] GetAnalyzers()
        {
            return ServiceHelper.Run(service => service.GetAnalyzers().Select(x => new Analyzer(x.Id) { Name = x.Name }).ToArray());
        }

        public void StartRawFileRecorder(Guid analyzerGuid, string fileName)
        {
            ServiceHelper.Run(service => service.StartRawFileRecorder(analyzerGuid, fileName));
        }

        public void StopRawFileRecorder()
        {
            ServiceHelper.Run(service => service.StopRawFileRecorder());
        }
    }
}
