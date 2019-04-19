﻿using System.Windows;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using OpenRem.Common;
using OpenRem.Common.Application.Autofac;
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
            var serviceContainer = AutofacConfiguration.BuildContainer(AssemblyFilter.OnlyLogic);
            this.serviceWrapper = serviceContainer.Resolve<IEngineServiceHost>();


            var applicationContainer = AutofacConfiguration.BuildContainer(AssemblyFilter.OnlyApplicationLayer);
            var csl = new AutofacServiceLocator(applicationContainer);
            ServiceLocator.SetLocatorProvider(() => csl);

            this.serviceWrapper.Start();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.serviceWrapper.StopAsync();

            base.OnExit(e);
        }
    }
}