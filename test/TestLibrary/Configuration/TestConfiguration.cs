using UIT.iDeal.TestLibrary.Browsers;
using UIT.iDeal.TestLibrary.Configuration.Ioc;
using UIT.iDeal.TestLibrary.Configuration.WebServers;

namespace UIT.iDeal.TestLibrary.Configuration
{
    public class TestConfiguration
    {
        public IConfigureIoc Container { get; set; }
        public IConfigureWebServer WebServer { get; set; }
        public BrowserDriver WebDriver { get; set; }
        
        public virtual void SetUpTestEnvironment()
        {
            // Replace with collection of IConfigurationItems
            // Loop through collection and call Start() method
            if (Container != null)
            {
                Container.Start();
            }
            if (WebServer != null)
            {
                WebServer.Start();
            }
        
        }

        public virtual void TearDownTestEnvironment()
        {
            // Replace with collection of IConfigurationItems
            // Loop through collection and call Stop() method

            if (WebServer != null)
            {
                WebServer.Stop();
            }
            if (Container != null)
            {
                Container.Stop();
            }
        }
    }
}