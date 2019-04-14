using System;
using System.ServiceModel;
using Binding = System.ServiceModel.Channels.Binding;

namespace OpenRem.Service
{
    public static class OpenRemServiceConfig
    {
        public static string ServiceAddress => Environment.MachineName;
        public static int ServicePort => 32123;

        public static Binding Binding => new NetTcpBinding(SecurityMode.None)
        {
            MaxBufferPoolSize = int.MaxValue,
            MaxBufferSize = int.MaxValue,
            MaxReceivedMessageSize = int.MaxValue,
            ReceiveTimeout = TimeSpan.MaxValue,
            SendTimeout = TimeSpan.MaxValue
        };

        public static string GetAddress(string machine, int port, string name)
        {
            return $"net.tcp://{machine}:{port}/{name}";
        }

        public static string GetAddress(Type type)
        {
            return GetAddress(ServiceAddress, ServicePort, type.Name);
        }

        public static string GetAddress(string name)
        {
            return GetAddress(ServiceAddress, ServicePort, name);
        }

    }
}
