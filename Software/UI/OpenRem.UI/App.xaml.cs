using System.Windows;
using Autofac;
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
        private IEngineServiceHost serviceWrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceContainer = AutofacConfiguration.BuildContainer(new[] { "OpenRem.exe", "OpenRem.Service.Client" });
            this.serviceWrapper = serviceContainer.Resolve<IEngineServiceHost>();


            var applicationContainer = AutofacConfiguration.BuildContainer(new[] { "OpenRem.Service.dll", "OpenRem.Engine" });
            var csl = new AutofacServiceLocator(applicationContainer);
            ServiceLocator.SetLocatorProvider(() => csl);

            this.serviceWrapper.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.serviceWrapper.Stop();

            base.OnExit(e);
        }
    }
}
