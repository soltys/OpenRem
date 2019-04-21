namespace OpenRem.Config
{
    public interface IApplicationConfigReader
    {
        BootstraperConfig GetBootstraperConfig();
        ServiceConfig GetServiceConfig();
    }
}
