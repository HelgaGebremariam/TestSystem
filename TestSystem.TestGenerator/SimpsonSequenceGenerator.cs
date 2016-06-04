using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.TestGenerator
{
    public class SimpsonSequenceGenerator : SequenceGenerator
    {
        public SimpsonSequenceGenerator(IRandomGenerator randomGenerator) : base(randomGenerator)
        {
        }

        public double[] GenerateSequence()
        {
            double [] sequence = new double[SequenceLength];
            double a = Min/2;
            double b = Max/2;
            for (int i = 0; i < SequenceLength; i++)
            {
                double x = a + (b - a)*_randomGenerator.GetNextNumber();
                double y = a + (b - a) * _randomGenerator.GetNextNumber();
                sequence[i] = x + y;
            }
            return sequence;
        }
    }
}
