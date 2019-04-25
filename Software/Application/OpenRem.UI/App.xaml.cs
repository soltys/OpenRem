using System;
using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using OpenRem.Common.Application.Autofac;
using OpenRem.Config;
using OpenRem.Service.Server;

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
            var applicationContainer = Bootstraper.BuildContainer(AssemblyFilter.OnlyApplicationLayer);
            var configReader = applicationContainer.Resolve<IApplicationConfigReader>();
            var bootstrapperConfig = configReader.GetBootstrapperConfig();

            if (bootstrapperConfig.LogicSeparation == LogicSeparation.Unknown)
            {
                throw new ArgumentException($"{nameof(LogicSeparation)} should be defined in application config");
            }

            if (bootstrapperConfig.LogicSeparation == LogicSeparation.Binary)
            {
                var container = Bootstraper.BuildContainer(AssemblyFilter.OmitServiceLayer);
                ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container));
                return;
            }
            else if (bootstrapperConfig.LogicSeparation == LogicSeparation.SelfHostedService)
            {
                ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(applicationContainer));
                var serviceContainer = Bootstraper.BuildContainer(AssemblyFilter.OnlyLogic);
                this.serviceWrapper = serviceContainer.Resolve<IEngineServiceHost>();
                this.serviceWrapper.Start();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.serviceWrapper?.StopAsync();

            base.OnExit(e);
        }
    }
}