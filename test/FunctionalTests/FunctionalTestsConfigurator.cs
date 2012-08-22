using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using NUnit.Framework;
using UIT.iDeal.Acceptance.FunctionalTests.UserStories;
using UIT.iDeal.Common.Builders.Entities.Framework;
using UIT.iDeal.Common.Configuration;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks;
using UIT.iDeal.Infrastructure.Configuration.Flavours;
using UIT.iDeal.TestLibrary.Browsers;
using UIT.iDeal.TestLibrary.Configuration;
using UIT.iDeal.TestLibrary.Configuration.Bddify;
using UIT.iDeal.TestLibrary.Configuration.Ioc;
using UIT.iDeal.TestLibrary.Configuration.WebServers;
using UIT.iDeal.TestLibrary.Data;

namespace UIT.iDeal.Acceptance.FunctionalTests
{
    [SetUpFixture]
    public class FunctionalTestsConfigurator
    {
        public TestConfiguration Configuration { get; private set; }

        public FunctionalTestsConfigurator()
        {
            Configuration = TestConfigurator.New(cfg =>
            {
                cfg.UseIocContainer(new CastleWindsorIocContainer(ProjectFlavour.FunctionalTests));
                cfg.UseWebServer(new IISExpressWebServer(ConfigSettings.WebFolderName, ProjectFlavour.FunctionalTests));
                cfg.UseBrowser(BrowserDriver.Chrome);
            });
        }

        [SetUp]
        public void SetUpTestEnvironment()
        {
            BddifyConfiguration.InitializeBddify(new FunctionalTestsHtmlReportConfig());
            BuilderSetup.SetDefaultPropertyNamer(new PersistableEntityPropertyNamer(new ReflectionUtil()));

            Configuration.SetUpTestEnvironment();
            BrowserScenario.BaseUrl = Configuration.WebServer.BaseUrl;
            BrowserScenario.Driver = Configuration.WebDriver;
        }

        [TearDown]
        public void TearDownTestEnvironment()
        {
            Configuration.TearDownTestEnvironment();
        }
    }
}
