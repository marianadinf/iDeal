using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US001;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Tasks;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US001
{
    public class sc01_View_a_list_of_Tasks : us001_sc01<AutoMappedViewControllerScenario<TasksController, IEnumerable<TaskItemViewModel>>>
    {

        public override void When_I_view_the_Task_list()
        {
            SUT.ExecuteAction(x => x.Index());
        }

        public override void Then_there_should_be_3_Tasks_in_the_list()
        {
            SUT.ViewModel.Count().Should().Be(3);
        }

        public override void And_I_should_see_the_Description_and_whether_each_Task_is_done_or_not()
        {
            SUT.ViewModel.ShouldBeEquivalentTo(_tasks, transformWith: x => new { x.Id, x.Description, x.IsDone });
        }

       
    }
}