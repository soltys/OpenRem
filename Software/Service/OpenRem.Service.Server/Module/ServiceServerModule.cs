using Autofac;

namespace OpenRem.Service.Server.Module
{
    public class ServiceServerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EngineServiceHost>().As<IEngineServiceHost>().SingleInstance();
            builder.RegisterType<ServiceConfig>().AsSelf().SingleInstance();
        }
    }
}