namespace OpenRem.Application.Config
{
    internal interface IApplicationConfigurationProvider
    {
        IApplicationConfiguration GetConfigurationRoot();
    }
}