using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Interfaces.Forms;
using UIT.iDeal.Infrastructure.Bootstrapper.Installers;
using UIT.iDeal.Infrastructure.Configuration;
using UIT.iDeal.Infrastructure.Configuration.Flavours;
using UIT.iDeal.Infrastructure.Web;
using It = Machine.Specifications.It;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Bootstrapper.Installers
{
    [Subject(typeof(FormProcessorInstaller))]
    public class when_registering_form_processor_with_container : WithFakes
    {
        protected static IWindsorContainer SUT;
        protected static object _formProcessor;

        Establish context = () =>
            {

                EntityFrameworkDataProviderFacility.ConnectionStringProvider = () =>
                    ConfigurationFactory.DevelopmentEnvironment().ConnectionString(ProjectFlavour.IntegrationTests);

                EntityFrameworkDataProviderFacility.WithContextLifeStyle(LifestyleType.Transient);

                SUT = new WindsorContainer()
                    .AddFacility<EntityFrameworkDataProviderFacility>()
                    .Install(new AutoMapperInstaller(),
                                new FluentValidationInstaller(),
                                new CommandHandlerInstaller(),
                                new FormProcessorInstaller());
                SUT.Register(Component.For<IWindsorContainer>().Instance(SUT));
            };

        Because of = () =>
            _formProcessor = SUT.Resolve<IFormProcessor>();

        It should_be_able_to_resolve_FormProcessor = () =>
             _formProcessor.ShouldBeOfType<FormProcessor>();
    }
}