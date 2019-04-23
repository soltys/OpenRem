using OpenRem.Config;

namespace OpenRem.Service.Client
{
    public class ServiceConfig
    {
        private readonly Config.ServiceConfig config;

        public ServiceConfig(IApplicationConfigReader configReader)
        {
            this.config = configReader.GetServiceConfig();
        }

        public int ServicePort => this.config.Port;
        public string HostName => this.config.HostName;
    }
}