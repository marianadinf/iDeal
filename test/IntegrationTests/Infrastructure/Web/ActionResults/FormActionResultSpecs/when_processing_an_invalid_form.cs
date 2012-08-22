using System.Linq;
using System.Web.Mvc;
using Machine.Specifications;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.TestLibrary.Extensions;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.ActionResults.FormActionResultSpecs
{
    public class when_processing_an_invalid_form : FormActionResult_specification
    {
        protected static string ERROR_PROPERTY = "propertyName";
        protected static string ERROR_MESSAGE = "Problem with 'propertyName'";
        protected static string FORM_NAME = "Test Form";

        protected static Moq.Mock<ActionResult> MockFailureResult;

        Establish context = () =>
            {
                Result.Add(MessageCategory.BrokenBusinessRule, ERROR_MESSAGE, ERROR_PROPERTY);
            MockFailureResult = new Moq.Mock<ActionResult>();
            Subject.Failure = MockFailureResult.Object;
            Subject.FailureActions.Add(() => Form.Name = FORM_NAME);
        };

        Because of = () =>
            Subject.ExecuteResult(Controller.ControllerContext);

        It should_execute_the_failure_action_result = () =>
        {
            Subject.ActionThatWasInvoked.ShouldEqual(Subject.Failure);
            MockFailureResult.Verify(x => x.ExecuteResult(Controller.ControllerContext));
        };

        It should_return_reasons_for_failure_as_errors = () =>
        {
            Subject.Result.Errors.Count().ShouldEqual(1);
            Controller.ModelState.Count.ShouldEqual(1);
        };

        It should_return_an_error_message_for_the_property_in_ModelState = () =>
        {
            Subject.Result.Errors.First().Value.PropertyName.ShouldEqual(ERROR_PROPERTY);
            Controller.ModelState.ShouldHaveErrorMessage(ERROR_PROPERTY, ERROR_MESSAGE);
        };

        It should_register_failure_actions = () =>
            Subject.FailureActions.Count.ShouldEqual(1);

        It should_run_failure_actions = () =>
            Subject.Form.Name.ShouldEqual(FORM_NAME);

    }
}