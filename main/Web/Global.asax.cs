using System;
using UIT.iDeal.Infrastructure.Bootstrapper;
using System.Web.Mvc;
using FluentSecurity;
using System.Web.Routing;
using System.Collections.Generic;
using System.Web;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            ApplicationConfigurator.BuildContainer();
            ApplicationConfigurator.Start();
        }

        protected void Application_End(Object sender, EventArgs e)
        {
            ApplicationConfigurator.End();
        }
        
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }


        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }
    }
}