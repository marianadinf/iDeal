using System.Collections.Generic;
using FizzWare.NBuilder;

namespace UIT.iDeal.Common.Builders
{
    public abstract class ObjectBuilder<T> : IObjectBuilder<T> where T : class
    {
        public virtual ISingleObjectBuilder<T> InitializeOne()
        {
            var entity = Builder<T>
                .CreateNew();

            return entity;
        }

        public virtual IListBuilder<T> InitializeList(int size)
        {
            var list = Builder<T>
                .CreateListOfSize(size);
            return list;
        }

        public T CreateOne()
        {
            return InitializeOne().Build();
        }

        public IList<T> CreateList(int size)
        {
            return InitializeList(size).Build();
        }

        public virtual IList<T> CreateMany()
        {
            return InitializeList(Many).Build();
        }

        public int Many
        {
            get { return 3; }
        }

    }
}