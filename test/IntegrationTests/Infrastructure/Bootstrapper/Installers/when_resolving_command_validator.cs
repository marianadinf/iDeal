using Castle.Windsor;
using FluentValidation;
using Machine.Specifications;
using UIT.iDeal.Commands.AddTask;
using UIT.iDeal.Infrastructure.Bootstrapper.Installers;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Bootstrapper.Installers
{
    [Subject("Given a Windsor container with fluent validators")]
    public class when_resolving_command_validator
    {
        protected static IWindsorContainer container;
        protected static IValidator<AddTaskCommand> SUT;

        Establish context = () =>
            container = new WindsorContainer().Install(new FluentValidationInstaller());

        Because of = () =>
            SUT = container.Resolve<IValidator<AddTaskCommand>>();

        It should_return_the_correct_validator = () =>
            SUT.ShouldBeOfType<AddTaskCommandValidator>();
    }
}