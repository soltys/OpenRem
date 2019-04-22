namespace OpenRem.Service.Config
{
    internal interface IServiceConfigurationProvider
    {
        ISerivceConfiguration GetConfigurationRoot();
    }
}