using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UIT.iDeal.Common.Interfaces.Data;
using UIT.iDeal.Common.Interfaces.Queries;
using UIT.iDeal.Infrastructure.QueryHandlers;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.ViewModel.Tasks;
using MvcContrib;


namespace UIT.iDeal.Web.Controllers
{
    public class TasksController : BaseController
    {
        private readonly ITasksQuery _query;

        public TasksController(ITasksQuery query)
        {
            _query = query;
        }

        
        public AutoMappedViewResult Index()
        {
            var tasks = _query.GetAll();

            return AutoMappedView<IEnumerable<TaskItemViewModel>>(tasks);
        }


        [HttpGet]
        public ViewResult Create()
        {
            return View(new AddTaskForm());
        }


        [HttpPost]
        public ActionResult Create(AddTaskForm addTaskForm)
        {
            return
                HandleForm(addTaskForm)
                    .WithSuccessResult(this.RedirectToAction(x => x.Index()));
        }

        [HttpPost]
        public ActionResult Edit(EditTaskForm editTaskForm)
        {
           return HandleForm(editTaskForm)
               .WithSuccessResult(this.RedirectToAction(x => x.Index()));
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var task =  _query.Get(id);
            return AutoMappedView<EditTaskForm>(task);
        }

        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            var task = _query.Get(id);
            return AutoMappedView<TaskItemViewModel>(task);
        }
        [HttpPost]
        public ActionResult Delete(TaskItemViewModel deleteTask)
        {
            return HandleForm(deleteTask).WithSuccessResult(this.RedirectToAction(x => x.Index()));
        }

    }
}