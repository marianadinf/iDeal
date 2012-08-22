using System.Web.Mvc;
using System.Web.Routing;

namespace UIT.iDeal.Infrastructure.Security
{
    public class DenyBrowsersAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.ActionDescriptor.IsDefined(typeof (AllowBrowsersAttribute), true) ||
                    filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof (AllowBrowsersAttribute), true))
            {
                return;
            }

            var request = filterContext.HttpContext.Request;

            if (request == null)
                Redirect(filterContext);

            var userAgent = request.UserAgent;

            if (string.IsNullOrEmpty(userAgent) || !userAgent.Contains("Chrome"))
                Redirect(filterContext);
        }

        static void Redirect(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                            {"controller", "Errors"},
                            {"action", "Browser"}
                    });
        }
    }
}