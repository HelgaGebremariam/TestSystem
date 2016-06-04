using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystem.TestGenerator.Interfaces;

namespace TestSystem.TestGenerator
{
    public class StatisticsCreator
    {
        private ISequenceGenerator _sequenceGenerator;
        private double[] _sequence;
        public StatisticsCreator(ISequenceGenerator sequenceGenerator)
        {
            _sequenceGenerator = sequenceGenerator;
        }

        public int[] CreateGistogramm(int countIntervals)
        {
            if (_sequence == null)
            {
                _sequence = _sequenceGenerator.GenerateSequence();
            }
            int[] gistogramm = new int[countIntervals];
            int diapason = 1;
            double diapasonSize = (double)diapason / countIntervals;

            for (int i = 0; i < _sequenceGenerator.SequenceLength; i++)
            {
                int intervalNumber = (int)(_sequence[i] / diapasonSize);

                gistogramm[intervalNumber]++;
            }

            return gistogramm;
        }

        public double MathematicalExpectation
        {
            get
            {
                double sum = 0;
                for (int i = 0; i < _sequence.Length; i++)
                {
                    sum += _sequence[i];
                }
                return (double)sum / _sequence.Length;
            }
        }

        public double Dispersion
        {
            get
            {
                double mathematicalExpectation = MathematicalExpectation;
                double dispersion = 0;
                for (int i = 0; i < _sequence.Length; i++)
                {
                    dispersion += Math.Pow(_sequence[i] - mathematicalExpectation, 2);
                }
                dispersion /= _sequence.Length;
                return dispersion;
            }
        }
        public double SKO
        {
            get
            {
                return Math.Sqrt(Dispersion);
            }
        }

        public double IndirectEstimate
        {
            get
            {
                int K = 0;
                for (int i = 1; i < _sequence.Length; i += 2)
                {
                    if (Math.Pow((double)_sequence[i], 2) + Math.Pow((double)_sequence[i - 1], 2) < 1)
                        K++;
                }
                return (2 * (double)K) / (double)_sequence.Length;
            }
        }
    }
}
