using System;
using FluentValidation;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.UnitTests.Infrastructure.Commands
{
    public class when_processing_a_command_that_does_not_have_a_validator : command_invoker_specification
    {
        protected static Exception Exception;

        Establish context = () =>
            The<IValidatorFactory>()
                .WhenToldTo(x => x.GetValidator<FakeCommand>())
                .Return<IValidator<ICommand>>(null);

        Because of = () =>
            Result = Subject.Execute(Command);

        It should_return_success_status = () =>
            Result.IsSuccessFull.ShouldBeTrue();

        It should_have_no_error_message = () =>
            Result.Errors.ShouldBeEmpty();

        It should_begin_a_transaction = () =>
            The<IUnitOfWork>().WasToldTo(x => x.BeginTransaction());

        It should_commit_the_transaction = () =>
            The<IUnitOfWork>().WasToldTo(x => x.CommitTransaction());
    }
}