using FizzWare.NBuilder;

namespace UIT.iDeal.Common.Builders.DataSources.Base
{
    public class RandomListGenerator : IDataGenerator<int> //where T : struct, IConvertible
    {
        readonly int _minValue;
        readonly int _maxValue;
        private readonly RandomGenerator _generator;

        public RandomListGenerator()
        {
            _generator = new RandomGenerator();
        }

        public RandomListGenerator(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _generator = new RandomGenerator();
        }

        public int Generate()
        {
            return _generator.Next(_minValue, _maxValue);
        }

        public void StartingWith(int nextValueToGenerate)
        {
            throw new System.NotImplementedException();
        }

        public int Increment { get; set; }
        public bool Unique { get; set; }
    }
}