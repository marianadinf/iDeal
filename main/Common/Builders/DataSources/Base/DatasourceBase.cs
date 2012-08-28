using System;
using System.Collections;
using System.Collections.Generic;

namespace UIT.iDeal.Common.Builders.DataSources.Base
{
    public abstract class DatasourceBase<T> : IEnumerable<T>
    {
        private readonly Lazy<List<T>> _lazyList;

        public IDataGenerator<int> Generator { get; set; }
        public int Increment
        {
            get { return Generator.Increment; } 
            set { Generator.Increment = value; }
        }

        public DatasourceBase()
        {
            Generator = new SequentialListGenerator();
            _lazyList = new Lazy<List<T>>(InitialiseList);
            Increment = 1;
            Generator.StartingWith(0);
        }

        public virtual T Next()
        {
            return _lazyList.Value[Generator.Generate()];
        }

        protected abstract List<T> InitialiseList();

        public IEnumerator<T> GetEnumerator()
        {
            return _lazyList.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public interface IDataGenerator<T>
    {
        T Generate();
        void StartingWith(T nextValueToGenerate);
        T Increment { get; set; }
        bool Unique { get; set; }
    }

    //public class RandomDataGenerator : IDataGenerator<int> where T : struct, IConvertible
    //{
    //    private readonly RandomGenerator _generator;

    //    public RandomDataGenerator()
    //    {
    //        _generator = new RandomGenerator();
    //        _generator.Direction = GeneratorDirection.Ascending;
    //    }

    //    public int Generate()
    //    {
    //        return _generator.Next(3, 12);
    //    }

    //    public void StartingWith(int nextValueToGenerate)
    //    {
    //        _generator.StartingWith(nextValueToGenerate);
    //    }

    //    public int Increment
    //    {
    //        get { return 0; }
    //        set { _generator.Increment = value; }
    //    }
    //}

}
