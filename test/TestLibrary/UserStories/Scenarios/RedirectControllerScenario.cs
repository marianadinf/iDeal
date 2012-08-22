using System.Web.Mvc;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public class RedirectControllerScenario<TController, TForm> : ControllerScenario<TController, RedirectToRouteResult, TForm>
        where TController : BaseController
    { }
}