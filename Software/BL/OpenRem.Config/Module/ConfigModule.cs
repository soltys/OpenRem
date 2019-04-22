using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using OpenRem.Config.Infrastructure;

namespace OpenRem.Config.Module
{
    class ConfigModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces();

            builder.RegisterType<BusinessLogicConfigurationProvider>()
                .As<IBusinessLogicConfigurationProvider>()
                .SingleInstance();

            builder.Register(c => c.Resolve<IBusinessLogicConfigurationProvider>().GetConfigurationRoot())
                .As<IBusinessLogicConfiguration>()
                .SingleInstance();
        }
    }
}