using Castle.Windsor;

namespace UIT.iDeal.TestLibrary.Configuration.Ioc
{
    public interface IConfigureIoc : IConfigurationItem
    {
        IWindsorContainer Container { get; }
    }
}