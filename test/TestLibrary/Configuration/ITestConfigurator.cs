using System;
using UIT.iDeal.TestLibrary.Browsers;
using UIT.iDeal.TestLibrary.Configuration.Ioc;
using UIT.iDeal.TestLibrary.Configuration.WebServers;

namespace UIT.iDeal.TestLibrary.Configuration
{
    public interface ITestConfigurator : IDisposable
    {
        void UseIocContainer(IConfigureIoc container);
        void UseWebServer(IConfigureWebServer webServer);
        void UseBrowser(BrowserDriver browserType);
    }
}
