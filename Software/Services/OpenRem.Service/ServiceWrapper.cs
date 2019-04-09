using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using Autofac;
using OpenRem.Common;
using OpenRem.Engine;
using OpenRem.Service.Interface;

namespace OpenRem.Service
{
    public class ServiceWrapper : IServiceWrapper
    {
        private ServiceHost host;

        public bool IsRunning => ServiceHelper.CanConnect();

        public void StartServer()
        {
            var container = AutofacConfiguration.BuildContainer();

            Task.Factory.StartNew(() =>
            {
                this.host = new ServiceHost(container.Resolve<OpenRemService>());
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
            }
        }

        public Analyzer[] GetAnalyzers()
        {
            return ServiceHelper.Run(service => service.GetAnalyzers().Select(x => new Analyzer(x.Id) { Name = x.Name }).ToArray());
        }
    }
}
