using System;
using FizzWare.NBuilder;

namespace UIT.iDeal.Common.Builders.DataSources.Base
{
    /// <summary>
    /// The NBuilder SequentialGenerator does not reset the sequence, which you need to do for enumerations, days of the week, etc.
    /// This class decorates the NBuilder SequentialGenerator with the ability to 
    /// (a) reset the sequence when the end is reached (the default) or
    /// (b) throw an exception when the end is reached
    /// </summary>
    internal class RepeatableSequentialGenerator : SequentialGenerator<int>
    {
        private readonly SequentialGenerator<int> _generator;
        private int _minValue = 0;
        private int _maxValue = int.MaxValue;

        public RepeatableSequentialGenerator()
        {
            _generator = new SequentialGenerator<int>();
        }

        public RepeatableSequentialGenerator(int minValue, int maxValue)
        {
            _minValue = minValue;
            _maxValue = maxValue;
            _generator = new SequentialGenerator<int>();
        }

        protected override void Advance()
        {
            int current = _generator.Generate();
            if (Direction == GeneratorDirection.Ascending)
            {
                if (current > _maxValue)
                {
                    if (Unique)
                        throw new InvalidOperationException("There are not enough elements in the data source to continue adding unique items");
                    _generator.StartingWith(_minValue);
                    current = _generator.Generate();
                }
            }
            else
            {
                if (current > _minValue)
                {
                    if (Unique)
                        throw new InvalidOperationException("There are not enough elements in the data source to continue adding unique items");
                    _generator.StartingWith(_maxValue);
                    current = _generator.Generate();
                }
            }
            base.StartingWith(current);
        }

        public override void StartingWith(int nextValueToGenerate)
        {
            if (_generator != null)
            {
                _generator.StartingWith(nextValueToGenerate);
            }
        }

        public new int Increment
        {
            get { return _generator.Increment; }
            set { _generator.Increment = value; }
        }

        public new GeneratorDirection Direction
        {
            get { return _generator.Direction; }
            set
            {
                _generator.Direction = value;
                _generator.StartingWith(value == GeneratorDirection.Ascending ? _minValue : _maxValue);
            }
        }

        public int MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        public int MaxValue
        {
            get { return _maxValue; }
            set { _maxValue = value; }
        }

        public bool Unique { get; set; }
    }
}