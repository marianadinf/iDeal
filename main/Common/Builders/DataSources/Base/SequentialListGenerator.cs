using FizzWare.NBuilder;

namespace UIT.iDeal.Common.Builders.DataSources.Base
{
    public class SequentialListGenerator : IDataGenerator<int> //where T : struct, IConvertible
    {
        private readonly RepeatableSequentialGenerator _generator;

        public SequentialListGenerator()
        {
            _generator = new RepeatableSequentialGenerator();
            _generator.Direction = GeneratorDirection.Ascending;
        }

        public SequentialListGenerator(int minValue, int maxValue)
        {
            _generator = new RepeatableSequentialGenerator(minValue, maxValue);
        }

        public int Generate()
        {
            return _generator.Generate();
        }

        public void StartingWith(int nextValueToGenerate)
        {
            _generator.StartingWith(nextValueToGenerate);
        }

        public int Increment
        {
            get { return _generator.Increment; }
            set { _generator.Increment = value; }
        }

        public bool Unique
        {
            get { return _generator.Unique; }
            set { _generator.Unique = value; }
        }
    }
}