using System.Web.Mvc;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.ViewModel.Tasks;
using UIT.iDeal.Web.Controllers;
using UIT.iDeal.TestLibrary.Extensions;
namespace UIT.iDeal.UnitTests.Web.Controllers.Task
{
    public class when_posting_to_create_a_new_Task : WithSubject<TasksController>
    {
        private static ActionResult _actionResult;
        private static AddTaskForm _addForm;

        Establish context = () => 
            _addForm = new AddTaskForm();

        Because of = () =>
            _actionResult = Subject.Create(_addForm);

        It should_have_success_action_result_to_redirect_to_the_index_ = () =>
            _actionResult.OnSuccessShouldRedirectTo<AddTaskForm, TasksController>(x => x.Index());

        It should_have_failure_action_result_to_stay_on_the_create_view = () =>
            _actionResult.OnFailureShouldStayOnDefaultViewForAction<AddTaskForm>();
         
    }
}