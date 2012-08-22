using FluentValidation;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.UnitTests.Infrastructure.Commands
{
    public class when_processing_a_valid_command : command_invoker_specification
    {
        Because of = () =>
            Result = Subject.Execute(Command);

        It should_return_success_status = () =>
            Result.IsSuccessFull.ShouldBeTrue();

        It should_ask_the_validator_factory_for_a_validator_for_the_command = () =>
            The<IValidatorFactory>().WasToldTo(x => x.GetValidator<FakeCommand>());

        It should_ask_the_command_handler_factory_for_a_handler_for_the_command = () =>
            The<ICommandHandlerFactory>().WasToldTo(x => x.HandlerForCommand(Command));

        It should_begin_a_transaction = () =>
            The<IUnitOfWork>().WasToldTo(x => x.BeginTransaction());

        It should_commit_the_transaction = () =>
            The<IUnitOfWork>().WasToldTo(x => x.CommitTransaction());
    }
}