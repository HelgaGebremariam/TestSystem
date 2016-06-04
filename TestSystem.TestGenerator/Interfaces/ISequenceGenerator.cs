using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystem.TestGenerator.Interfaces
{
    public interface ISequenceGenerator
    {
        int SequenceLength { get; set; }
        int N { get; set; }
        double MathematicalExpectation { get; set; }
        double RoofMeanSquareDeviation { get; set; }
        double Min { get; set; }
        double Max { get; set; }
        int Nu { get; set; }
        double[] GenerateSequence();
    }
}
