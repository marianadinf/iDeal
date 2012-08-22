using Castle.Core.Logging;
using Moq;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Infrastructure.ObjectMapping;
using UIT.iDeal.Infrastructure.Web;
using UIT.iDeal.TestLibrary.FakeObjects;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Web.Forms.FormProcessorSpecs
{
    public class form_processor_specification : WithSubject<FormProcessor>
    {
        protected static FakeForm Form;
        protected static ExecutionResult Result;
        protected static Mock<ILogger> MockLogger;
        protected static ExecutionResult CommandResult;
        protected static FakeCommand Command = new FakeCommand();
            
        Establish context = () =>
        {
            CommandResult = new ExecutionResult(Command);
            Form = new FakeForm();
            The<IModelMapper>()
                .WhenToldTo(x => x.MapFormToCommand(Form))
                .Return(Command);
            The<ICommandInvoker>()
                .WhenToldTo(x => x.Execute(Command))
                .Return(CommandResult);
            MockLogger = new Mock<ILogger>();
            Subject.Logger = MockLogger.Object;
        };

    }
}
