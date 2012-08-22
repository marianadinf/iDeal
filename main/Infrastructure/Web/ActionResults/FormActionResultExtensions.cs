using System;
using System.Web.Mvc;

namespace UIT.iDeal.Infrastructure.Web.ActionResults
{
    public static class FormActionResultExtensions
    {
        /// <summary>
        /// The result that should be executed if action succeeds. Usually a RedirectToAction.
        /// </summary>
        public static FormActionResult<TForm> WithSuccessResult<TForm>(this FormActionResult<TForm> result, ActionResult successResult)
            where TForm : class
        {
            result.Success = successResult;
            return result;
        }

        /// <summary>
        /// The result that should be executed if action fails. Should only be called for partials.
        /// </summary>
        public static FormActionResult<TForm> WithFailureResult<TForm>(this FormActionResult<TForm> result, ActionResult failureResult)
            where TForm : class
        {
            result.Failure = failureResult;
            return result;
        }

        /// <summary>
        /// The result that should be executed if action fails. 
        /// create an instance of the type of action passed
        /// </summary>
        /// <typeparam name="TForm">Form to be handled</typeparam>
        /// <typeparam name="TAction">type of action invoked in case of failure</typeparam>
        /// <param name="result">form action result to be fluently chained</param>
        /// <param name="failureResultCallBack">action accepting the action</param>
        /// <returns>form action result to be fluently chained</returns>
        public static FormActionResult<TForm> WithFailureResultAndActionCallBack<TForm, TAction>(this FormActionResult<TForm> result, Action<TAction> failureResultCallBack)
            where TForm : class
            where TAction : ActionResult, new()
        {
            var actionResult = new TAction();
            result.Failure = actionResult;
            result.FailureActions.Add(() => failureResultCallBack(actionResult));
            return result;
        }

        

    }
}