using System;
using System.ServiceModel;
using Autofac;
using Autofac.Integration.Wcf;
using OpenRem.Common;
using OpenRem.Engine;
using OpenRem.Service.Interface;

namespace OpenRem.Service.Module
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ServiceWrapper>().As<IServiceWrapper>().SingleInstance();


            builder.Register(c => new ChannelFactory<IDetectManager>(
                   OpenRemServiceConfig.Binding,
                   OpenRemServiceConfig.GetAddress("DetectManager")))
                .SingleInstance();
            builder
                .Register(c => c.Resolve<ChannelFactory<IDetectManager>>().CreateChannel())
                .As<IDetectManager>()
                .UseWcfSafeRelease();

            builder.Register(c => new ChannelFactory<IRawFileRecorder>(
                    OpenRemServiceConfig.Binding,
                    OpenRemServiceConfig.GetAddress("RawFileRecorder")))
                .SingleInstance();
            builder
                .Register(c => c.Resolve<ChannelFactory<IRawFileRecorder>>().CreateChannel())
                .As<IRawFileRecorder>()
                .UseWcfSafeRelease();

        }
    }
}
