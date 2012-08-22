using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Data.EntityFrameworkProvider.Repositories;
using UIT.iDeal.Domain.Model;

namespace UIT.iDeal.IntegrationTests.Data.EntityFrameworkProvider.Repositories
{

    [Subject(typeof(TasksQuery))]
    public class when_retrieving_all_existing_tasks : DatabaseSpec
    {
        private static TasksQuery _repository;
        static List<Task> _existingTasks;
        private static IQueryable<Task> _allSavedTasks;

        Establish context = () =>
        {
            _existingTasks = new TaskBuilder(3);


            _existingTasks
                .Each(task =>
                {
                    task.SetValue(x => x.Id, Guid.NewGuid());
                    Context.CreateIncludedSet<Task>().Add(task);
                });

            Context.SaveChanges();
            _repository = new TasksQuery(Context);
        };

        Because of = () =>
            _allSavedTasks = _repository.GetAll();

       
        It should_retrieve_all_existing_tasks = () =>
            _allSavedTasks.ShouldContainOnly(_existingTasks);

    }
}
