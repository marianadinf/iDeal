using System;
using Castle.Windsor;
using FluentValidation;

namespace UIT.iDeal.Infrastructure.Bootstrapper.Factories
{
    public class WindsorValidatorFactory : ValidatorFactoryBase
    {
        readonly IWindsorContainer container;

        public WindsorValidatorFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator result = null;
            try
            {
                result = this.container.Resolve(validatorType) as IValidator;
            }
            catch
            { }

            return result;

        }
    }
}
