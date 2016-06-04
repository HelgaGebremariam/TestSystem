using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.TestGenerator
{
    public class ExponentialSequenceGenerator : SequenceGenerator
    {
        public ExponentialSequenceGenerator(IRandomGenerator randomGenerator) : base(randomGenerator)
        {
        }

        public double[] GenerateSequence()
        {
            double[] sequence = base.GenerateSequence();
            for (int i = 0; i < SequenceLength; i++)
            {
                sequence[i] = -MathematicalExpectation*Math.Log(sequence[i]);
            }
            return sequence;
        }
    }
}
