using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UIT.iDeal.Web.Notification
{
    public class AjaxMessagesFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
           if(filterContext.HttpContext.Request.IsAjaxRequest())
           {
               var viewData = filterContext.Controller.ViewData;
               var response = filterContext.HttpContext.Response;

               foreach(var messageType in Enum.GetNames(typeof(MessageType)))
               {
                   var message = viewData.ContainsKey(messageType) ? viewData[messageType] : null;
                   if(message!=null)
                   {
                       response.AddHeader("X-Message-Type", messageType);
                       response.AddHeader("X-Message", message.ToString());
                       return;
                   }

               }
           }
        }
    }
}