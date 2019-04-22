﻿using Microsoft.Extensions.Configuration;

namespace OpenRem.Config
{
    public static class ServiceConfigurationHelper
    {
        public static ISerivceConfiguration ToApplicationConfiguration(this IConfiguration configuration)
        {
            return new ServiceConfigurationAdapter(configuration);
        }
    }
}