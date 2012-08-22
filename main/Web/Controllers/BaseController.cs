using System.Web.Mvc;
using Seterlund.CodeGuard;
using UIT.iDeal.Common.Interfaces.Forms;
using UIT.iDeal.Common.Interfaces.Queries;
using UIT.iDeal.Infrastructure.QueryHandlers;
using UIT.iDeal.Infrastructure.Web.ActionResults;

namespace UIT.iDeal.Web.Controllers
{
    public class BaseController : Controller
    {
        public IFormProcessor FormProcessor { get; set; }
        public IQueryHandlerFactory QueryHandlerFactory { get; set; }

        protected FormActionResult<TForm> HandleForm<TForm>(TForm form)
            where TForm : class
        {
            return new FormActionResult<TForm>(form, FormProcessor)
                .WithFailureResult(View(form));
        }

        protected IQueryHandler HandleQueryWith<TQueryHandler>()
            where TQueryHandler : QueryHandler
        {
            return QueryHandlerFactory.Create<TQueryHandler>();
        }

        
        /// <summary>
        /// Return a view result with an auto mapped view model.
        /// Applies for simple one to one mapping from a domain to a view Model
        /// </summary>
        /// <typeparam name="TViewModel">any view model</typeparam>
        /// <param name="domain">any domain entity</param>
        /// <returns>An auto mapped view result</returns>
        protected AutoMappedViewResult AutoMappedView<TViewModel>(object domain)
            where TViewModel : class
        {
            Guard.That(domain).IsNotNull();
            ViewData.Model = domain;

            return
                new AutoMappedViewResult(typeof (TViewModel))
                {
                    ViewData = ViewData,
                    TempData = TempData
                };

        }
        
        /// <summary>
        /// Return a json result with an auto mapped view model.
        /// Applies for simple one to one mapping from a domain to a view Model
        /// </summary>
        /// <typeparam name="TViewModel">any view model to be serialsed as Json</typeparam>
        /// <param name="domain">any domain entity</param>
        /// <returns>An auto mapped Json result</returns>
        protected AutoMappedJsonResult AutoMappedJsonResult<TViewModel>(object domain)
            where TViewModel : class
        {
            return new AutoMappedJsonResult(typeof(TViewModel), domain);
        }

        
    }
}