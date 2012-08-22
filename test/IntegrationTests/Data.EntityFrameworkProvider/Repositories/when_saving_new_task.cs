using System;
using Machine.Specifications;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Data.EntityFrameworkProvider.Repositories;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider.Repositories
{
    [Subject(typeof(TasksRepository))]
    public class when_saving_a_new_task : DatabaseSpec
    {
        static TasksRepository _repository;
        static Task _newTaskToBeSaved;

        Establish context = () =>
        {
            _repository = new TasksRepository(Context);
            _newTaskToBeSaved = new TaskBuilder();
        };

        Because of = () => 
            _repository.Save(_newTaskToBeSaved);

        It should_generate_new_id = () =>
             _newTaskToBeSaved.Id.ShouldNotEqual(Guid.Empty);

      
        It should_persist_new_task = () =>
            _repository.Get(_newTaskToBeSaved.Id).ShouldNotBeNull();
    }
}