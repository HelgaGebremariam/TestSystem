using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.TestGenerator
{
    public class MushGenerator : IRandomGenerator
    {
        private Random rnd = new Random();
        public double GetNextNumber()
        {
            return (double) rnd.Next() / int.MaxValue;
        }
    }
}
