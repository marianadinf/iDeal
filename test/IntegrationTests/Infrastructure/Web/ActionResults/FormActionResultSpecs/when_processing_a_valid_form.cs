using Machine.Specifications;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.ActionResults.FormActionResultSpecs
{
    public class when_processing_a_valid_form : FormActionResult_specification
    {
        Because of = () =>
            Subject.ExecuteResult(Controller.ControllerContext);

        It should_execute_the_success_result = () =>
            Subject.ActionThatWasInvoked.ShouldEqual(Subject.Success);
    }
}