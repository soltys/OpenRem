using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace OpenRem.Engine
{
    public class EngineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterType<DetectManager>().As<IDetectManager>();

            builder.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(dataAccess).AsSelf();

            builder.RegisterType<AnalyzerCollection>().As<IAnalyzerCollection>().SingleInstance();
        }
    }
}