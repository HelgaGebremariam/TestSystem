using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.TestGenerator
{
    public class SequenceGenerator : ISequenceGenerator
    {

        protected IRandomGenerator _randomGenerator;
   
        public SequenceGenerator(IRandomGenerator randomGenerator)
        {
            _randomGenerator = randomGenerator;
        }

        protected int _sequenceLength;
        public int SequenceLength 
        {
            get
            {
                if (_sequenceLength <= 0)
                    _sequenceLength = 100;
                return _sequenceLength;
            }

            set
            {
                _sequenceLength = value;
            }
        }

        protected int _n;
        public int N
        {
            get
            {
                if (_n < 6 || _n > 12)
                    return _n = 6;
                return _n;
            }

            set { _n = value; }
        }

        protected double _m;

        public double MathematicalExpectation
        {
            get { return _m; }

            set { _m = value; }
        }

        protected double _rMSD;

        public double RoofMeanSquareDeviation
        {
            get { return _rMSD;}
            set { _rMSD = value; }
        }

        protected double _min;

        public double Min
        {
            get { return _min; }
            set { _min = value; }
        }
        protected double _max;

        public double Max
        {
            get { return _max; }
            set { _max = value; }
        }

        protected double[] GenerateSequence(int length)
        {
            double[] sequence = new double[length];
            for (int i = 0; i < length; i++)
            {
                sequence[i] = Min + ((Max - Min) * _randomGenerator.GetNextNumber());

            }
            return sequence;
        }

        public virtual double[] GenerateSequence()
        {
            return GenerateSequence(SequenceLength);
        }
    }
}
