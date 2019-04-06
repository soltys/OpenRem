using Autofac;

namespace OpenRem.Emulator.Module
{
    public class EmulatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmbeddedSample>().As<IEmbeddedSample>();
            builder.RegisterType<EmulatorFactory>().As<IEmulatorFactory>();
        }
    }
}
