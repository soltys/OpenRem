using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace OpenRem.Config.Module
{
    class ConfigModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces();

            builder.RegisterType<ConfigurationRootProvider>()
                .As<IConfigurationRootProvider>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IConfigurationRootProvider>().GetConfigurationRoot())
                .As<IConfiguration>()
                .SingleInstance();
        }
    }
}
