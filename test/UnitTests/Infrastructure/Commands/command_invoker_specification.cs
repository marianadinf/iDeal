using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Machine.Fakes;
using Machine.Specifications;
using Moq;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Infrastructure.Commands;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.UnitTests.Infrastructure.Commands
{
    public class command_invoker_specification : WithSubject<CommandInvoker>
    {
        protected static FakeCommand Command = new FakeCommand();
        protected static ExecutionResult Result;
        protected static AbstractValidator<FakeCommand> FailingCommandValidator = new Mock<AbstractValidator<FakeCommand>>().Object;
        protected static ICommandHandler<FakeCommand> FailingCommandHandler = new Mock<ICommandHandler<FakeCommand>>().Object;

        Establish context = () =>
        {
            The<IValidatorFactory>()
                .WhenToldTo(x => x.GetValidator<FakeCommand>())
                .Return(new TestCommandValidator());

            The<ICommandHandlerFactory>()
                .WhenToldTo(x => x.HandlerForCommand(Command))
                .Return(new TestCommandHandler());
            
            FailingCommandValidator
                .WhenToldTo(x => x.Validate(Moq.It.IsAny<FakeCommand>()))
                .Throw(new ValidationException(new List<ValidationFailure>
                                                   {
                                                       new ValidationFailure("Some Property","Validation failure occured")
                                                   }));

            FailingCommandHandler
                .WhenToldTo(x => x.Handle())
                .Throw(new BusinessRuleException("property", "Business rule exception occured"));

        };
    }
}
