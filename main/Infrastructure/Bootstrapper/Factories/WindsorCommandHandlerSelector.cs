using System;
using System.Reflection;
using Castle.Facilities.TypedFactory;
using UIT.iDeal.Common.Interfaces.Commands;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Factories
{
    public class WindsorCommandHandlerSelector : DefaultTypedFactoryComponentSelector
    {
        protected override Type GetComponentType(MethodInfo method, object[] arguments)
        {
            var command = arguments[0];
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            return handlerType;
        }
    }
}