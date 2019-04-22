using System.Reflection;
using Autofac;

namespace OpenRem.Service.Config
{
    class ServiceConfigModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces();

            builder.RegisterType<ServiceConfigurationProvider>()
                .As<IServiceConfigurationProvider>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IServiceConfigurationProvider>().GetConfigurationRoot())
                .As<ISerivceConfiguration>()
                .SingleInstance();
        }
    }
}