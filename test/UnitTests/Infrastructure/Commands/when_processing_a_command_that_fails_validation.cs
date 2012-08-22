using System;
using System.Linq;
using FluentValidation;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.UnitTests.Infrastructure.Commands
{
    public class when_processing_a_command_that_fails_validation : command_invoker_specification
    {
        protected static Exception Exception;

        Establish context = () =>
            The<IValidatorFactory>()
                .WhenToldTo(x => x.GetValidator<FakeCommand>())
                .Return(FailingCommandValidator);


        Because of = () =>
            Result = Subject.Execute(Command);

        It should_not_return_success_status = () =>
            Result.IsSuccessFull.ShouldBeFalse();

        It should_have_one_error = () =>
            Result.Errors.Count().ShouldEqual(1);

        It should_have_the_error_message_Validation_failure_occured = () =>
            Result.AllErrorMessages.First().ShouldEqual("Validation failure occured");

        It should_have_the_message_category_as_Business_rule = () =>
            Result.Errors.First().Key.ShouldEqual(MessageCategory.BrokenBusinessRule);

        It should_begin_a_transaction = () =>
            The<IUnitOfWork>().WasToldTo(x => x.BeginTransaction());

        It should_rollback_the_transaction = () =>
            The<IUnitOfWork>().WasToldTo(x => x.RollbackTransaction());
    }
}