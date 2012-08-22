using System.Web.Mvc;
using Machine.Specifications;
using UIT.iDeal.Infrastructure.Web.ActionResults;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.ActionResults.FormActionResultSpecs
{
    public class when_setting_action_results_with_extension_methods : FormActionResult_specification
    {
        protected static ActionResult SuccessResult = new ViewResult();
        protected static ActionResult FailureResult = new HttpNotFoundResult();

        Because of = () =>
            Subject
                .WithSuccessResult(SuccessResult)
                .WithFailureResult(FailureResult);

        It should_be_able_to_set_the_success_action_result = () =>
            Subject.Success.ShouldEqual(SuccessResult);

        It should_be_able_to_set_the_failure_action_result = () =>
            Subject.Failure.ShouldEqual(FailureResult);
    }
}