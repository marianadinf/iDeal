using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US001;
using UIT.iDeal.TestLibrary.Browsers;
using UIT.iDeal.TestLibrary.Browsers.PageObjects.Task;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.Web.Controllers;
using UIT.iDeal.TestLibrary.Browsers.PageObjects.Framework;

namespace UIT.iDeal.Acceptance.FunctionalTests.UserStories.US001
{
    public class sc02_Navigate_to_Add_new_Task_Form : us001_sc02<BrowserScenario>
    {
        TaskListPage _taskListPage;
        AddTaskPage _addTaskPage;

        public override void Given_I_am_presented_with_a_list_of_many_Tasks()
        {
            base.Given_I_am_presented_with_a_list_of_many_Tasks();
            _taskListPage = SUT.NavigateToInitialPage<TaskListPage, TasksController>(x => x.Index());
        }
        public override void When_I_click_on_add_a_new_Task()
        {
            _addTaskPage = _taskListPage.ClickOnAddTask();
        }

        public override void Then_I_should_be_presented_with_a_add_new_task_form()
        {
            _addTaskPage.Should().BeCurrentBrowserPage();
        }
    }

    

  
}