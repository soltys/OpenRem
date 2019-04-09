using System;
using System.ServiceModel;

namespace OpenRem.Service
{
    internal static class ServiceHelper 
    {
        private class ServiceChannel : IDisposable
        {
            internal readonly IOpenRemService service;

            internal ServiceChannel()
            {
                service = new ChannelFactory<IOpenRemService>(OpenRemServiceConfig.Binding).CreateChannel(new EndpointAddress(OpenRemServiceConfig.EndpointAddress(OpenRemServiceConfig.ServiceAddress, OpenRemServiceConfig.ServicePort)));
            }

            public void Dispose()
            {
                IContextChannel context = service as IContextChannel;
                context.Close();
            }
        }

        internal static void Run(Action<IOpenRemService> action)
        {
            using (var conn = new ServiceChannel())
            {
                action(conn.service);
            }
        }

        internal static T Run<T>(Func<IOpenRemService, T> action)
        {
            using (var conn = new ServiceChannel())
            {
                return action(conn.service);
            }
        }

        internal static bool CanConnect()
        {
            try
            { 
                using (var conn = new ServiceChannel())
                {
                    IContextChannel context = conn.service as IContextChannel;
                    context.Open();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
