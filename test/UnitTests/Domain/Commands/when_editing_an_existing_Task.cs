using System;
using Machine.Fakes;
using Machine.Specifications;
using Moq;
using UIT.iDeal.Commands.EditTask;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Domain.Model;
using It = Machine.Specifications.It;

namespace UIT.iDeal.UnitTests.Domain.Commands
{
    public class when_editing_an_existing_Task : WithSubject<EditTaskCommandHanlder>
    {
        protected static Guid _taskId = Guid.NewGuid();
        protected static Task _task = new Mock<Task>().Object;

        Establish context = () =>
        {
            Subject.Command =
                new EditTaskCommand
                {
                    Id = _taskId,
                    Description = "Updated description",
                    IsDone = true,
                    ToBeCompletedByDate = new DateTime(2012, 05, 02)
                };

            The<ITasksRepository>()
                .WhenToldTo(x => x.Get(_taskId))
                .Return(_task);
        };

        Because of = () =>
            Subject.Handle();

        It should_ask_the_repository_to_retrieve_the_task_to_edit = () =>
            The<ITasksRepository>().WasToldTo(x => x.Get(_taskId));

        It should_ask_the_Task_Domain_to_Edit_the_task = () =>
            _task.WasToldTo(x => x.Edit(Subject.Command.Description ,Subject.Command.IsDone, Subject.Command.ToBeCompletedByDate));

        It should_ask_the_repository_to_save_updated_Task = () =>
            The<ITasksRepository>().WasToldTo(x => x.Save(_task));
    }
}