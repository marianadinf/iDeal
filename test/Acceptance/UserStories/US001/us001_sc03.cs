using System;
using System.Collections.Generic;
using UIT.iDeal.Common.Builders.Entities;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US001
{
    public abstract class us001_sc03<T> : ScenarioFor<T, us001_list_of_tasks>
        where T : class
    {
        protected List<Task> _tasks;
        protected string _newTaskDescription = "Drive infrastructure with add new Task scenario";

        protected us001_sc03() : base(3, "Add new undone task") {}

        public virtual void Given_there_are_3_tasks()
        {
            _tasks = new TaskBuilder(3);
            Database.SaveList(_tasks);
        }

        public abstract void AndGiven_there_I_am_prensented_with_add_new_task_form();

        public abstract void AndGiven_I_have_entered_the_Task_description();

        public abstract void AndGiven_I_have_entered_a_date_when_the_tasked_needs_to_be_completed_by();

        public abstract void When_I_add_a_new_Task();

        public abstract void Then_I_should_be_redirected_to_the_Task_List();

        public abstract void And_There_should_be_4_Tasks_in_the_list();

        public abstract void And_the_newly_added_Task_is_not_done();
        
        public abstract void And_the_newly_added_Task_description_is_Drive_infrastructure_with_add_new_Task_scenario();
    }
}