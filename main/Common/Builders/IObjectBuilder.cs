using System.Collections.Generic;
using FizzWare.NBuilder;

namespace UIT.iDeal.Common.Builders
{
    public interface IObjectBuilder<T>
    {
        ISingleObjectBuilder<T> InitializeOne();
        IListBuilder<T> InitializeList(int size);

        T CreateOne();
        IList<T> CreateList(int size);
        IList<T> CreateMany();
        int Many { get; }
    }
}