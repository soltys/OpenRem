using System.Reflection;
using Autofac;

namespace OpenRem.Emulator.Module
{
    public class EmulatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(dataAccess)
                .AsImplementedInterfaces();
        }
    }
}
