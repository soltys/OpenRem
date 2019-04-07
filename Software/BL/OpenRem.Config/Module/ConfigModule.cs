using System.Reflection;
using Autofac;

namespace OpenRem.Config.Module
{
    class ConfigModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces();
        }
    }
}
