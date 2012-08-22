using System;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Machine.Specifications;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Infrastructure.Bootstrapper.Installers;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Bootstrapper.Installers
{
    public class when_requesting_a_handler_for_a_command_with_a_handler : command_handler_specification
    {
        Because of = () =>
            result = SUT.HandlerForCommand(new CommandWithHandler());

        It should_return_the_correct_handler = () =>
            result.GetType().CanBeCastTo<ICommandHandler<CommandWithHandler>>();

        It should_handle_the_correct_command = () =>
            (result as ICommandHandler<CommandWithHandler>).Command.ShouldBeOfType<CommandWithHandler>();
    }

    public class when_requesting_a_handler_for_a_command_without_a_handler : command_handler_specification
    {
        protected static Exception exception;

        Because of = () =>
            exception = Catch.Exception(() => SUT.HandlerForCommand(new CommandWithoutHandler()));

        It should_throw_a_ComponentNotFoundException = () =>
            exception.ShouldBeOfType<ComponentNotFoundException>();
    }

    [Subject("Given a Windsor container with command handlers")]
    public abstract class command_handler_specification
    {
        protected static IWindsorContainer container;
        protected static ICommandHandlerFactory SUT;
        protected static ICommandHandler result;

        Establish context = () =>
        {
            var commandHandlerInstaller = new CommandHandlerInstaller() { TypesToRegister = AllTypes.FromThisAssembly() };
            container = new WindsorContainer().Install(commandHandlerInstaller);
            SUT = container.Resolve<ICommandHandlerFactory>();
        };
    }

    public class CommandWithHandlerHandler : ICommandHandler<CommandWithHandler>
    {
        public void Handle()
        {
            throw new System.NotImplementedException();
        }

        public CommandWithHandler Command { get; set; }
    }

    public class CommandWithHandler : ICommand
    {
    }

    public class CommandWithoutHandler : ICommand
    {
    }
}
