using System.Reflection;
using Autofac;
using OpenRem.Engine.Interface;
using Module = Autofac.Module;

namespace OpenRem.Engine
{
    public class EngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterType<DetectManager>().As<IDetectManager>();

            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces();
        }
    }
}
