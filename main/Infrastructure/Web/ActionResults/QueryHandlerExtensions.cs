using System.Web.Mvc;
using UIT.iDeal.Common.Interfaces.Queries;

namespace UIT.iDeal.Infrastructure.Web.ActionResults
{
    public static class QueryHandlerExtensions
    {
        
        public static ViewResult AndReturnViewResult(this IQueryHandler queryHandler) 
        {
            return new ViewResult {ViewData = new ViewDataDictionary(queryHandler.BuildViewModel())};
        }

        public static PartialViewResult AndReturnPartialViewResult(this IQueryHandler queryHandler)
        {
            return new PartialViewResult { ViewData = new ViewDataDictionary(queryHandler.BuildViewModel()) };
        }


        public static JsonResult AndReturnJsonResult(this IQueryHandler queryHandler, JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.DenyGet)
        {
            return new JsonResult { Data = queryHandler.BuildViewModel(), JsonRequestBehavior = jsonRequestBehavior };

        }
    }
}