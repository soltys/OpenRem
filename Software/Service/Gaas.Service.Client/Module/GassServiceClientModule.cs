using System.Reflection;
using Autofac;
using Gaas.Service.Client;
using Gaas.Service.Client.Interface;
using OpenRem.Core;

[assembly: ApplicationLayer]
namespace OpenRem.Gaas.Service.Client.Module
{
    class GassServiceClientModule:Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GraphChannelProvider>().As<IGraphChannelProvider>();
            builder.RegisterType<GraphServiceClient>().As<IGraphServiceClient>();

        }
    }
}
