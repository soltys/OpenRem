using OpenRem.Service.Config;

namespace OpenRem.Service.Server
{
    public class ServiceConfig
    {
        private readonly Config.ServiceConfig config;

        public ServiceConfig(IServiceConfigReader configReader)
        {
            config = configReader.GetServiceConfig();
        }

        public int ServicePort => config.Port;
        public string HostName => config.HostName;
    }
}