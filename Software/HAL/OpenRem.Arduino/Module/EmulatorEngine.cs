using Autofac;
using OpenRem.Arduino.Interface;

namespace OpenRem.Arduino.Module
{
    public class ArduinoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ArduinoFactory>().As<IArduinoFactory>();
        }
    }
}
