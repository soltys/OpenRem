using System.Windows;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using OpenRem.Common;
using OpenRem.Service.Interface;

namespace OpenRem.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var container = AutofacConfiguration.BuildContainer();
            var csl = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => csl);
            var serviceWrapper = ServiceLocator.Current.GetInstance<IEngineServiceHost>();
            serviceWrapper.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ServiceLocator.Current.GetInstance<IEngineServiceHost>().Stop();

            base.OnExit(e);
        }
    }
}
