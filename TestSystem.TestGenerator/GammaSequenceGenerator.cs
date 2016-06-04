using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.TestGenerator
{
    public class GammaSequenceGenerator : SequenceGenerator
    {
        public GammaSequenceGenerator(IRandomGenerator randomGenerator) : base(randomGenerator)
        {
        }

        public double[] GenerateSequence()
        {
            double [] sequence = new double[SequenceLength];
            for (int i = 0; i < SequenceLength; i++)
            {
                double[] tempSequence = base.GenerateSequence(Nu);
                double tempMagicNumber = 1;
                for (int j = 0; j < Nu; j++)
                {
                    tempMagicNumber *= tempSequence[j];
                }
                sequence[i] = -(MathematicalExpectation/Nu)*Math.Log(tempMagicNumber);
            }
            return sequence;
        }

    }
}
