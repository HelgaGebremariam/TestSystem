using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.TestGenerator
{
    public class TriangleSequenceGenerator : SequenceGenerator
    {
        public TriangleSequenceGenerator(IRandomGenerator randomGenerator) : base(randomGenerator)
        {
        }

        public TriangleSequenceGenerator(IRandomGenerator randomGenerator, bool isIncrease) : base(randomGenerator)
        {
            IsIncrease = isIncrease;
        }
        public bool IsIncrease { get; set; }

        public double[] GenerateSequence()
        {
            double[] sequence = new double[SequenceLength];

            for (int i = 0; i < SequenceLength; i++)
            {
                double R1 = _randomGenerator.GetNextNumber();
                double R2 = _randomGenerator.GetNextNumber();
                if (IsIncrease)
                {
                    sequence[i] = Min + (Max - Min)*Math.Max(R1, R2);
                }
                else
                {
                    sequence[i] = Min + (Max - Min)*Math.Min(R1, R2);
                }

            }
            return sequence;
        }
    }
}
