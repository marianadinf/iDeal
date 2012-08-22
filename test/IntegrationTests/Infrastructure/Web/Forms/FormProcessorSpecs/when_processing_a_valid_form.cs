using FluentValidation;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Infrastructure.ObjectMapping;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.Forms.FormProcessorSpecs
{
    public class when_processing_a_valid_form : form_processor_specification
    {
        Because of = () =>
            Result = Subject.Execute(Form);

        It should_return_success_status = () =>
            Result.IsSuccessFull.ShouldBeTrue();

        It should_ask_the_validator_factory_for_a_validator_for_the_form = () =>
            The<IValidatorFactory>().WasToldTo(x => x.GetValidator<FakeForm>());

        It should_map_the_form_to_a_command = () =>
            The<IModelMapper>().WasToldTo(x => x.MapFormToCommand(Form));

        It should_ask_the_command_invoker_to_execute_the_command = () =>
            The<ICommandInvoker>().WasToldTo(x => x.Execute(Command));

        It should_log_the_result = () =>
            MockLogger.Verify(x => x.Info(Result.ToString()));
    }
}