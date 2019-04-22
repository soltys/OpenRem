using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace OpenRem.Common.Config
{
    public static class ConfigurationHelper
    {
        public static IEnumerable<T> BindAll<T>(this IConfiguration config, string sectionName) where T : class, new()
        {
            var entries = config.GetSection(sectionName)
                .GetChildren();

            var dtos = new List<T>(entries.Count());
            foreach (var entry in entries)
            {
                var dto = new T();
                entry.Bind(dto);
                dtos.Add(dto);
            }

            return dtos;
        }
    }
}