using Microsoft.Extensions.Configuration;
namespace OpenRem.Config
{
    class ServiceConfigReader : IServiceConfigReader
    {
        private ISerivceConfiguration configuration;

        public ServiceConfigReader(ISerivceConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public ServiceConfig GetServiceConfig()
        {
            var dto = new ServiceConfig();
            this.configuration.Bind("ServiceConfig", dto);
            return dto;
        }
    }
}