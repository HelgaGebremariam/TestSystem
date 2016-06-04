using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.TestGenerator
{
    public class GaussianSequenceGenerator : SequenceGenerator
    {
        public GaussianSequenceGenerator(IRandomGenerator randomGenerator) : base(randomGenerator)
        {
        }

        public double[] GenerateSequence()
        {
            double[] sequence = new double[SequenceLength];
            double[] tempSequence;
            for (int i = 0; i < SequenceLength; i++)
            {
                tempSequence = GenerateSequence(N);
                double tempMagicNumber = 0;
                for (int j = 0; j < N; j++)
                    tempMagicNumber += tempSequence[j];
                tempMagicNumber -= (double)N/2;
                sequence[i] = MathematicalExpectation + (Math.Sqrt(12/(double)N)*RoofMeanSquareDeviation * tempMagicNumber);
            }

            return sequence;
        }
    }
}
