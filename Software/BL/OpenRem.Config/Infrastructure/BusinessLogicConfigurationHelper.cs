using Microsoft.Extensions.Configuration;

namespace OpenRem.Config.Infrastructure
{
    public static class BusinessLogicConfigurationHelper
    {
        public static IBusinessLogicConfiguration ToBusinessLogicConfiguration(this IConfiguration configuration)
        {
            return new BusinessLogicConfigurationAdapter(configuration);
        }
    }
}