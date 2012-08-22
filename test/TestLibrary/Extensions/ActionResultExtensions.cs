using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.TestLibrary.Extensions
{
    public static class ActionResultExtensions
    {
        public static ViewResult ShouldBeDefaultViewForAction(this ActionResult actionResult)
        {
            return actionResult.AssertViewRendered().ForView(string.Empty);
        }

        public static ViewResult ShouldBeViewNamed(this ActionResult actionResult, string viewName)
        {
            return actionResult.AssertViewRendered().ForView(viewName);
        }

        public static PartialViewResult ShouldBePartialViewNamed(this ActionResult actionResult, string partialViewName)
        {
            return actionResult.AssertPartialViewRendered().ForView(partialViewName);
        }

        public static RedirectToRouteResult ShouldBeRedirect<TController>(this ActionResult actionResult, Expression<Action<TController>> action) where TController : IController
        {
            return actionResult.AssertActionRedirect().ToAction(action);
        }

        public static T ShouldHaveModelOfType<T>(this ActionResult actionResult)
        {
            return actionResult.AssertViewRendered().WithViewData<T>();
        }

        public static TActionResult OnFailureWithPostActionResultShouldBeType<TForm, TActionResult>(this ActionResult actionResult)
            where TForm : class
            where TActionResult : ActionResult
        {
            var formActionResult = actionResult.TypeShouldBe<FormActionResult<TForm>>();

            formActionResult.FailureActions.Each( action => action.Invoke());

            return formActionResult.Failure.TypeShouldBe<TActionResult>();
        }

        public static ViewResult OnFailureShouldStayOnDefaultViewForAction<TForm>(this ActionResult actionResult)
            where TForm : class
        {
            var formActionResult = actionResult.TypeShouldBe<FormActionResult<TForm>>();

            formActionResult.FailureActions.Each(action => action.Invoke());

            return formActionResult.Failure.ShouldBeDefaultViewForAction();
        }


        public static TActionResult OnSuccessWithPostActionResultShouldBeType<TForm, TActionResult>(this ActionResult actionResult)
            where TForm : class
            where TActionResult : ActionResult
        {
            var formActionResult = actionResult.TypeShouldBe<FormActionResult<TForm>>();
            
            return formActionResult.Success.TypeShouldBe<TActionResult>();
        }

        public static RedirectToRouteResult OnSuccessShouldRedirectTo<TForm, TController>(this ActionResult actionResult, Expression<Action<TController>> actionToBeRedirectedTo)
            where TForm : class
            where TController : BaseController
        {
            var formActionResult = actionResult.TypeShouldBe<FormActionResult<TForm>>();

            return
                formActionResult
                    .Success
                    .TypeShouldBe<RedirectToRouteResult>()
                    .ShouldBeRedirect(actionToBeRedirectedTo);
        }


        public static TForm OnSuccessWithPostJsonResultDataShouldBeType<TForm>(this ActionResult actionResult)
            where TForm : class
        {
            var formActionResult = actionResult.TypeShouldBe<FormActionResult<TForm>>();

            return formActionResult.Success.TypeShouldBe<JsonResult>().Data.TypeShouldBe<TForm>();
        }

        public static JsonResult OnFailureWithPostActionResultShouldBeJsonResultFor<TForm>(this ActionResult actionResult)
            where TForm : class
        {
            var formActionResult = actionResult.TypeShouldBe<FormActionResult<TForm>>();

            return formActionResult.Success.TypeShouldBe<JsonResult>();
        }

        public static List<String> OnFailureWithPostJsonResultShouldReturnFor<TForm>(this ActionResult actionResult)
            where TForm : class
        {
            var formActionResult = actionResult.TypeShouldBe<FormActionResult<TForm>>();

            return formActionResult.Failure.TypeShouldBe<JsonResult>().Data.TypeShouldBe<List<string>>();
        }

    }
}
