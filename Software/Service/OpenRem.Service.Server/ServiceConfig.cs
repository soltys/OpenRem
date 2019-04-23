using OpenRem.Service.Config;

namespace OpenRem.Service.Server
{
    public class ServiceConfig
    {
        private readonly Config.ServiceConfig config;

        public ServiceConfig(IServiceConfigReader configReader)
        {
            this.config = configReader.GetServiceConfig();
        }

        public int ServicePort => this.config.Port;
        public string HostName => this.config.HostName;
    }
}