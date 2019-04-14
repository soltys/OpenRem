using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenRem.Service
{
    public class ServiceType
    {
        public Type Implementation { get; private set; }
        public Type[] Interfaces { get; private set; }

        public static ServiceType[] GetServiceTypes(IEnumerable<Type> implementations, IEnumerable<Type> interfaces)
        {
            return implementations.Select(implementation =>
            {
                var publicInterfaces = implementation
                    .GetInterfaces()
                    .Where(interfaces.Contains).ToArray();

                return new ServiceType { Implementation = implementation, Interfaces = publicInterfaces };
            }).Where(x => x.Interfaces != null && x.Interfaces.Length > 0).ToArray();
        }
    }
}