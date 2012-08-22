using System.Web.Mvc;
using UIT.iDeal.Infrastructure.Web.ActionResults;
using UIT.iDeal.Web.Controllers;

namespace UIT.iDeal.TestLibrary.UserStories.Scenarios
{
    public abstract class AutoMappedActionResultControllerScenario<TController, TActionResult, TModel>
        : ControllerScenario<TController, TActionResult, TModel>
        where TController : BaseController
        where TActionResult : ActionResult, IAutoMapViewModel

    {
        public override void BuildViewModel()
        {
            ViewModel = (TModel) ActionResult.BuildViewModel();
        }

    }
}