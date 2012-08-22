using Machine.Specifications;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Data.EntityFrameworkProvider.Repositories;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider.Repositories
{
    [Subject(typeof(TasksRepository))]
    public class when_saving_an_updated_Task : DatabaseSpec
    {
        static TasksRepository _repository;
        static Task _updatedTask;

        Establish context = () =>
            {
                _repository = new TasksRepository(Context);
                _updatedTask = _repository.Save(new TaskBuilder());
                _updatedTask.SetValue(x => x.Description, "My updated task description");
            };

        Because of = () =>
            _repository.Save(_updatedTask);

        It should_persist_updated_task_description = () =>
            _repository.Get(_updatedTask.Id).Description.ShouldEqual("My updated task description");
    }
}