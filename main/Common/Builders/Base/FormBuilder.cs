using System.Collections.Generic;
using UIT.iDeal.Common.ObjectMapping;

namespace UIT.iDeal.Common.Builders.Base
{
    public abstract class FormBuilder<TForm> : ObjectBuilder<TForm> where TForm : class
    {
        public static ModelMapper Mapper;

        public TForm CreateOne<TSource>(TSource source)
        {
            return Mapper.CreateInstance<TSource, TForm>(source);
        }

        public IEnumerable<TForm> CreateList<TSource>(IEnumerable<TSource> source)
        {
            return Mapper.CreateSet<TSource, TForm>(source);
        }

    }
}