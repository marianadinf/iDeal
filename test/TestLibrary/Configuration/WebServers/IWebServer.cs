namespace UIT.iDeal.TestLibrary.Configuration.WebServers
{
    public interface IConfigureWebServer : IConfigurationItem
    {
        string BaseUrl { get; }
    }
}