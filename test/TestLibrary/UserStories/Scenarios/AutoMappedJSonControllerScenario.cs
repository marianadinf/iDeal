using System;
using System.Web.Mvc;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public class AutoMappedJSonControllerScenario<TController,TModel>
        : AutoMappedActionResultControllerScenario<TController, AutoMappedJsonResult, TModel>
        where TController : BaseController
    { }
}