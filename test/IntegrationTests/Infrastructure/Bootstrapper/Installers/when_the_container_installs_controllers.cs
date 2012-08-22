using System.Linq;
using System.Web.Mvc;
using Castle.Core;
using Castle.Core.Internal;
using Castle.Windsor;
using Machine.Specifications;
using UIT.iDeal.Common.Extensions;
using UIT.iDeal.Common.Helpers;
using UIT.iDeal.Infrastructure.Bootstrapper.Installers;
using UIT.iDeal.TestLibrary.Extensions;

namespace UIT.iDeal.IntegrationTests.Infrastructure.Bootstrapper.Installers
{
    [Subject("Given a new container")]
    public class when_the_container_installs_controllers
    {
        protected static IWindsorContainer _container;

        Because of = () =>
            _container = new WindsorContainer().Install(new ControllersInstaller());

        It should_only_install_types_that_implement_the_IController_interface = () =>
        {
            var allHandlers = WindsorHelpers.GetAllHandlers(_container);
            var controllerHandlers = WindsorHelpers.GetHandlersFor(typeof(IController), _container);

            allHandlers.ShouldNotBeEmpty();
            allHandlers.ShouldEqual(controllerHandlers);
        };

        It should_install_all_types_that_implement_the_IController_interface = () =>
        {
            // Is<TType> is a helper extension method from Windsor
            // which behaves like 'is' keyword in C# but at a Type, not instance level
            var allControllers = WindsorHelpers.GetPublicClassesFromApplicationAssembly(c => c.Is<IController>());
            var registeredControllers = WindsorHelpers.GetImplementationTypesFor(typeof(IController), _container);
            allControllers.ShouldEqual(registeredControllers);
        };

        It should_only_install_types_that_have_Controller_suffix = () =>
        {
            var allControllers = WindsorHelpers.GetPublicClassesFromApplicationAssembly(c => (c.Name.EndsWith("Controller") && c.GetInterface("IController") != null));
            var registeredIControllers = WindsorHelpers.GetImplementationTypesFor(typeof(IController), _container);

            allControllers.ShouldBeEquivalentTo(registeredIControllers);
        };

        It should_only_install_types_that_live_in_the_Controllers_namespace = () =>
        {
            var allControllers = WindsorHelpers.GetPublicClassesFromApplicationAssembly(
                c => c.Namespace.Contains("Controllers") && c.CanBeCastTo<IController>());
            var registeredControllers = WindsorHelpers.GetImplementationTypesFor(typeof(IController), _container);
            allControllers.ShouldEqual(registeredControllers);
        };

        It should_give_all_Controllers_a_lifestyle_of_transient = () =>
        {
            var nonTransientControllers = WindsorHelpers.GetHandlersFor(typeof(IController), _container)
                .Where(controller => controller.ComponentModel.LifestyleType != LifestyleType.Transient)
                .ToArray();

            nonTransientControllers.ShouldBeEmpty();
        };
    }
}
