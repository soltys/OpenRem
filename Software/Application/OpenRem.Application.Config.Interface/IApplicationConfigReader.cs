namespace OpenRem.Config
{
    public interface IApplicationConfigReader
    {
        BootstrapperConfig GetBootstrapperConfig();
        ServiceConfig GetServiceConfig();
    }
}