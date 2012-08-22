using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US001;
using UIT.iDeal.TestLibrary.Browsers;
using UIT.iDeal.TestLibrary.Browsers.PageObjects.Task;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.FunctionalTests.UserStories.US001
{
    public class sc01_View_a_list_of_Tasks : us001_sc01<BrowserScenario>
    {
        TaskListPage _taskList;

        
        public override void When_I_view_the_Task_list()
        {
            _taskList = SUT.NavigateToInitialPage<TaskListPage, TasksController>(x => x.Index());
        }

        public override void Then_there_should_be_3_Tasks_in_the_list()
        {
            _taskList.Grid.NumberOfRows.Should().Be(3);

        }

        public override void And_I_should_see_the_Description_and_whether_each_Task_is_done_or_not()
        {
            _tasks.ShouldBeEquivalentTo(_taskList.Grid, x => new {x.Description, x.IsDone});

        }
    }

}