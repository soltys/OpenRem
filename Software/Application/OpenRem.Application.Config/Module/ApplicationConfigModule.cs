using System.Reflection;
using Autofac;

namespace OpenRem.Application.Config
{
    class ConfigModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces();

            builder.RegisterType<ApplicationConfigurationProvider>()
                .As<IApplicationConfigurationProvider>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IApplicationConfigurationProvider>().GetConfigurationRoot())
                .As<IApplicationConfiguration>()
                .SingleInstance();
        }
    }
}