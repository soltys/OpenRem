using Autofac;

namespace OpenRem.UI.Module
{
    public class ViewModelModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindowViewModel>().AsSelf();
        }
    }
}