using System.Collections.Generic;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;

namespace UIT.iDeal.Acceptance.UserStories.US000
{
    public abstract class us000_sc04<T>:ScenarioFor<T, us000_list_of_tasks> 
        where T:class
    {
        protected IList<Task> _tasks;
        protected us000_sc04():base(4, "Delete a task")
        {
        }
        public virtual void Given_there_are_3_tasks()
        {
            //_tasks = Database.SaveList(new TaskBuilder(persistable: true).CreateMany());
        }

        public abstract void When_I_delete_a_task();
        public abstract void Then_I_should_be_redirected_to_the_Task_List();

        public abstract void And_There_should_be_2_Tasks_in_the_list();
    }
}