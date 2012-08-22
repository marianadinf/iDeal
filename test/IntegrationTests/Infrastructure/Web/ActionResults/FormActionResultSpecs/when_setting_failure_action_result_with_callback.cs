using System.Web.Mvc;
using Machine.Specifications;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.ActionResults.FormActionResultSpecs
{
    public class when_setting_failure_action_result_with_callback : FormActionResult_specification
    {
        protected static string FORM_NAME = "Test Form";

        Because of = () =>
            Subject
                .WithFailureResultAndActionCallBack<FakeForm, JsonResult>(form => form.Data = "blah");

        It should_be_able_to_set_the_failure_action_result = () =>
            Subject.Failure.GetType().ShouldEqual(typeof(JsonResult));

        It should_run_action = () =>
            Subject.OnFailureWithPostActionResultShouldBeType<FakeForm, JsonResult>().Data.ShouldEqual("blah");
    }
}