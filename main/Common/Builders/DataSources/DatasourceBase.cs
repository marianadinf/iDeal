namespace UIT.iDeal.Common.Builders.DataSources
{
    public abstract class DatasourceBase<T>
    {
        public abstract T Next();
        public IDataGenerator<int> Generator { get; set; }
        public int Increment
        {
            get { return Generator.Increment; } 
            set { Generator.Increment = value; }
        }

        public DatasourceBase()
        {
            Generator = new SequentialListGenerator();
            //Generator.Direction = GeneratorDirection.Ascending;
            Increment = 1;
            Generator.StartingWith(0);
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
