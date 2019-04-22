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
            configuration.Bind("BootstrapperConfig", dto);
            return dto;
        }
    }
}