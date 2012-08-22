using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public class AutoMappedViewControllerScenario<TController,TModel>
        : AutoMappedActionResultControllerScenario<TController, AutoMappedViewResult, TModel> 
        where TController : BaseController
        where TModel:class 
    { }
}