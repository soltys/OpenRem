using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace OpenRem.Config.Infrastructure
{
    class BusinessLogicConfigurationAdapter : IBusinessLogicConfiguration
    {
        private readonly IConfiguration configuration;

        public BusinessLogicConfigurationAdapter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string this[string key]
        {
            get => this.configuration[key];
            set => this.configuration[key] = value;
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return this.configuration.GetChildren();
        }

        public IChangeToken GetReloadToken()
        {
            return this.configuration.GetReloadToken();
        }

        public IConfigurationSection GetSection(string key)
        {
            return this.configuration.GetSection(key);
        }
    }
}