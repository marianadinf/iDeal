using System;
using Machine.Specifications;
using UIT.iDeal.Common.Interfaces.Commands;

namespace UIT.iDeal.UnitTests.Infrastructure.Commands
{
    public class when_processing_a_null_command : command_invoker_specification
    {
        private static Exception Exception;
        private static ICommand NullCommand;

        Because of = () =>
            Exception = Catch.Exception(() => Subject.Execute(NullCommand));

        It should_throw_an_ArgumentNullException = () =>
            Exception.ShouldBeOfType<ArgumentNullException>();
    }
}