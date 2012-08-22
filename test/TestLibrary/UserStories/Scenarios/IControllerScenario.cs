using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public interface IControllerScenario<TController> 
        where TController:BaseController
    {
        TController Controller { get; set; }
    }
}