using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using OpenRem.Engine;
using OpenRem.Service.Protocol;

namespace OpenRem.Service.Client.Module
{
    class ServiceClientModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();
        }
    }
}
