using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Interfaces.ObjectMapping;
using UIT.iDeal.Common.ObjectMapping;
using UIT.iDeal.Domain.Model.Base;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.TestLibrary.Extensions;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public class PostControllerScenario<TController, TForm>
        : ControllerScenario<TController, ActionResult, TForm>
        where TController : BaseController
        where TForm : class
    {
        readonly IModelMapper _modelMapper;
        public TForm Form { get; set; }
        public ExecutionResult CommandResult { get; private set; }

        public PostControllerScenario(IModelMapper modelMapper)
        {
            _modelMapper = modelMapper;
        }

        public override void ExecuteAction(Func<TController, ActionResult> func)
        {
            base.ExecuteAction(func);
            CommandResult = Controller.FormProcessor.Execute(Form); ;
            var result = (FormActionResult<TForm>)ActionResult;
            ActionResult = CommandResult.IsSuccessFull ? result.Success : result.Failure;

        }

        public void CreateFormUsing<TEntity>(TEntity entity)
            where TEntity : Entity
        {
            Form = _modelMapper.CreateInstance<TEntity, TForm>(entity);
        }
      
        public void PropertyShouldHaveError(Expression<Func<TForm, object>> property, ErrorType type)
        {
            CommandResult.ShouldHaveError(property, type);
        }

    }
}