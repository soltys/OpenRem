using System;

namespace OpenRem.Service.Server
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceImplementationAttribute : Attribute
    {
        /// <summary>
        /// Add Service implementation for Server instance
        /// </summary>
        /// <param name="grpcService">gRPC generated service</param>
        public ServiceImplementationAttribute(Type grpcService)
        {
            Service = grpcService;
        }

        /// <summary>
        /// gRPC service type
        /// </summary>
        public Type Service { get; }
    }
}