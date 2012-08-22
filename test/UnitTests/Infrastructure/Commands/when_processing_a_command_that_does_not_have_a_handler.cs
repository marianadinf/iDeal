using System;
using System.Linq;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Infrastructure.Commands;

namespace UIT.iDeal.UnitTests.Infrastructure.Commands
{
    public class when_processing_a_command_that_does_not_have_a_handler : command_invoker_specification
    {
        protected static Exception Exception;

        Establish context = () =>
            The<ICommandHandlerFactory>()
                .WhenToldTo(x => x.HandlerForCommand(Command))
                .Return<ICommandHandler<ICommand>>(null);

        Because of = () =>
            Result = Subject.Execute(Command);

        It should_have_the_message_category_as_Fatal_Exception = () =>
            Result.Errors.First().Key.ShouldEqual(MessageCategory.FatalException);

        It should_have_the_error_message_Command_handler_not_found_for_command_type = () =>
            Result.AllErrorMessages.First().ShouldEqual("Command handler not found for command type: FakeCommand");

    }
}