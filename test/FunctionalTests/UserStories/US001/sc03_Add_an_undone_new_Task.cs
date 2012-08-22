using System;
using System.Linq;
using FizzWare.NBuilder.Dates;
using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US001;
using UIT.iDeal.TestLibrary.Browsers;
using UIT.iDeal.TestLibrary.Browsers.PageObjects.Task;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.FunctionalTests.UserStories.US001
{
    public class sc03_Add_an_undone_new_Task : us001_sc03<BrowserScenario>
    {
        AddTaskPage _addTaskPage;
        TaskListPage _taskListPage;

        public override void AndGiven_there_I_am_prensented_with_add_new_task_form()
        {
            _addTaskPage = SUT.NavigateToInitialPage<AddTaskPage, TasksController>(x => x.Create());
        }

        public override void AndGiven_I_have_entered_the_Task_description()
        {
            _addTaskPage.EnterDescription(_newTaskDescription);
        }

        public override void AndGiven_I_have_entered_a_date_when_the_tasked_needs_to_be_completed_by()
        {
            _addTaskPage.EnterDateToBeCompletedBy(The.Year(2012).On.May.The2nd);
        }

        public override void When_I_add_a_new_Task()
        {
            _taskListPage = _addTaskPage.ClickOnSave();
        }

        public override void Then_I_should_be_redirected_to_the_Task_List()
        {
            _taskListPage.Should().BeCurrentBrowserPage();
        }

        public override void And_There_should_be_4_Tasks_in_the_list()
        {
            _taskListPage.Grid.NumberOfRows.Should().Be(4);
        }

        public override void And_the_newly_added_Task_is_not_done()
        {
            _taskListPage.Grid.Last().IsDone.Should().BeFalse();
        }

        public override void And_the_newly_added_Task_description_is_Drive_infrastructure_with_add_new_Task_scenario()
        {
            _taskListPage.Grid.Last().Description.Should().Be(_newTaskDescription);
        }
    }
}