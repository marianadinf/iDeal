using System;
using UIT.iDeal.TestLibrary.Browsers;
using UIT.iDeal.TestLibrary.Configuration.Ioc;
using UIT.iDeal.TestLibrary.Configuration.WebServers;

namespace UIT.iDeal.TestLibrary.Configuration
{
    public class TestConfigurator : ITestConfigurator
    {
        private IConfigureWebServer _webServer;
        private BrowserDriver _webDriver;
        private IConfigureIoc _container;
       
        public static TestConfiguration New(Action<ITestConfigurator> action)
        {
            //AssemblyResolver.HandleUnresovledAssemblies();
            
            using (var configurator = new TestConfigurator())
            {
                action(configurator);
                return configurator.Create();
            }
        }

        TestConfiguration Create()
        {
            var cfg = new TestConfiguration()
            {
                Container = _container,
                WebServer = _webServer,
                WebDriver = _webDriver,
            };
            return cfg;
        }

        public void UseIocContainer(IConfigureIoc container)
        {
            _container = container;
        }

        public void UseWebServer(IConfigureWebServer webServer)
        {
            _webServer = webServer;
        }

        public void UseBrowser(BrowserDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void Dispose()
        {
        }


    }
}