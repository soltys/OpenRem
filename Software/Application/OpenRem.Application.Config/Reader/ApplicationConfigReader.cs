using Microsoft.Extensions.Configuration;
namespace OpenRem.Config
{
    class ApplicationConfigReader : IApplicationConfigReader
    {
        private IApplicationConfiguration configuration;

        public ApplicationConfigReader(IApplicationConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public BootstraperConfig GetBootstraperConfig()
        {
            var dto = new BootstraperConfig();
            this.configuration.GetSection("BootstraperConfig").Bind(dto);
            return dto;
        }

        public ServiceConfig GetServiceConfig()
        {
            var dto = new ServiceConfig();
            this.configuration.GetSection("BootstraperConfig").Bind(dto);
            return dto;
        }
    }
}