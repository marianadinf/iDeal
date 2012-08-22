using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UIT.iDeal.Common.Errors;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Interfaces.Forms;

namespace UIT.iDeal.Infrastructure.Web.ActionResults
{
    public class FormActionResult<TForm> : ActionResult
        where TForm : class
    {
        private readonly IFormProcessor _formProcessor;

        public TForm Form { get; private set; }
        public ExecutionResult Result { get; set; }
        public ActionResult Success { get; set; }
        public ActionResult Failure { get; set; }
        public ActionResult ActionThatWasInvoked { get; set; }
        public IList<Action> FailureActions { get; set; }

        public FormActionResult(TForm form, IFormProcessor formProcessor)
        {
            _formProcessor = formProcessor;
            Form = form;
            FailureActions = new List<Action>();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            Execute();
            if (!Result.IsSuccessFull)
            {
                context.Controller.ViewData.ModelState.MergeWith(Result);
                FailureActions.Each(action => action());
            }
            ActionThatWasInvoked.ExecuteResult(context);
        }

        public ExecutionResult Execute()
        {
            Result = _formProcessor.Execute(Form);
            ActionThatWasInvoked = Result.IsSuccessFull ? Success : Failure;
            return Result;
        }
    }

}