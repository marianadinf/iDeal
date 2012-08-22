using System.Web.Mvc;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public class ViewControllerScenario<TController, TModel>
        : ControllerScenario<TController, ViewResult, TModel>
        where TController : BaseController
        where TModel : class
    { }
}