using System.Collections.Generic;
using UIT.iDeal.Common.Interfaces.Commands;

namespace UIT.iDeal.Infrastructure.ObjectMapping
{
    public interface IModelMapper
    {
        ICommand MapFormToCommand(object form);
        TDestination CreateInstance<TSource, TDestination>(TSource source);
        IEnumerable<TDestination> CreateSet<TSource, TDestination>(IEnumerable<TSource> source);
    }
}