using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Machine.Fakes;
using Machine.Specifications;
using Moq;
using UIT.iDeal.Common.Interfaces.Queries;
using UIT.iDeal.Infrastructure.Bootstrapper.Factories;
using UIT.iDeal.Infrastructure.Bootstrapper.Installers;
using It = Machine.Specifications.It;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Bootstrapper.Installers
{
    [Subject(typeof(QueryHandlerInstaller))]
    public class when_registering_query_handlers_with_container : WithFakes
    {
        protected static IWindsorContainer SUT;
        protected static object _queryHandlerFactory;

        static IQueryHandler _fakeQueryHandler = new Mock<IQueryHandler>().Object;
        static IQueryHandler _result;

        Establish context = () =>
        {

            SUT = new WindsorContainer().Install(new QueryHandlerInstaller());
            SUT.Register(Component.For<IQueryHandler>().Instance(_fakeQueryHandler));

            SUT.Register(Component.For<IWindsorContainer>().Instance(SUT));
        };

        Because of = () =>
            _result = SUT.Resolve<IQueryHandler>();

        It should_be_able_to_resolve_the_query_handler_factory = () =>
            _result.ShouldBeTheSameAs(_fakeQueryHandler);


    }

    [Subject(typeof(QueryHandlerInstaller))]
    public class when_registering_query_handler_factory_with_container : WithFakes
    {
        protected static IWindsorContainer SUT;
        protected static object _queryHandlerFactory;

        Establish context = () =>
        {

            SUT = new WindsorContainer()
                .Install(new QueryHandlerInstaller());
            SUT.Register(Component.For<IWindsorContainer>().Instance(SUT));
        };

        Because of = () =>
            _queryHandlerFactory = SUT.Resolve<IQueryHandlerFactory>();

        It should_be_able_to_resolve_the_query_handler_factory = () =>
            _queryHandlerFactory.ShouldBeOfType<QueryHandlerFactory>();
    }
}