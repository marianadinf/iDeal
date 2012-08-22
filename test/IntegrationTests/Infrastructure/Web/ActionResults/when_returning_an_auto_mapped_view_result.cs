using Machine.Specifications;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.ActionResults
{
    public class when_returning_an_auto_mapped_view_result
    {
        protected static AutoMappedViewResult Subject;
        protected static FakeDomain DomainAggregateRoot;
        protected static FakeView View;
        protected static object Result;

        Establish context = () =>
        {
            DomainAggregateRoot = new FakeDomain();
            View = new FakeView();
            Subject = new AutoMappedViewResult(typeof (FakeView)) {ViewData = {Model = DomainAggregateRoot}};
            MapperExtensions.Map = (entity, entityType, dtoType) => View;
        };

        Because of = () =>
            Result = Subject.BuildViewModel();

        It should_map_the_domain_object_to_a_view_model = () =>
            Result.ShouldBeOfType<FakeView>();
    }
}