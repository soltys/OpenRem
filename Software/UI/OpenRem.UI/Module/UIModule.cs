using Autofac;

namespace OpenRem.UI.Module
{
    public class UIModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().AsSelf();
        }
    }
}
