using System.Web.Mvc;
using Castle.Windsor;
using Seterlund.CodeGuard;
using UIT.iDeal.Infrastructure.Bootstrapper.Factories;

namespace UIT.iDeal.Infrastructure.Bootstrapper.StartupTasks
{
    public class MvcBootstrapper : IRunTaskAtStartup
    {
        private readonly IWindsorContainer _container;

        public MvcBootstrapper(IWindsorContainer container)
        {
            _container = container;
        }

        public void Execute()
        {
            RegisterGlobalFilters(GlobalFilters.Filters);

            var controllerFactory = new WindsorControllerFactory(_container);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Guard.That(() => filters).IsNotNull();

            filters.Add(new HandleErrorAttribute());
            
        }
    }
}