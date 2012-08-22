using UIT.iDeal.Common.Interfaces.Commands;

namespace UIT.iDeal.Common.Interfaces.ObjectMapping
{
    public interface IMapToCommand<T> where T : ICommand
    {
    }
}