using System;
using System.Collections.Generic;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US001
{
    public abstract class us001_sc01<T> : ScenarioFor<T, us001_list_of_tasks>
        where T : class
    {
        protected List<Task> _tasks;


        protected us001_sc01() : base(1, "View a list of tasks") {}

        public virtual void Given_there_are_3_tasks()
        {

            _tasks = new TaskBuilder(3);
            Database.SaveList(_tasks);
        }

        public abstract void When_I_view_the_Task_list();

        public abstract void Then_there_should_be_3_Tasks_in_the_list();

        public abstract void And_I_should_see_the_Description_and_whether_each_Task_is_done_or_not();

    }
}