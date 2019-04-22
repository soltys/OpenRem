using OpenRem.Config;

namespace OpenRem.Service.Client
{
    public class ServiceConfig
    {
        private readonly Config.ServiceConfig config;

        public ServiceConfig(IApplicationConfigReader configReader)
        {
            config = configReader.GetServiceConfig();
        }

        public int ServicePort => config.Port;
        public string HostName => config.HostName;
    }
}