using FluentAssertions;
using UIT.iDeal.Acceptance.UserStories.US001;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.TestLibrary.UserStories.Scenarios;
using UIT.iDeal.ViewModel.Tasks;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Acceptance.ExecutableSpecifications.UserStories.US001
{
    public class sc02_Navigate_to_Add_new_Task_Form : us001_sc02<ViewControllerScenario<TasksController, AddTaskForm>>
    {
        public override void When_I_click_on_add_a_new_Task()
        {
            SUT.ExecuteAction(x => x.Create());
        }

        public override void Then_I_should_be_presented_with_a_add_new_task_form()
        {
            SUT.ActionResult.ShouldBeDefaultViewForAction();
        }
    }
}