using System;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using UIT.iDeal.Common.Configuration;
using UIT.iDeal.Common.Extensions;

namespace UIT.iDeal.Common.Helpers
{
    public static class WindsorHelpers
    {
        public static FromAssemblyDescriptor GetAssemblyDescriptorFromExecuting()
        {
            return AllTypes.FromAssembly(Assembly.GetExecutingAssembly());
        }
        public static IHandler[] GetAllHandlers(IWindsorContainer container)
        {
            return GetHandlersFor(typeof(object), container);
        }
        
        public static IHandler[] GetHandlersFor(Type type, IWindsorContainer container)
        {
            return container.Kernel.GetAssignableHandlers(type);
        }

        public static Type[] GetImplementationTypesFor(Type type, IWindsorContainer container)
        {
            return GetHandlersFor(type, container)
                .Select(h => h.ComponentModel.Implementation)
                .OrderBy<Type, string>(t => t.Name)
                .ToArray();
        }

        public static Type[] GetPublicClassesFromApplicationAssembly(Predicate<Type> where)
        {
            return
                AssemblyScanner
                    .GetExportedTypesFromAssemblyNamed(ConfigSettings.WebAssemblyName)
                    .Matching(t => t.IsConcrete() && t.IsClass)
                    .Matching(where.Invoke)
                    .OrderBy(t => t.Name)
                    .ToArray();
        }
    }
}
