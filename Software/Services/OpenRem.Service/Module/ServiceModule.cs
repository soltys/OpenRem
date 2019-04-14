using System;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;
using OpenRem.Service.Interface;

namespace OpenRem.Service.Module
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceWrapper>().As<IServiceWrapper>().SingleInstance();

            var serviceTypes = ServiceWrapper.GetEngineTypes();

            foreach (var serviceType in serviceTypes)
            {
                foreach (var interfaceType in serviceType.Interfaces)
                {
                    RegisterServiceTypes(builder, serviceType.Implementation, interfaceType);
                }
            }
        }

        private static void RegisterServiceTypes(ContainerBuilder builder, Type implementation, Type interfaceType)
        {
            Type channelFactoryType = typeof(ChannelFactory<>);
            Type serviceAccessType = channelFactoryType.MakeGenericType(interfaceType);

            builder.Register(x => Activator.CreateInstance(serviceAccessType, OpenRemServiceConfig.Binding, OpenRemServiceConfig.GetAddress(implementation)))
                .As(serviceAccessType)
                .SingleInstance();

            builder.Register(x =>
                {
                    object serviceAccess = x.Resolve(serviceAccessType);
                    dynamic cf = Convert.ChangeType(serviceAccess, serviceAccessType);
                    return cf.CreateChannel();
                })
                .As(interfaceType)
                .UseWcfSafeRelease();
        }
    }
}
