using System.Linq;
using System.Reflection;
using Autofac;

namespace OpenRem.Service.Server.Module
{
    public class ServiceServerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EngineServiceHost>().As<IEngineServiceHost>().SingleInstance();
            builder.RegisterType<ServiceConfig>().AsSelf().SingleInstance();

            var dataAccess = Assembly.GetExecutingAssembly();
            var serviceImplementations = dataAccess.GetTypes()
                .Where(x => x.GetCustomAttribute<ServiceImplementationAttribute>() != null)
                .ToArray();
            builder.RegisterTypes(serviceImplementations);
        }
    }
}