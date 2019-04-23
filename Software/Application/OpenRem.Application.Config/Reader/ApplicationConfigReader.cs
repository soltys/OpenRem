using Microsoft.Extensions.Configuration;
using OpenRem.Config;

namespace OpenRem.Application.Config
{
    class ApplicationConfigReader : IApplicationConfigReader
    {
        private IApplicationConfiguration configuration;

        public ApplicationConfigReader(IApplicationConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BootstrapperConfig GetBootstrapperConfig()
        {
            var dto = new BootstrapperConfig();
            this.configuration.Bind("BootstrapperConfig", dto);
            return dto;
        }

        public ServiceConfig GetServiceConfig()
        {
            var dto = new ServiceConfig();
            this.configuration.Bind("ServiceConfig", dto);
            return dto;
        }
    }
}