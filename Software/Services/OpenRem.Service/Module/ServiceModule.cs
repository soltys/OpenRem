using Autofac;
using OpenRem.Service.Interface;

namespace OpenRem.Service.Module
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OpenRemService>().AsSelf();
            builder.RegisterType<ServiceWrapper>().As<IServiceWrapper>().SingleInstance();
        }
    }
}
