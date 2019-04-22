namespace OpenRem.Config.Infrastructure
{
    internal interface IBusinessLogicConfigurationProvider
    {
        IBusinessLogicConfiguration GetConfigurationRoot();
    }
}