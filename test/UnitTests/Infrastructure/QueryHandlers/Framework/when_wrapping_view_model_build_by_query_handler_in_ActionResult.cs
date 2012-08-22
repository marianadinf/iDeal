using System.Web.Mvc;
using Machine.Fakes;
using Machine.Specifications;
using Moq;
using UIT.iDeal.Common.Interfaces.Queries;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.TestLibrary.FakeObjects;
using It = Machine.Specifications.It;

namespace UIT.iDeal.UnitTests.Infrastructure.QueryHandlers.Framework
{
    
    public class QueryHandlerExtensionsSpec<TActionResult> : WithFakes
        where TActionResult:ActionResult
    {
        protected static IQueryHandler QueryHandler;
        protected static TActionResult Result;
        protected static FakeForm FakeViewModel;

        Establish context = () =>
        {
            QueryHandler = new Mock<IQueryHandler>().Object;
            QueryHandler
                .WhenToldTo(x => x.BuildViewModel())
                .Return(new FakeForm());
        };
        
    }

    [Subject(typeof(QueryHandlerExtensions))]
    public class when_wrapping_view_model_build_by_query_handler_in_ViewResult : QueryHandlerExtensionsSpec<ViewResult>
    {
        Because of = () =>
            Result = QueryHandler.AndReturnViewResult();

        It should_have_the_view_model_as_the_one_built_by_query_handler = () =>
            Result.Model.ShouldNotBeTheSameAs(FakeViewModel);

    }



    [Subject(typeof(QueryHandlerExtensions))]
    public class when_wrapping_view_model_build_by_query_handler_in_partial_view_result : QueryHandlerExtensionsSpec<PartialViewResult>
    {
        Because of = () =>
            Result = QueryHandler.AndReturnPartialViewResult();

        It should_have_the_view_model_as_the_one_built_by_query_handler = () =>
            Result.Model.ShouldNotBeTheSameAs(FakeViewModel);

    }

    [Subject(typeof(QueryHandlerExtensions))]
    public class when_wrapping_view_model_build_by_query_handler_in_Json_result : QueryHandlerExtensionsSpec<JsonResult>
    {
        Because of = () =>
            Result = QueryHandler.AndReturnJsonResult();

        It should_have_the_view_model_as_the_one_built_by_query_handler = () =>
            Result.Data.ShouldNotBeTheSameAs(FakeViewModel);

    }
}