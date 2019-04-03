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

            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces();
        }
    }
}
