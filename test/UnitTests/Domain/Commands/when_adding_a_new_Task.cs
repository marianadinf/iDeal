using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.Commands.AddTask;
using UIT.iDeal.Common.Interfaces.Commands;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model;
using It = Machine.Specifications.It;

namespace UIT.iDeal.UnitTests.Domain.Commands
{
    public class when_adding_a_new_Task : WithSubject<AddTaskCommandHandler>
    {
        Establish context = () =>
            Subject.Command = new AddTaskCommand {Description = "New Task Added"};

        Because of = () =>
            Subject.Handle();

        It should_ask_repository_to_save_the_new_task = () =>
            The<ITasksRepository>().WasToldTo(x => x.Save(Moq.It.IsAny<Task>()));

    }
}