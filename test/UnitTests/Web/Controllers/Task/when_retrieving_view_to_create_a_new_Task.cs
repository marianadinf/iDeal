using System.Web.Mvc;
using Machine.Fakes;
using Machine.Specifications;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.ViewModel.Tasks;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.UnitTests.Web.Controllers.Task
{
    public class when_retrieving_view_to_create_a_new_Task : WithSubject<TasksController>
    {
        private static ViewResult _viewResult;
        private static AddTaskForm _emptyFormModel = new AddTaskForm();
        
        Because of = () =>
            _viewResult = Subject.Create();

        It should_return_a_create_view_result = () =>
            _viewResult.ShouldBeDefaultViewForAction();

        It should_return_an_add_task_form_model = () =>
            _viewResult.Model.ShouldBeOfType<AddTaskForm>();

        It should_have_the_form_model_empty = () =>
            _viewResult.Model.ShouldBeEquivalentToModel(_emptyFormModel);

    }
}