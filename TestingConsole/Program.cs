using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem;
using TestSystem.TestGenerator;
using TestSystem.TestGenerator.Interfaces;

namespace TestingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IRandomGenerator randomGenerator = new MushGenerator();
            ISequenceGenerator sequenceGenerator = new GaussianSequenceGenerator(randomGenerator);
            StatisticsCreator statisticsCreator = new StatisticsCreator(sequenceGenerator);
            sequenceGenerator.Min = 0;
            sequenceGenerator.Max = 1;
            sequenceGenerator.RoofMeanSquareDeviation = 0.1;
            double[] sequence = sequenceGenerator.GenerateSequence();
        }
    }
}
