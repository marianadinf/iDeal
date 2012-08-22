using FizzWare.NBuilder;
using FizzWare.NBuilder.Implementation;
using NUnit.Framework;
using UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories;
using UIT.iDeal.Common.Builders.Entities.Framework;
using UIT.iDeal.Infrastructure.Configuration.Flavours;
using UIT.iDeal.TestLibrary.Configuration;
using UIT.iDeal.TestLibrary.Configuration.Bddify;
using UIT.iDeal.TestLibrary.Configuration.Ioc;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications
{
    /// <summary>
    /// This class has methods that run before and after each test run
    /// It configures and disposes the Inversion of Control container
    /// </summary>
    [SetUpFixture]
    public class ExecutableSpecificationsConfigurator
    {
        public TestConfiguration Configuration { get; private set; }

        public ExecutableSpecificationsConfigurator()
        {
            Configuration = TestConfigurator.New(cfg => cfg.UseIocContainer(new CastleWindsorIocContainer(ProjectFlavour.ExecutableSpecifications)));
        }

        [SetUp]
        public void SetUpTestEnvironment()
        {
            BddifyConfiguration.InitializeBddify(new ExecutableSpecificationsHtmlReportConfig());
            BuilderSetup.SetDefaultPropertyNamer(new PersistableEntityPropertyNamer(new ReflectionUtil()));

            Configuration.SetUpTestEnvironment();
        }

        [TearDown]
        public void TearDownTestEnvironment()
        {
            Configuration.TearDownTestEnvironment();
        }
    }
}
