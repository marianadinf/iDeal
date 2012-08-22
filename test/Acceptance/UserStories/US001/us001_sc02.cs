using System;
using System.Collections.Generic;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US001
{
    public abstract class us001_sc02<T> : ScenarioFor<T, us001_list_of_tasks>
        where T : class
    {
        protected List<Task> _tasks;

        protected us001_sc02() : base(2,"Navigate to Add new Task Form") { }

        public virtual void Given_I_am_presented_with_a_list_of_many_Tasks()
        {
            _tasks = new TaskBuilder(3);
            Database.SaveList(_tasks);
        }

        public abstract void When_I_click_on_add_a_new_Task();

        public abstract void Then_I_should_be_presented_with_a_add_new_task_form();
    }
}