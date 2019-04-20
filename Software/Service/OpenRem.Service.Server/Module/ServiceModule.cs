using System;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;
using OpenRem.Service.Interface;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Module
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EngineServiceHost>().As<IEngineServiceHost>().SingleInstance();
        }
    }
}
