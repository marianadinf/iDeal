using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder.Dates;
using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US001;
using UIT.iDeal.Domain.Model;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Tasks;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US001
{
    public class sc03_Add_an_undone_new_Task : us001_sc03<PostControllerScenario<TasksController, AddTaskForm>>
    {
        private IEnumerable<Task> _allSavedTaks;
        

        public override void AndGiven_there_I_am_prensented_with_add_new_task_form()
        {
            SUT.Form = new AddTaskForm();
        }

        public override void AndGiven_I_have_entered_the_Task_description()
        {
            SUT.Form.Description = _newTaskDescription;
        }

        public override void AndGiven_I_have_entered_a_date_when_the_tasked_needs_to_be_completed_by()
        {
            SUT.Form.ToBeCompletedByDate = The.Year(2012).On.May.The2nd;
        }

        public override void When_I_add_a_new_Task()
        {
            SUT.ExecuteAction(x => x.Create(SUT.Form));
        }

        public override void Then_I_should_be_redirected_to_the_Task_List()
        {
            SUT.ActionResult.ShouldBeRedirect<TasksController>(x => x.Index());
        }

        public override void And_There_should_be_4_Tasks_in_the_list()
        {
            _allSavedTaks = Database.Query<Task>().GetAll().ToList();
            _allSavedTaks.Count().Should().Be(4);
        }

        public override void And_the_newly_added_Task_is_not_done()
        {
            _allSavedTaks.Last().IsDone.Should().BeFalse();
        }

        public override void And_the_newly_added_Task_description_is_Drive_infrastructure_with_add_new_Task_scenario()
        {
            _allSavedTaks.Last().Description.Should().Be(_newTaskDescription);
        }
    }
}