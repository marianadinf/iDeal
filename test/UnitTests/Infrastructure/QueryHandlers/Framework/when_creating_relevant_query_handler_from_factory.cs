using Castle.Windsor;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Interfaces.Queries;
using UIT.iDeal.Infrastructure.Bootstrapper.Factories;
using UIT.iDeal.Infrastructure.QueryHandlers;

namespace UIT.iDeal.UnitTests.Infrastructure.QueryHandlers.Framework
{
    
    public class when_creating_relevant_query_handler_from_factory : WithSubject<QueryHandlerFactory>
    {
        static IQueryHandler _result;
        
        Establish context = () =>
            The<IWindsorContainer>()
                .WhenToldTo(x => x.Resolve<FakeQueryHandler>())
                .Return(new FakeQueryHandler());

        Because of = () =>
            _result = Subject.Create<FakeQueryHandler>();

        It should_ask_the_container_to_return_the_requested_Query_Handler = () =>
            The<IWindsorContainer>().WasToldTo(x => x.Resolve<FakeQueryHandler>());


    }

    public class FakeQueryHandler : QueryHandler
    {
        public override object BuildViewModel()
        {
            throw new System.NotImplementedException();
        }
    }
}