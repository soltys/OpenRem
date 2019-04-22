namespace OpenRem.Config
{
    public interface IAnalyzerConfigReader
    {
        AnalyzerConfig GetConfig(string name);
    }
}